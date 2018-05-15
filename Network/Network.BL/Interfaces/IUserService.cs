using Network.DAL.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Network.BL.Interfaces
{
    public interface IUserService
    {
        void AddUser(User user);
        byte[] СonvertingImg(HttpPostedFileBase img);
        void UpdateUser(User user);
        //IQueryable<string> GetAllLeadListId();
        //IQueryable<string> GetAllMemberListId();
        User GetUserById(Guid id);
        User GetUserByAspNetId(string id);
        Guid GetUserIdByAspId(string id);
        string GetTypeUser(string id);
        string GetRoleNameByAspId(string id);
        string GetRoleNameForUser(string id);
        List<Guid> ExcludeListIdInLIs(List<Guid> listIdsUsers, List<Guid> listIdGroupMem);
        List<Guid> GetUserIdForListAspId(IQueryable<string> stringId);
        IQueryable<User> GetAllUser();
        //List<User> GetUsersByListId(IQueryable<string> listId);
        AspNetUsers GetAspUser(string id);
        Guid GetIdByAspId(string id);
        IQueryable<User> GetUsersByDirection(string direction);
        List<User> GetUsersByListId(IQueryable<Guid> listId);










    }
}
