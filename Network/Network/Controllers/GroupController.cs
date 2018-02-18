using Microsoft.AspNet.Identity;
using Network.BL.WebServices;
using Network.DAL.EFModel;
using Network.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace Network.Controllers
{
    public class GroupController : Controller
    {    
        private  GroupService _groupService;
        private  UserService _userService;
        public GroupController(GroupService groupService, UserService userService)
        {
            _groupService = groupService;
            _userService = userService;
        }
              
        public ActionResult Index()
        {
            List<GroupViewModel> model = new List<GroupViewModel>();
            var idList = _groupService.GetListOfId();
            if (idList != null)
            {
                var listGroup = _groupService.GetGroupList(idList);

                foreach (var item in listGroup)
                {
                    GroupViewModel gr = new GroupViewModel();
                    gr.Id = item.Id;

                    var userHead = _userService.GetUserPersData(item.HeadId);
                    //var head = _userService.GetDataByAspUserId(userHead.Id);
                    gr.NameHead = userHead.Name;

                    gr.Number = Convert.ToInt32(item.Number);
                    gr.Specialization = item.Specialization;

                    model.Add(gr);
                }

            }

            return View(model);
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Authorize(Roles = "secretary")]
        public ActionResult CreateGroup()
        {
            var leadList = GetLead();
            var model = new CreateGroup();
            model.ListLead = leadList;
            return View("_CreateGroup",model);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Authorize(Roles = "secretary")]
        public ActionResult CreateGroup(CreateGroup u)
        {
            if (u != null)
            {
                Group gr = new Group
                {                    
                    HeadId = u.Head.Id,
                    Number = u.Number,
                    Specialization = u.Specialization
                };
                _groupService.AddGroup(gr);
            }
            return RedirectToAction("Index","Group");
        }

        [System.Web.Mvc.Authorize(Roles = "team_lead")]
        public ActionResult AddMembToGroup(Guid groupId)
        {
            AddToGroupMember model = new AddToGroupMember();
            var aspIdsAllUsers = _userService.GetAllMemberListId();

            var listIdAllMem = _userService.GetUserIdForListAspId(aspIdsAllUsers);
            var listIdGroupMem = _groupService.GetmembersListByGroupId(groupId).ToList();

            var listId = _userService.ExcludeListIdInLIs(listIdAllMem, listIdGroupMem);

            var userList = _userService.GetDataForListOfUser(listId);

            List<UserListViewModel> modell = new List<UserListViewModel>();
            foreach (var item in userList)
            {
                UserListViewModel user = new UserListViewModel();
                user.Id = item.Id;
                user.Name = item.Name;
                user.Image = _userService.GetImageByDataId(item.Id);

                modell.Add(user);
            }
            model.grId = groupId;
            model.ListUser = modell;



            return PartialView("_AddMembToGroup", model);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize(Roles = "team_lead")]
        public ActionResult AddToGroup(Guid memId,Guid groupId)
        {
            var user = _userService.GetUserByUserPersDataId(memId);
            var check = _groupService.CheckMemberInGroup(user.Id,groupId);

            if (check == true)
            {
                MembersOfGroup members = new MembersOfGroup();
                members.GroupId = groupId;
                members.MembersId = user.Id;
                _groupService.AddMembersToGroup(members);
                return RedirectToAction("Index", "Group");
            }
            else return RedirectToAction("Index", "Group");               
            }

        [System.Web.Http.Authorize(Roles = "team_lead")]
        public ActionResult RemoveFromGroup(Guid memId, Guid groupId)
        {
            var user = _userService.GetUserByUserPersDataId(memId);
            _groupService.DeleteMemberInGroup(groupId, user.Id);
            return RedirectToAction("GroupsForUser", "Group");
        }



        public ActionResult ListMembersOfGroup(Guid id)
        {
            List<UserListViewModel> members = new List<UserListViewModel>();
            var listMembersId = _groupService.GetmembersListByGroupId(id);
            var data = _userService.GetDataForListOfUser(listMembersId);

            foreach (var item in data)
            {
                UserListViewModel user = new UserListViewModel();
                user.Id = item.Id;
                user.Name = item.Name;
                user.Image = _userService.GetImageByDataId(item.Id);

                members.Add(user);
            }

            UserListOfGroupViewModel model = new UserListOfGroupViewModel();
            model.Members = members;
            model.Id = id;
            model.Status = _groupService.CheckHeadGroup(User.Identity.GetUserId(),id);
           
            return PartialView("_ListMembersOfGroup", model);
        }
        
        [System.Web.Mvc.Authorize(Roles = "secretary")]
        public ActionResult DeleteGroup(Guid id)
        {
            var group = _groupService.GetGroupById(id);
            return View("_Delete", group);
        }
        [System.Web.Mvc.Authorize(Roles = "secretary")]
        [System.Web.Mvc.HttpPost]
        public ActionResult DeleteGroup(Group gr)
        {
           _groupService.DeleteMembersInGroup(gr.Id);           
            _groupService.DeleteGroup(gr.Id);            
            return RedirectToAction("Index","Group");
        }


       
        public ActionResult GroupsForUser()
        {
            var idString = User.Identity.GetUserId();
            var id = _userService.GetUserIdByAspId(idString);
            var user = _userService.GetUserById(id);
            
            List<Group> list = new List<Group>();

            if (User.IsInRole("team_lead"))
            {
                 list = _groupService.GetGroupsForHead(user.PersonalDataId);
            }
            if (User.IsInRole("group_member"))
            {
                 list = _groupService.GetGroupsForUser(user.Id);
            }
          

            List<GroupViewModel> model = new List<GroupViewModel>();
            foreach (var item in list)
            {
                GroupViewModel gr = new GroupViewModel();
                gr.Id = item.Id;

                var userHead = _userService.GetUserPersData(item.HeadId);
                gr.NameHead = userHead.Name;

                gr.Number = Convert.ToInt32(item.Number);
                gr.Specialization = item.Specialization;

                model.Add(gr);
            }


            return View(model);
        }

      

        /// <summary>
        /// ////
        /// </summary>
        /// <returns></returns>
        public List<SelectLeadViewModel> GetLead()
        {
            var leadList = _userService.GetLeadAll();
            if (leadList != null)
            {
                List<SelectLeadViewModel> list = new List<SelectLeadViewModel>();
                foreach (var lead in leadList)
                {
                    SelectLeadViewModel item = new SelectLeadViewModel();
                    item.Id = lead.Id;
                    item.Name = lead.Name;
                    list.Add(item);
                }
                return list;
            }
            else return null;
        }
    }
}