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
        private readonly IGroupService _groupService;

        public UserController(UserService userService, AducationService aducationService,
            GroupService groupService) : base()
        {
            _userService = userService;
            _aducationService = aducationService;
            _groupService = groupService;
        }

        // GET: User
        [Authorize]        
        public ActionResult Index()
        {
            UserIndexViewModel model = new UserIndexViewModel();
            if (User.IsInRole("secretary"))
            {
                 model = GetProfileSecretary();
            }
            if (User.IsInRole("group_member"))
            {
               model = GetProfile();
            }

            return View(model);
        }

        [Authorize(Roles = "secretary")]
        protected UserIndexViewModel GetProfileSecretary()
        {
            UserIndexViewModel model = new UserIndexViewModel();
            return model;
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
            List<EditAducViewModel> model = new List<EditAducViewModel>();
            var userId = _userService.GetIdByAspId(User.Identity.GetUserId());
            var list = _aducationService.GetAducation(userId).ToList();
            if (list.Count!=0)
            {
                foreach (var item in list)
                {
                    EditAducViewModel adc = new EditAducViewModel
                    {
                        University = item.University,
                        GradYear = item.GradYear,
                        StartYear = item.StartYear
                    };
                }

            }
            return PartialView("_EditAducation.cshtml", model);
        }

        [HttpPost]
        public ActionResult EditAducation(Conference mod)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddAducation()
        {
            return PartialView("_AddAducation");
        }

        [HttpPost]
        public ActionResult AddAducation(Aducation model)
        {
            model.UserId = _userService.GetIdByAspId(User.Identity.GetUserId());
            _aducationService.AddAducation(model);
            return RedirectToAction("Index","User");
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
        [HttpGet]
        public ActionResult AddUser(StartAddUserViewModel data)
        {
            AddUserViewModel model = new AddUserViewModel()
            {
                AspUserId = User.Identity.GetUserId(),
                Name=data.Name,
                Surname=data.Surname
            };
           
            return View("AddUser", model);
        }

    

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
            var leadList = _groupService.GetAllLeadId();
            var memberList = _groupService.GetAllMemberListId(leadList);
            var data = _userService.GetUsersByListId(memberList);
            if (memberList != null && data != null)
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
            var listLeadId = _groupService.GetAllLeadId();
            var listId = _groupService.GetAllMemberListId(listLeadId);
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
            else
            {
                ViewBag.Text = "Список участников групп пуст";
                return PartialView("_GetMemberGroup", ViewBag);
            }
        }

        public ActionResult MyProfile()
        {
            return View();
        }
    }
}


