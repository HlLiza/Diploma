using Microsoft.AspNet.Identity;
using Network.BL.Interfaces;
using Network.BL.WebServices;
using Network.DAL.EFModel;
using Network.Views.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Network.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAducationService _aducationService;

        public UserController(UserService userService, AducationService aducationService) : base()
        {
            _userService = userService;
            _aducationService = aducationService;
        }

        // GET: User
        [Authorize]        
        public ActionResult Index()
        {
            var model = GetProfile();
            return View(model);
        }

        [Authorize(Roles = "group_member")]
        protected UserIndexViewModel GetProfile()
        {
            var aspId = User.Identity.GetUserId();
            var aspUser = _userService.GetAspUser(aspId);
            UserIndexViewModel model = new UserIndexViewModel();
            model.AspUserId = aspId;
            model.Role = _userService.GetRoleNameByAspId(aspId);
            model.Id = _userService.GetIdByAspId(aspId);

            if (!User.IsInRole("secretary"))
            {
                if (model.Id != null)
                {
                    var data = _userService.GetUserById(model.Id);
                    model.Id = data.Id;
                    model.Name = data.Name;
                    model.Surname = data.Surname;
                    model.Direction = data.Direction;
                    model.Skype = data.Skype;
                    model.Image = data.Image;
                }

                model.PhoneNumber = aspUser.PhoneNumber;
                model.Email = aspUser.Email;

                var aducation = _aducationService.GetAducation(model.Id);
                model.Aducation = GetAducInfo(aducation);

                return model;
            }
            return null;
        }


        [HttpGet]
        public ActionResult ChangePhoto()
        {
            ChangePhotoViewModel model = new ChangePhotoViewModel();
            model.UserId = _userService.GetIdByAspId(User.Identity.GetUserId());
            return PartialView("_ChangePhoto",model);
        }

        [HttpPost]
        public ActionResult ChangePhoto(ChangePhotoViewModel viewModel)
        {
            if (viewModel.Image != null)
            {
                var user = _userService.GetUserById(viewModel.UserId);
                var img = _userService.СonvertingImg(viewModel.Image);
                user.Image = img;
                _userService.UpdateUser(user);
                return RedirectToAction("Index", "User");
            }

             return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult EditPersInfo()
        {
            var user = _userService.GetUserByAspNetId(User.Identity.GetUserId());
            EditPersInfViewModel model = new EditPersInfViewModel()
            {
                Name = user.Name,
                Surname=user.Surname,
                Skype=user.Skype,
                Direction=user.Direction,
                //Image=user.Image
            };

            return PartialView("_EditPersInfo", model);
        }

        [HttpPost]
        public ActionResult EditPersInfo(EditPersInfViewModel model)
        {
            if (model != null)
            {
                var user = _userService.GetUserByAspNetId(User.Identity.GetUserId());
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Skype = model.Skype;
                user.Direction = model.Direction;
                _userService.UpdateUser(user);
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult EditAducation()
        {
            return PartialView("_AducationInfo");
        }

        [HttpPost]
        public ActionResult EditAducation(Conference mod)
        {
            return RedirectToAction("Index");
        }






        List<AducationInfo> GetAducInfo(IQueryable<Aducation> data)
        {
            List<AducationInfo> result = new List<AducationInfo>();
            foreach (var item in data)
            {
                AducationInfo inf = new AducationInfo()
                {
                    University = item.University,
                    StartYear = item.StartYear,
                    GradYear = item.GradYear
                };
                result.Add(inf);
            }
            return result;

        }


        [Authorize(Roles = "group_member")]
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
                    Skype = model.Skype,
                    Name=model.Name,
                    Surname=model.Surname,
                    Direction=model.Direction,                    
                    Visibility = true,
                    AspUserId = model.AspUserId,
                    
                };
                _userService.AddUser(user);

                if (model.University != null)
                {
                    Aducation adc = new Aducation()
                    {
                        University = model.University,
                        StartYear = model.StartYear,
                        GradYear = model.GradYear,
                        UserId=user.Id
                    };
                    _aducationService.AddAducation(adc);

                }
            }
            return RedirectToAction("Index","User");
        }


        public ActionResult BrowseUser()
        {
            return View();
        }


        public ActionResult GetAllUsers()
        {
            List<UserListViewModel> model = new List<UserListViewModel>();
            var list = _userService.GetAllUser();

            if (list!=null)
            {
                foreach (var item in list)
                {
                    UserListViewModel user = new UserListViewModel();
                    user.Id = item.Id;
                    user.Name = item.Name;
                    user.Surname = item.Surname;
                    user.Direction = item.Direction;
                    user.Image = item.Image;
                    model.Add(user);
                }

                return PartialView("_GetAllUsers",model);
            }
            else {
                ViewBag.Text = "Список пользователей пуст";
                return PartialView("_GetAllUsers",ViewBag);
            }
        }

        public ActionResult GetLead()
        {
            List<UserListViewModel> model = new List<UserListViewModel>();
            var listId = _userService.GetAllLeadListId();
            var data = _userService.GetUsersByListId(listId);
            if (listId != null && data != null)
            {
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
                return PartialView("_GetLead", model);
            }
            else {
                ViewBag.Text = "Список руководителей групп пуст";
                return PartialView("_GetLead",ViewBag);
            }
           
        }

        public ActionResult GetMemberGroup()
        {
            List<UserListViewModel> model = new List<UserListViewModel>();
            var listId = _userService.GetAllMemberListId();
            var data = _userService.GetUsersByListId(listId);
            if (listId != null && data != null)
            {
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
                return PartialView("_GetMemberGroup", model);
            }
            else {
                ViewBag.Text = "Список участников групп пуст";
                return PartialView("_GetMemberGroup", ViewBag);
            }
        }






        //[HttpGet]
        //public ActionResult EditUser()
        //{
        //    var model = GetProfileFromPage();
        //    return View("EditUser",model);
        //}

        //[HttpPost]
        //public ActionResult EditUser(UserIndexViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        //Aducation aducation = new Aducation()
        //        //{
        //        //    Type = model.Type,
        //        //    Institution = model.Specialization,
        //        //    StartYear = model.StartYear,
        //        //    GradYear = model.GradYear,
        //        //    Specialization = model.Specialization
        //        //};
        //        //User_sContact contact = new User_sContact()
        //        //{
        //        //    PhoneNumber = model.PhoneNumber,
        //        //    Skype = model.Skype
        //        //};
        //        //User_sPersonalData data = new User_sPersonalData()
        //        //{
        //        //    Name = model.Name
        //        //};
        //        User user = new User()
        //        {
        //            AspUserId = model.AspUserId,
        //            Id = model.Id
        //        };

        //      //  _userService.EditProfile(aducation, contact, data, user);
        //        return RedirectToAction("Index", "User");
        //    }
        //    else
        //    {
        //       // var model = GetProfileFromPage();

        //        return View("EditUser", model);
        //    }

           
            

        //    //return RedirectToAction("Index", "User");
        //}



    }
}


