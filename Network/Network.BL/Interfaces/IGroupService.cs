using Network.DAL.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Network.BL.Interfaces
{
    public interface IGroupService
    {
        IQueryable<Guid> GetAllLeadId();
        IQueryable<Guid> GetAllMemberListId(IQueryable<Guid> listLead);
        void AddResource(ResourceGroup res);
        IQueryable<Group> GetAll();
        IQueryable<Guid> GroupId(Guid userId);
        List<Group> GetGroupByListId(IQueryable<Guid> listId);
        bool UserIsHead(Guid groupId, Guid userId);
        void AddGroup(Group gr);
        void AddMembership(MemberOfGroup membership);
        Group GetGroup(Guid groupId);
        IQueryable<Guid> MembersId(Guid groupId);
        bool UserIsHead(string userId, Guid groupId);
        List<ResourceGroup> GetResourceGroup(Guid groupId);
        IQueryable<User> GetMembers(string direction, string aspId);
        List<string> GetAllDirections();
        ResourceGroup FindRes(Guid resId);
        List<Guid> ExcludeMembers(List<Guid> fullList, List<Guid> excludeList);
    }
}
