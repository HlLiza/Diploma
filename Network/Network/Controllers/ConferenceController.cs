﻿using Microsoft.AspNet.Identity;
using Network.BL.WebServices;
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
        

        [Authorize(Roles = "group_member")]
        public bool IsMemberConference(Guid confId)
        {
            var userId = _userService.GetIdByAspId(User.Identity.GetUserId());
            return _conferencService.UserIsMember(confId, userId); ;
        }

        [Authorize(Roles = "group_member")]
        public ActionResult AddListener(Guid confId)
        {
            var userId = _userService.GetIdByAspId(User.Identity.GetUserId());
            if (confId != Guid.Empty && userId != Guid.Empty)
            {
                _conferencService.AddListener(userId,confId);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "group_member")]
        public ActionResult DeleteListener(Guid confId)
        {
            var userId = _userService.GetIdByAspId(User.Identity.GetUserId());
            if (confId != Guid.Empty && userId != Guid.Empty)
            {
                _conferencService.DeleteListener(userId, confId);
            }
            return RedirectToAction("Index");
        }

        public ActionResult OpenConference(Guid confId)
        {
            var conference = _conferencService.GetConferenceById(confId);
            return View(conference);
        }

        [System.Web.Mvc.Authorize(Roles = "group_member")]
        [HttpGet]
        public ActionResult JoinToConference(Guid confId)
        {
            var userId = _userService.GetIdByAspId(User.Identity.GetUserId());
            RegistrToConfer model = new RegistrToConfer()
            {
                ConfId = confId,
                UserId = userId
            };

            return View("_JoinToConference",model);
        }

        [HttpPost]
        public ActionResult JoinToConference(RegistrToConfer model)
        {

            return RedirectToAction("Index","Conference");
        }


        //[Authorize(Roles = "secretary")]
        public ActionResult GetListOfListener(Guid confId)
        {
            List<UserAtConference> result = new List<UserAtConference>();

            var listenerIds = _conferencService.GetListIdListeners(confId);
            var lesteners = _userService.GetUsersByListId(listenerIds);
            if (lesteners.Count != 0)
            {
                foreach (var lst in lesteners)
                {
                    UserAtConference user = new UserAtConference
                    {
                        Id=lst.Id,
                        Name=lst.Name,
                        Surname=lst.Surname,
                        Direction=lst.Direction
                    };
                    if (lst.Image.Length != 0)
                        user.Image = lst.Image;
                    result.Add(user);
                }
            }

            return PartialView("_GetListOfListener", result);
        }

        [Authorize(Roles = "secretary")]
        public ActionResult GetListOfMembers(Guid confId)
        {
            List<UserAtConference> list = new List<UserAtConference>();

            return PartialView();
        }


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