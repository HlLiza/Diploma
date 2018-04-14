using Microsoft.AspNet.Identity;
using Network.BL.WebServices;
using Network.DAL.EFModel;
using Network.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Network.Controllers
{
    public class ConferenceController : Controller
    {
        private ConferenceService _conferencService;
        private UserService _userService;

        public ConferenceController(ConferenceService conferencService, UserService userService)
        {
            _conferencService = conferencService;
            _userService = userService;
        }


        // GET: Conference
        public ActionResult Index()
        {
            List<ConferenceIndexViewModel> model = new List<ConferenceIndexViewModel>();
            var list = _conferencService.GetConfIndex();
            var userId = _userService.GetIdByAspId(User.Identity.GetUserId());

            if (!User.IsInRole("secretary"))
            {
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        ConferenceIndexViewModel confer = new ConferenceIndexViewModel();
                        confer.Id = item.Id;
                        confer.Thema = item.Thema;
                        confer.Date = Convert.ToDateTime(item.Date);
                        confer.MemberConferenceStatus = _conferencService.UserIsMember(item.Id, userId);
                        confer.ListenerConferenceStatus = _conferencService.UserIsListener(item.Id,userId);
                        model.Add(confer);

                    }

                }

            }
            else {
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        ConferenceIndexViewModel confer = new ConferenceIndexViewModel();
                        confer.Id = item.Id;
                        confer.Thema = item.Thema;
                        confer.Date = Convert.ToDateTime(item.Date);
                        confer.MemberConferenceStatus =true;
                        model.Add(confer);

                    }
                }
            }

            return View(model);
        }

        //var conferListId = _conferencService.GetConferenceList();
        //var listConference = _conferencService.GetConferList(conferListId);
        //if (!User.IsInRole("secretary"))
        //{
        //    var idString = User.Identity.GetUserId();
        //    var id = _userService.GetUserIdByAspId(idString);

        //    if (conferListId != null)
        //    {

        //        foreach (var item in listConference)
        //        {
        //            ConferenceViewModel confer = new ConferenceViewModel();
        //            confer.Id = item.Id;
        //            confer.Thema = item.Thema;
        //            confer.Date = Convert.ToDateTime(item.Date);
        //           // confer.Place = item.Place;
        //            confer.MembersStatus = _conferencService.CheckMemberInConference(id, item.Id);
        //            model.Add(confer);
        //        }
        //    }
        //    return View(model);
        //}

        //else
        //{
        //    if (conferListId != null)
        //    {

        //        foreach (var item in listConference)
        //        {
        //            ConferenceViewModel confer = new ConferenceViewModel();
        //            confer.Id = item.Id;
        //            confer.Thema = item.Thema;
        //            confer.Date = Convert.ToDateTime(item.Date);
        //           // confer.Place = item.Place;
        //            confer.MembersStatus = true;
        //            model.Add(confer);
        //        }
        //    }
        //    return View(model);

        //}



        [Authorize(Roles = "group_member")]
        public bool IsMemberConference(Guid confId)
        {
            var userId = _userService.GetIdByAspId(User.Identity.GetUserId());
            return _conferencService.UserIsMember(confId, userId); ;
        }

        //[Authorize(Roles = "secretary")]
        //public ActionResult CreateConference()
        //{
        //    var model = new Conference();
        //    return View("_CreateConference", model);
        //}

        //[HttpPost]
        //[Authorize(Roles = "secretary")]
        //public ActionResult CreateConference(Conference model)
        //{
        //    if (model != null)
        //    {
        //        _conService.AddCovference(model);
        //    }
        //    return RedirectToAction("Index", "Conference");
        //}


        //public ActionResult JoinConference(Guid id)
        //{
        //    MembersOfConference mem = new MembersOfConference()
        //    {
        //        ConferenceId = id
        //    };
        //    return View("_JoinConference", mem);
        //}

        //[HttpPost]
        //public ActionResult JoinConference(MembersOfConference mem)
        //{
        //    var curUser = User.Identity.GetUserId();
        //    var user = _userService.GetUserByAspNetId(curUser);
        //    mem.UserId = user.Id;
        //    var check = _conferencService.CheckMemberInConference(user.Id, mem.ConferenceId);

        //    if (check)
        //    {

        //        _conferencService.AddMembersToConference(mem);
        //    }

        //    return RedirectToAction("Index", "Conference");
        //}

        ////public ActionResult ListMembersOfConference(Guid id)
        ////{
        ////    List<UserListViewModel> model = new List<UserListViewModel>();
        ////    var listMembersId = _conService.GetMembersListByConferenceId(id);
        ////    var data = _userService.GetDataForListOfUser(listMembersId);

        ////    foreach (var item in data)
        ////    {
        ////        UserListViewModel user = new UserListViewModel();
        ////        user.Id = item.Id;
        ////        user.Name = item.Name;
        ////        user.Image = _userService.GetImageByDataId(item.Id);

        ////        model.Add(user);
        ////    }

        ////    return PartialView("_ListMembersOfConference", model);
        ////}

        //[HttpGet]
        //public ActionResult GetListConferenceForUser()
        //{
        //    var idString = User.Identity.GetUserId();
        //    var id = _userService.GetUserIdByAspId(idString);
        //    var listIdOfConfer = _conferencService.ListConferIdsByMemberId(id);
        //    var conference = _conferencService.GetConferList(listIdOfConfer);

        //    List<ConferenceViewModel> model = new List<ConferenceViewModel>();

        //    foreach (var item in conference)
        //    {
        //        ConferenceViewModel confer = new ConferenceViewModel();
        //        confer.Id = item.Id;
        //        confer.Thema = item.Thema;
        //        confer.Date = Convert.ToDateTime(item.Date);
        //        //confer.Place = item.Place;
        //        model.Add(confer);
        //    }


        //    return View(model);
        //}

        //public ActionResult LeaveConference(Guid ConferenceId)
        //{
        //    var idString = User.Identity.GetUserId();
        //    var idMem = _userService.GetUserIdByAspId(idString);

        //    var membership = _conferencService.GetMembership(ConferenceId, idMem);

        //    return View("_LeaveConference", membership);
        //}

        //[HttpPost]
        //public ActionResult LeaveConference(MembersOfConference mem)
        //{
        //    _conferencService.RemoveMembers(mem);

        //    return RedirectToAction("GetListConferenceForUser", "Conference");
        //}


    }
}