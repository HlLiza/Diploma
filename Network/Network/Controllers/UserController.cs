using Microsoft.AspNet.Identity;
using Network.BL.WebServices;
using Network.DAL.EFModel;
using Network.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Network.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        public UserController() { }

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // GET: User
        [Authorize]        
        public ActionResult Index()
        {
            var model = GetProfileFromPage();
            return View(model);
        }

        protected UserIndexViewModel GetProfileFromPage()
        {
            var id = User.Identity.GetUserId();
            UserIndexViewModel model = new UserIndexViewModel();
            model.AspUserId = id;

            if (!User.IsInRole("secretary"))
            {
                var user = _userService.GetUserByAspNetId(id);
                model.Id = user.Id;

                //var data = _userService.GetDataByAspUserId(id);
                //if (data != null)
                //{
                //    model.Name = data.Name;
                //    model.Image = _userService.GetImageByDataId(data.Id);

                //}

                //var contact = _userService.GetContactById(user.ContactId);
                //if (contact != null)
                //{
                //    model.Skype = contact.Skype;
                //    model.PhoneNumber = contact.PhoneNumber;
                //}


                //var aducation = _userService.GetAducationInf(model.Id);
                //if (aducation != null)
                //{
                //    model.Type = aducation.Type;
                //    model.Institution = aducation.Institution;
                //    model.Specialization = aducation.Specialization;
                //    model.StartYear = aducation.StartYear;
                //    model.GradYear = aducation.GradYear;
                //}
                return model;

            }
            return null;
        }




       public ActionResult AddUser(string id)
        {

            AddUserViewModel model = new AddUserViewModel()
            {
                AspUserId = id
               
            };
            return View("AddUser", model);
        }

        [HttpPost]
        public ActionResult AddUser(AddUserViewModel model)
        {
            if (model != null)
            {
                User user = new User()
                {
                    
                    Visibility = true,
                    AspUserId = model.AspUserId
                };
                _userService.AddUser(user);
            }
            return View("DisplayEmail");
        }


        public ActionResult BrowseUser()
        {
            return View();
        }


        public ActionResult GetAllUsers()
        {
            List<UserListViewModel> model = new List<UserListViewModel>();
            //var listIdsUsers = _userService.GetAllUsersId();

            //var data = _userService.GetDataForListOfUserByAspId(listIdsUsers);

            //foreach (var item in data)
            //{
            //    UserListViewModel user = new UserListViewModel();
            //    user.Id = item.Id;
            //    user.Name = item.Name;
            //    user.Image = _userService.GetImageByDataId(item.Id);

            //    model.Add(user);
            //}
            return PartialView("_GetAllUsers",model);
        }

        public ActionResult GetLead()
        {
            List<UserListViewModel> model = new List<UserListViewModel>();
            //var listIdsUsers = _userService.GetAllLeadListId();

            //var data = _userService.GetDataForListOfUserByAspId(listIdsUsers);
            //foreach (var item in data)
            //{
            //    UserListViewModel user = new UserListViewModel();
            //    user.Id = item.Id;
            //    user.Name = item.Name;
            //    user.Image = _userService.GetImageByDataId(item.Id);

            //    model.Add(user);
            //}
            return PartialView("_GetLead",model);
        }

        public ActionResult GetMemberGroup()
        {
            List<UserListViewModel> model = new List<UserListViewModel>();
            //var listIdsUsers = _userService.GetAllMemberListId();
            //var data = _userService.GetDataForListOfUserByAspId(listIdsUsers);
            //foreach (var item in data)
            //{
            //    UserListViewModel user = new UserListViewModel();
            //    user.Id = item.Id;
            //    user.Name = item.Name;
            //    user.Image = _userService.GetImageByDataId(item.Id);

            //    model.Add(user);
            //}

            return PartialView("_GetMemberGroup", model);
        }

        //public ActionResult GetContact(Guid idUser)
        //{
        //    var user = _userService.GetUserById(idUser);
        //    var contact = _userService.GetContactById(user.ContactId);
        //    return PartialView("_ShowContact",contact);
        //}

        [HttpGet]
        public ActionResult EditUser()
        {
            
            var model = GetProfileFromPage();

            return View("EditUser",model);
        }

        [HttpPost]
        public ActionResult EditUser(UserIndexViewModel model)
        {
            if (ModelState.IsValid)
            {

                //Aducation aducation = new Aducation()
                //{
                //    Type = model.Type,
                //    Institution = model.Specialization,
                //    StartYear = model.StartYear,
                //    GradYear = model.GradYear,
                //    Specialization = model.Specialization
                //};
                //User_sContact contact = new User_sContact()
                //{
                //    PhoneNumber = model.PhoneNumber,
                //    Skype = model.Skype
                //};
                //User_sPersonalData data = new User_sPersonalData()
                //{
                //    Name = model.Name
                //};
                User user = new User()
                {
                    AspUserId = model.AspUserId,
                    Id = model.Id
                };

              //  _userService.EditProfile(aducation, contact, data, user);
                return RedirectToAction("Index", "User");
            }
            else
            {
               // var model = GetProfileFromPage();

                return View("EditUser", model);
            }

           
            

            //return RedirectToAction("Index", "User");
        }



    }
}


