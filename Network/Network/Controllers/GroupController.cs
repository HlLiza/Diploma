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
            var idList = _groupService.MembersId(groupId);
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

//directions.GroupId = groupId;
//directions.Directions = list;
//directions.SelectedDirection = direction;                
//model.ListDirections = directions;

//List<SelectMemberViewModel> listUser = new List<SelectMemberViewModel>();


//if (direction.Length > 0)
//{
//    var members = _groupService.GetMembers(direction, User.Identity.GetUserId());
//    if (members == null)
//        listUser = null;
//    else
//    {
//        foreach (var item in members)
//        {
//            SelectMemberViewModel user = new SelectMemberViewModel()
//            {
//                Id = item.Id,
//                Name = item.Name,
//                Surname = item.Surname,
//                ImageUser = item.Image
//            };
//            listUser.Add(user);
//        }
//    }
//    model.Users = listUser;
//    return View(model);
//}


//}



//public ActionResult Index()
//{
//    List<GroupViewModel> model = new List<GroupViewModel>();
//    var idList = _groupService.GetListOfId();
//    if (idList != null)
//    {
//        var listGroup = _groupService.GetGroupList(idList);

//        foreach (var item in listGroup)
//        {
//            GroupViewModel gr = new GroupViewModel();
//            gr.Id = item.Id;

//            var userHead = _userService.GetUserPersData(item.HeadId);
//            var head = _userService.GetDataByAspUserId(userHead.Id);
//            gr.NameHead = userHead.Name;

//            gr.Number = Convert.ToInt32(item.Number);
//            gr.Specialization = item.Specialization;

//            model.Add(gr);
//        }

//    }

//    return View(model);
//}

//[System.Web.Mvc.HttpGet]
//[System.Web.Mvc.Authorize(Roles = "secretary")]
//public ActionResult CreateGroup()
//{
//    var leadList = GetLead();
//    var model = new CreateGroup();
//    model.ListLead = leadList;
//    return View("_CreateGroup", model);
//}

//[System.Web.Mvc.HttpPost]
//[System.Web.Mvc.Authorize(Roles = "secretary")]
//public ActionResult CreateGroup(CreateGroup u)
//{
//    if (u != null)
//    {
//        Group gr = new Group
//        {
//            HeadId = u.Head.Id,
//            Number = u.Number,
//            Specialization = u.Specialization
//        };
//        _groupService.AddGroup(gr);
//    }
//    return RedirectToAction("Index", "Group");
//}

//[System.Web.Mvc.Authorize(Roles = "team_lead")]
//public ActionResult AddMembToGroup(Guid groupId)
//{
//    AddToGroupMember model = new AddToGroupMember();
//    var aspIdsAllUsers = _userService.GetAllMemberListId();

//    var listIdAllMem = _userService.GetUserIdForListAspId(aspIdsAllUsers);
//    var listIdGroupMem = _groupService.GetmembersListByGroupId(groupId).ToList();

//    var listId = _userService.ExcludeListIdInLIs(listIdAllMem, listIdGroupMem);

//    var userList = _userService.GetDataForListOfUser(listId);

//    List<UserListViewModel> modell = new List<UserListViewModel>();
//    foreach (var item in userList)
//    {
//        UserListViewModel user = new UserListViewModel();
//        user.Id = item.Id;
//        user.Name = item.Name;
//        user.Image = _userService.GetImageByDataId(item.Id);

//        modell.Add(user);
//    }
//    model.grId = groupId;
//    model.ListUser = modell;



//    return PartialView("_AddMembToGroup", model);
//}

//[System.Web.Http.HttpPost]
//[System.Web.Http.Authorize(Roles = "team_lead")]
//public ActionResult AddToGroup(Guid memId, Guid groupId)
//{
//    var user = _userService.GetUserByUserPersDataId(memId);
//    var check = _groupService.CheckMemberInGroup(user.Id, groupId);

//    if (check == true)
//    {
//        MembersOfGroup members = new MembersOfGroup();
//        members.GroupId = groupId;
//        members.MembersId = user.Id;
//        _groupService.AddMembersToGroup(members);
//        return RedirectToAction("Index", "Group");
//    }
//    else return RedirectToAction("Index", "Group");
//}

//[System.Web.Http.Authorize(Roles = "team_lead")]
//public ActionResult RemoveFromGroup(Guid memId, Guid groupId)
//{
//    var user = _userService.GetUserByUserPersDataId(memId);
//    _groupService.DeleteMemberInGroup(groupId, user.Id);
//    return RedirectToAction("GroupsForUser", "Group");
//}



//public ActionResult ListMembersOfGroup(Guid id)
//{
//    List<UserListViewModel> members = new List<UserListViewModel>();
//    var listMembersId = _groupService.GetmembersListByGroupId(id);
//    var data = _userService.GetDataForListOfUser(listMembersId);

//    foreach (var item in data)
//    {
//        UserListViewModel user = new UserListViewModel();
//        user.Id = item.Id;
//        user.Name = item.Name;
//        user.Image = _userService.GetImageByDataId(item.Id);

//        members.Add(user);
//    }

//    UserListOfGroupViewModel model = new UserListOfGroupViewModel();
//    model.Members = members;
//    model.Id = id;
//    model.Status = _groupService.CheckHeadGroup(User.Identity.GetUserId(), id);

//    return PartialView("_ListMembersOfGroup", model);
//}

//[System.Web.Mvc.Authorize(Roles = "secretary")]
//public ActionResult DeleteGroup(Guid id)
//{
//    var group = _groupService.GetGroupById(id);
//    return View("_Delete", group);
//}


//[System.Web.Mvc.Authorize(Roles = "secretary")]
//[System.Web.Mvc.HttpPost]
//public ActionResult DeleteGroup(Group gr)
//{
//    _groupService.DeleteMembersInGroup(gr.Id);
//    _groupService.DeleteGroup(gr.Id);
//    return RedirectToAction("Index", "Group");
//}



//public ActionResult GroupsForUser()
//{
//    var idString = User.Identity.GetUserId();
//    var id = _userService.GetUserIdByAspId(idString);
//    var user = _userService.GetUserById(id);

//    List<Group> list = new List<Group>();

//    if (User.IsInRole("team_lead"))
//    {
//        list = _groupService.GetGroupsForHead(user.PersonalDataId);
//    }
//    if (User.IsInRole("group_member"))
//    {
//        list = _groupService.GetGroupsForUser(user.Id);
//    }


//    List<GroupViewModel> model = new List<GroupViewModel>();
//    foreach (var item in list)
//    {
//        GroupViewModel gr = new GroupViewModel();
//        gr.Id = item.Id;

//        var userHead = _userService.GetUserPersData(item.HeadId);
//        gr.NameHead = userHead.Name;

//        gr.Number = Convert.ToInt32(item.Number);
//        gr.Specialization = item.Specialization;

//        model.Add(gr);
//    }


//    return View(model);
//}



/// <summary>
/// ////
/// </summary>
/// <returns></returns>
//public List<SelectLeadViewModel> GetLead()
//{
//    var leadList = _userService.GetLeadAll();
//    if (leadList != null)
//    {
//        List<SelectLeadViewModel> list = new List<SelectLeadViewModel>();
//        foreach (var lead in leadList)
//        {
//            SelectLeadViewModel item = new SelectLeadViewModel();
//            item.Id = lead.Id;
//            item.Name = lead.Name;
//            list.Add(item);
//        }
//        return list;
//    }
//    else return null;
//}
//    }
//}