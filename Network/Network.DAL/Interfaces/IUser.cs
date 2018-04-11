﻿using Network.DAL.EFModel;
using System;
using System.Linq;

namespace Network.DAL.Interfaces
{
    public interface IUser
    {
        void AddUser(User item);
        void Update(User item);
        User Find(Guid id);
        User GetUserByAspUserId(string id);
        IQueryable<User> GetAll();


        string GetRoleId(string userId);
        string GetRole(string id);

        IQueryable<string> GetAllLeadId();
        IQueryable<string> GetAllMemberId();
      
    }
}
