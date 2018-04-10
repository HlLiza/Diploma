//using Microsoft.AspNet.Identity;
//using Network.BL.WebServices;
//using Network.DAL.EFModel;
//using Network.Views.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Web.Mvc;

//namespace Network.Controllers
//{
//    public class AdvertisementController : Controller
//    {
//        private readonly UserService _userService;
//        private readonly AdvertisementService _advService;

//        public AdvertisementController(UserService userService, AdvertisementService advService)
//        {
//            _userService = userService;
//            _advService = advService;
//        }

//        // GET: Advertisement
//        public ActionResult Index()
//        {
//            List<AdvertismentViewModel> model = new List<AdvertismentViewModel>();
//            var advList = _advService.GetAllAdvertisment();

//                foreach (var item in advList)
//                {
//                AdvertismentViewModel adv = new AdvertismentViewModel();
//                    adv.Id = item.Id;
//                    adv.Information = item.Information;
//                    adv.Date = Convert.ToDateTime(item.Date);
//                    model.Add(adv);
//                }
//            return View(model);
//        }


//        public ActionResult Add()
//        {
//            var senderAspId = User.Identity.GetUserId();
//            Advertisement adv = new Advertisement() { };
//            adv.SenderId = senderAspId;
//            return View("_AddAdvertisement", adv);
//        }

//        [HttpPost]
//        public ActionResult Add(Advertisement adv)
//        {
//            if (adv != null)
//            {
//                _advService.AddAdvert(adv);
//            }
            
//            return RedirectToAction("Index", "Advertisement");
//        }

//        public ActionResult GetAllAdvertisment()
//        {
//            return View("_GetListAdvertisement");
//        }

//    }
//}