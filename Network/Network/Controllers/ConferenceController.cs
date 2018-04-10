//using Microsoft.AspNet.Identity;
//using Network.BL.WebServices;
//using Network.DAL.EFModel;
//using Network.Views.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Web.Mvc;

//namespace Network.Controllers
//{
//    public class ConferenceController : Controller
//    {
//        private  ConferenceService _conService;
//        private UserService _userService;

//        public ConferenceController(ConferenceService conService, UserService userService)
//        {
//            _conService = conService;
//            _userService = userService;
//        }


//        // GET: Conference
//        public ActionResult Index()
//        {

//            List<ConferenceViewModel> model = new List<ConferenceViewModel>();
//            var conferListId = _conService.GetConferenceList();
//            var listConference = _conService.GetConferList(conferListId);
//            if (!User.IsInRole("secretary"))
//            {
//                var idString = User.Identity.GetUserId();
//                var id = _userService.GetUserIdByAspId(idString);

//                if (conferListId != null)
//                {

//                    foreach (var item in listConference)
//                    {
//                        ConferenceViewModel confer = new ConferenceViewModel();
//                        confer.Id = item.Id;
//                        confer.Thema = item.Thema;
//                        confer.Date = Convert.ToDateTime(item.Date);
//                        confer.Place = item.Place;
//                        confer.MembersStatus = _conService.CheckMemberInConference(id, item.Id);
//                        model.Add(confer);
//                    }
//                }
//                return View(model);
//            }

//            else
//            {
//                if (conferListId != null)
//                {

//                    foreach (var item in listConference)
//                    {
//                        ConferenceViewModel confer = new ConferenceViewModel();
//                        confer.Id = item.Id;
//                        confer.Thema = item.Thema;
//                        confer.Date = Convert.ToDateTime(item.Date);
//                        confer.Place = item.Place;
//                        confer.MembersStatus = true;
//                        model.Add(confer);
//                    }
//                }
//                return View(model);

//            }

//        }

//        [Authorize(Roles = "secretary")]
//        public ActionResult CreateConference()
//        {
//            var model = new Conference();
//            return View("_CreateConference",model);
//        }

//        [HttpPost]
//        [Authorize(Roles = "secretary")]
//        public ActionResult CreateConference(Conference model)
//        {
//            if (model != null)
//            {
//                _conService.AddCovference(model);
//            }
//            return RedirectToAction("Index", "Conference");
//        }


//        public ActionResult JoinConference(Guid id)
//        {
//            MembersOfConference mem = new MembersOfConference()
//            {
//                ConferenceId = id
//            };
//            return View("_JoinConference", mem);
//        }

//        [HttpPost]
//        public ActionResult JoinConference(MembersOfConference mem)
//        {
//            var curUser = User.Identity.GetUserId();
//            var user = _userService.GetUserByAspNetId(curUser);
//            mem.UserId = user.Id;
//            var check = _conService.CheckMemberInConference(user.Id, mem.ConferenceId);

//            if (check)
//            {

//                _conService.AddMembersToConference(mem);
//            }

//            return RedirectToAction("Index", "Conference");
//        }

//        public ActionResult ListMembersOfConference(Guid id)
//        {
//            List<UserListViewModel> model = new List<UserListViewModel>();
//            var listMembersId = _conService.GetMembersListByConferenceId(id);
//            var data = _userService.GetDataForListOfUser(listMembersId);

//            foreach (var item in data)
//            {
//                UserListViewModel user = new UserListViewModel();
//                user.Id = item.Id;
//                user.Name = item.Name;
//                user.Image = _userService.GetImageByDataId(item.Id);

//                model.Add(user);
//            }

//            return PartialView("_ListMembersOfConference", model);
//        }

//        [HttpGet]
//        public ActionResult GetListConferenceForUser()
//        {
//            var idString = User.Identity.GetUserId();
//            var id = _userService.GetUserIdByAspId(idString);
//            var listIdOfConfer = _conService.ListConferIdsByMemberId(id);
//            var conference = _conService.GetConferList(listIdOfConfer);

//            List<ConferenceViewModel> model = new List<ConferenceViewModel>();

//            foreach (var item in conference)
//            {
//                ConferenceViewModel confer = new ConferenceViewModel();
//                confer.Id = item.Id;
//                confer.Thema = item.Thema;
//                confer.Date = Convert.ToDateTime(item.Date);
//                confer.Place = item.Place;
//                model.Add(confer);
//            }


//            return View(model);
//        }

//        public ActionResult LeaveConference(Guid ConferenceId)
//        {
//            var idString = User.Identity.GetUserId();
//            var idMem = _userService.GetUserIdByAspId(idString);

//            var membership = _conService.GetMembership(ConferenceId, idMem);
            
//            return View("_LeaveConference", membership);
//        }

//        [HttpPost]
//        public ActionResult LeaveConference(MembersOfConference mem)
//        {
//            _conService.RemoveMembers(mem);

//            return RedirectToAction("GetListConferenceForUser", "Conference");
//        }


//    }
//}