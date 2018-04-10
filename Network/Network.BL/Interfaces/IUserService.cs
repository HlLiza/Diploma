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
        IQueryable<string> GetAllUsersId();
        IQueryable<string> GetAllLeadListId();
        IQueryable<string> GetAllMemberListId();
        User GetUserById(Guid id);
        User GetUserByAspNetId(string id);
        Guid GetUserIdByAspId(string id);
        string GetTypeUser(string id);
        string GetRoleNameByAspId(string id);
        string GetRoleNameForUser(string id);
        List<Guid> ExcludeListIdInLIs(List<Guid> listIdsUsers, List<Guid> listIdGroupMem);
        List<Guid> GetUserIdForListAspId(IQueryable<string> stringId);










    }
}
