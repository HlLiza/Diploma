﻿using Microsoft.AspNet.Identity;
using Network.BL.WebServices;
using Network.DAL.EFModel;
using Network.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Network.Controllers
{
    public class ConferenceController : Controller
    {
        private ConferenceService _conferencService;
        private UserService _userService; 

        public ConferenceController(ConferenceService conferencService,
            UserService userService)
        {
            _conferencService = conferencService;
            _userService = userService;
        }

        // GET: Conference
        public ActionResult Index()
        {
            List<ConferenceIndexViewModel> model = new List<ConferenceIndexViewModel>();
            var list = _conferencService.GetConfIndex();

            if (!User.IsInRole("secretary"))
            {
                var userId = _userService.GetIdByAspId(User.Identity.GetUserId());

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
                        confer.MemberConferenceStatus = true;
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
            OpenConferenceViewModel model = new OpenConferenceViewModel()
            {
                Id = conference.Id,
                Thema = conference.Thema,
                Date = Convert.ToDateTime(conference.Date),
                Details = conference.Details,
                Direction=conference.Direction,
                Image=conference.Image,
                Requirements=conference.Requirements
            };
            model.MemberConferenceStatus = false;
            if (!User.IsInRole("secretary"))
            {
                var userId = _userService.GetIdByAspId(User.Identity.GetUserId());
                model.MemberConferenceStatus = _conferencService.UserIsMember(model.Id, userId);
            }
                       
            return View(model);
        }

        public FileResult DownloadRequirements(Guid confId)
        {
            var conference = _conferencService.GetConferenceById(confId);
            byte[] requirements = conference.Requirements;
            string fileName = "Требования "+conference.Thema+".pdf";
            return File(requirements, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public FileResult DownloadReport(Guid reportId)
        {
            var report = _conferencService.GetReport(reportId);
            byte[] data = report.Content;
            string fileName = report.Title + ".doc";
            return File(data, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
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
            if (model != null)
            {
                MembersOfConference member = new MembersOfConference
                {
                    ConferenceId=model.ConfId,
                    UserId=_userService.GetIdByAspId(User.Identity.GetUserId())
                };

                ReportConference report = new ReportConference
                {
                    Title=model.Topic,
                    Extension= ".doc",
                    Content =_conferencService.ConvertFile(model.ReportText),
                   Author=model.Author
                };


                _conferencService.AddMembersToConference(member, report);
            }

            return RedirectToAction("Index","Conference");
        }


        [Authorize(Roles = "secretary")]
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
                    if (lst.Image!= null)
                        user.Image = lst.Image;
                    result.Add(user);
                }
            }

            return PartialView("_GetListOfListener", result);
        }

        [Authorize(Roles = "secretary")]
        public ActionResult GetListOfMembers(Guid confId)
        {
            List<UserAtConference> result = new List<UserAtConference>();
            var membersId = _conferencService.GetMembersList(confId);
            var data = _userService.GetUsersByListId(membersId);
            foreach (var item in data)
            {
                UserAtConference user = new UserAtConference
                {
                    Id = item.Id,
                    Name = item.Name,
                    Surname = item.Surname,
                    Direction = item.Direction
                };
                if (item.Image != null)
                    user.Image = item.Image;
                result.Add(user);
            }
            return PartialView("_GetListOfMember", result);
        }


        [Authorize(Roles = "secretary")]
        public ActionResult AddConference()
        {
            var model = new ConferenceAddViewModel();
            return View("_CreateConference", model);
        }

        [HttpPost]
        [Authorize(Roles = "secretary")]
        public ActionResult AddConference(ConferenceAddViewModel model)
        {
            if (model != null)
            {
                Conference conf = new Conference
                {
                    Thema = model.Thema,
                    Date = model.Date,
                    Direction = model.Direction,
                    Details = model.Details,
                    Requirements = null,
                    Image = null                  
                };

                if (model.Image!=null)
                    conf.Image = _userService.СonvertingImg(model.Image);
                if (model.Requirements!=null)
                    conf.Requirements = _userService.СonvertingImg(model.Requirements);

                _conferencService.AddCovference(conf);
            }
            return View("Index");
        }


        [Authorize(Roles = "group_member")]
        public ActionResult GetMembership(Guid confId)
        {
            var userId = _userService.GetIdByAspId(User.Identity.GetUserId());
            var membership = _conferencService.GetMembership(confId, userId);
            var report = _conferencService.GetReport(membership.ReportId);


            GetMyReportViewModel model = new GetMyReportViewModel
            {
                Title= report.Title,
                Author=report.Author,
                ReportId=report.Id
            };
       
            return PartialView("_GetMembership",model);
        }


        public ActionResult OpenArchive()
        {
            var listArchive = _conferencService.GetArchiveConferenc();
            List<ConferenceArchiveViewModel> model = new List<ConferenceArchiveViewModel>();
            foreach (var item in listArchive)
            {
                ConferenceArchiveViewModel conf = new ConferenceArchiveViewModel
                {
                    Id=item.Id,
                    Thema=item.Thema,
                    Date= Convert.ToDateTime(item.Date),
                    Direction=item.Direction,
                    Image=item.Image
                };
                model.Add(conf);
            }

            return View(model);
        }

        public ActionResult Reports(Guid confId)
        {
            List<ReportsViewModel> model = new List<ReportsViewModel>();
            ReportsViewModel item = new ReportsViewModel()
            {
                ConfId= confId
            };
                model.Add(item);

            return View(model);


            //var listId = _conferencService.GetListReports(confId);
            //if (listId.Count() > 0)
            //{
            //    foreach (var id in listId)
            //    {
            //        var report = _conferencService.GetReport(id);
            //        ReportsViewModel item = new ReportsViewModel();
            //        item.Id = report.Id;
            //        item.Title = report.Title;
            //        item.Author = report.Author;
            //        item.ConfId = confId;
            //        model.Add(item);
            //    }
            //}



        }

        public ActionResult GetAllReport(Guid confId)
        {
            List<Guid> listId = new List<Guid>();
            List<ReportsViewModel> model = new List<ReportsViewModel>();
            if (confId != null)
            {
                listId = _conferencService.GetListReports(confId).ToList();
            }
            foreach (var id in listId)
            {
                var report = _conferencService.GetReport(id);
                ReportsViewModel item = new ReportsViewModel
                {
                    Id = report.Id,
                    Title = report.Title,
                    Author = report.Author,
                    ConfId= confId
                };

                model.Add(item);
            }
            return PartialView("_GetAllReport",model);
        }

        //public List<ReportsViewModel> Search(string str)
        //{
        //    List<ReportsViewModel> model = new List<ReportsViewModel>();
        //    if (str.Length > 0)
        //    {
        //        var data = _conferencService.GetListFts(str);
        //        if (data.Count > 0)
        //        {
        //            foreach (var item in data)
        //            {
        //                ReportsViewModel rp = new ReportsViewModel
        //                {
        //                    Id = item.Id,
        //                    Title = item.Title,
        //                    Author = item.Author
        //                };
        //                model.Add(rp);
        //            }
                   
        //        }

        //    }

        //    return (model);
        //}
    }
}