using Microsoft.AspNet.Identity;
using Network.BL.Interfaces;
using Network.BL.WebServices;
using Network.DAL.EFModel;
using Network.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Network.Controllers
{
    public class GroupController : Controller
    {
        private IGroupService _groupService;
        private IUserService _userService;
        private IConferenceService _conferService;
        public GroupController(GroupService groupService, UserService userService,ConferenceService conferService)
        {
            _groupService = groupService;
            _userService = userService;
            _conferService = conferService;
        }

        public ActionResult Index()
        {
            var role = _userService.GetRoleNameByAspId(User.Identity.GetUserId());
            List<IndexGroup> model = new List<IndexGroup>();
            if (role == "secretary")
            {
                model = SecretaryGroup();
            }

            if (role == "group_member")
            {   
                model = MemberGroup();

            }

            return View(model);
        }

        [Authorize(Roles = "secretary")]
        List<IndexGroup> SecretaryGroup()
        {
            List<IndexGroup> result = new List<IndexGroup>();
            var list = _groupService.GetAll();
            if (list != null)
            {
                foreach (var item in list)
                {
                    IndexGroup group = new IndexGroup();
                    group.Id = item.Id;
                    group.NameProject = item.NameProject;
                    group.Direction = item.Direction;
                    group.Head = _userService.GetUserById(item.HeadId);
                    group.UserIsHead =false;

                    result.Add(group);
                }
             }

            return result;
        }

        [Authorize(Roles = "group_member")]
        List<IndexGroup> MemberGroup()
        {
            List<IndexGroup> result = new List<IndexGroup>();
            var userId = _userService.GetUserIdByAspId(User.Identity.GetUserId());

            var idGroup = _groupService.GroupId(userId);
            var groupList = _groupService.GetGroupByListId(idGroup);

            if (groupList.Count() > 0)
            {
                foreach (var item in groupList)
                {
                    IndexGroup group = new IndexGroup();
                    group.Id = item.Id;
                    group.NameProject = item.NameProject;
                    group.Direction = item.Direction;
                    group.Head = _userService.GetUserById(item.HeadId);
                    group.UserIsHead = _groupService.UserIsHead(group.Id, userId);              
                    result.Add(group);
                }
            }

            return result;
        }

        [Authorize(Roles = "secretary")]
        public ActionResult AddGroup()
        {
            var model = new AddGroup();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "secretary")]
        public ActionResult AddGroup(AddGroup model)
            {
            if (model == null || model.NameProject == null)
            {
                return Json(new { status = "error", message = "Ошибка при создании группы" });
            }

            Group gr = new Group()
            {
                Id = Guid.NewGuid(),
                NameProject = model.NameProject,
                Direction = model.Direction,
                HeadId = model.SelectedHead,
                DateStart = model.DateStart,
                DateFinish = model.DateFinish,
            };
            MemberOfGroup membership = new MemberOfGroup()
            {
                Id=Guid.NewGuid(),
                GroupId=gr.Id,
                UserId=gr.HeadId
            };
            _groupService.AddGroup(gr);
            _groupService.AddMembership(membership);

            return RedirectToAction("Index");
        }
        public ActionResult SelectDirection(string direction)
        {
            var users = _userService.GetUsersByDirection(direction);
            AddGroup model = new AddGroup();
            model.Direction = direction;

            if (users.Count() > 0)
            {
                model.Head = users;
            }
            else
            {
                model.Head = _userService.GetAllUser();
            }

            return PartialView("_AddModel", model);
        }

        public PartialViewResult ItemInfo(Guid groupId)
        {
            if (groupId!=null)
            {
                var group = _groupService.GetGroup(groupId);
                var head = _userService.GetUserById(group.HeadId);
                SimpleInfo headItem = new SimpleInfo
                {
                    Name = head.Name,
                    Surname = head.Surname,
                    Image = head.Image
                };

                GroupInfo model = new GroupInfo()
                {
                    Direction = group.Direction,
                    NameProject = group.NameProject,
                    DateStart = Convert.ToDateTime(group.DateStart),
                    DateFinish = Convert.ToDateTime(group.DateFinish),
                    Head = headItem,
                    Members=new List<SimpleInfo>()
                };
                model.Members = GetMembers(groupId);
                return PartialView("_ItemInfo",model);
            }
            ViewBag.Property = "Нет соответствующей информации";

            return PartialView("_ItemInfo", ViewBag);
        }

        public ActionResult OpenGroup(Guid groupId)
        {
            var group = _groupService.GetGroup(groupId);
            var head = _userService.GetUserById(group.HeadId);

            OpenGroupViewModel model = new OpenGroupViewModel()
            {
                Id = groupId,
                NameProject = group.NameProject,
                Direction = group.Direction,
                Head = head,
                DateStart = Convert.ToDateTime(group.DateStart),
                DateFinish = Convert.ToDateTime(group.DateFinish),
                Members = new List<SimpleInfo>()
            };
            model.Members = GetMembers(groupId);
            model.UserIsHead = _groupService.UserIsHead(User.Identity.GetUserId(),model.Id);

            return View(model);
        }

        public List<SimpleInfo> GetMembers(Guid groupId)
        {
            List<SimpleInfo> model = new List<SimpleInfo>();
            var idList = _groupService.MembersId(groupId).ToList();
            var userList = _userService.GetUsersByListId(idList);
            if (userList.Count() > 0)
            {
                foreach (var item in userList)
                {
                    SimpleInfo member = new SimpleInfo
                    {
                        Name = item.Name,
                        Surname = item.Surname,
                        Image = item.Image
                    };
                    model.Add(member);
                }
            }
            return model;
        }

        [HttpPost]
        public ActionResult AddResource(AddResource model)
        {
            if (model != null)
            {
                ResourceGroup res = new ResourceGroup
                {
                    AuthorId = _userService.GetIdByAspId(User.Identity.GetUserId()),
                    Date = DateTime.Now,
                    GroupId=model.GroupId,
                    Comments=model.Comments
                };
                var file = _conferService.ConvertFile(model.Resource);
                res.Resource = file;
                res.Id = Guid.NewGuid();
                _groupService.AddResource(res);
            }
            GroupIdParam id = new GroupIdParam
            {
                GroupId = model.GroupId
            };

            var param = model.GroupId.ToString();
            return RedirectToAction ("GetListResource",new {param=id });

        }


        [HttpPost]
        public ActionResult GetListResource(GroupIdParam param)
        {
            List<ResourceListViewModel> resources = new List<ResourceListViewModel>();
            var listRes = _groupService.GetResourceGroup(param.GroupId);
            if (listRes != null)
            {
                
                foreach (var res in listRes)
                {
                    ResourceListViewModel item = new ResourceListViewModel();
                    item.ResourceId = res.Id;
                    item.Comments = res.Comments;
                    var authorRes = _userService.GetUserById(res.AuthorId);
                    item.AuthorName = authorRes.Name;
                    item.AuthorSurname = authorRes.Surname;
                    item.Date = Convert.ToDateTime(res.Date);
                    resources.Add(item);
                }
                return PartialView("_ListResource", resources);
            }
            return PartialView("_ListResource", resources);

        }

        public ActionResult AddingMembers(string direction, Guid groupId)
        {
            try {

                    AddMemberViewModel model = new AddMemberViewModel()
                    {
                        GroupId = groupId,
                    };
                    
                    var list= _groupService.GetAllDirections();
                    list.Add("Все направления");
                    model.Directions = list;
                    model.SelectedDirection = direction;
                  
                return View(model);
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public ActionResult AddMembers(string userId, string groupId)
        {
            try
            {
                MemberOfGroup membership = new MemberOfGroup()
                {
                    Id = Guid.NewGuid(),
                    GroupId =Guid.Parse(groupId),
                    UserId = Guid.Parse(userId)
                };
                _groupService.AddMembership(membership);

                return RedirectToAction("OpenGroup", new { groupId = groupId });
            }
            catch (Exception e)
            {
                ViewBag.Text = "Попробуйте еще раз";
                return View();
            }
        }
      

        public ActionResult GetDirections(SelectDirection w)
        {
            try{
                List<Guid> result = new List<Guid>(); ;
                var allUser = _userService.AllUsersId().ToList();
                var currentMembers = _groupService.MembersId(w.GroupId).ToList();

                if (w.SelectedDirection=="Все направления")
                {                
                    if (currentMembers.Count() > 0)
                    {
                        result = _groupService.ExcludeMembers(allUser, currentMembers);
                    }
                }

                else {
                    var users = _userService.GetUsersByDirection(w.SelectedDirection).Select(x=>x.Id).ToList();
                    if (users.Count() > 0)
                    {
                        result = _groupService.ExcludeMembers(users, currentMembers);
                    }
                }

                
                    List<UserListViewModel> model = new List<UserListViewModel>();

                if (result.Count() > 0)
                {
                    var data = _userService.GetUsersByListId(result);
                    foreach (var item in data)
                    {
                        UserListViewModel user = new UserListViewModel();
                        user.Id = item.Id;
                        user.Name = item.Name;
                        user.Surname = item.Surname;
                        user.Direction = item.Direction;
                        user.Image = item.Image;
                        model.Add(user);
                    }                

                }
                model.OrderBy(x => x.Surname);
                ViewBag.GroupId = w.GroupId;
                return PartialView("_SelectMembers", model);
            }
            catch (Exception e)

            {
                return RedirectToAction("AddMembers");
            }

        }



        public FileResult DownloadResource(Guid resId)
        {
            var res = _groupService.FindRes(resId);
            byte[] requirements = res.Resource;
            string fileName = "Файл.pdf";
            return File(requirements, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

    }
}
