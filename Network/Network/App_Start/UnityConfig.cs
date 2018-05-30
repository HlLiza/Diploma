using Network.BL.WebServices;
using Network.BL.Interfaces;
using Network.Controllers;
using Network.DAL.Interfaces;
using Network.DAL.Repositories;
using System;

using Unity;
using Network.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Network
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });
        public static IUnityContainer Container => container.Value;
        #endregion       
        public static void RegisterTypes(IUnityContainer container)
        {            
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<ApplicationSignInManager>();
            // TODO: Register your type's mappings here.
            container.RegisterType<DAL.Interfaces.IUser, UserRepository>();
            container.RegisterType<IAducation, AducationRepository>();
            container.RegisterType<IConference, ConferenceRepository>();
            container.RegisterType<IListenerConference, ListenerConfRepository>();
            container.RegisterType<IReportConference, ReportConferenceRepository>();

            container.RegisterType<IUserService,UserService>();
            container.RegisterType<IAducationService, AducationService>();


            container.RegisterType<AccountController>();

            container.RegisterType<UserController>();
            container.RegisterType<GroupController>();
            container.RegisterType<ConferenceController>();


        }
    }
}