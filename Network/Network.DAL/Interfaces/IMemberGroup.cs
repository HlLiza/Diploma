using Network.DAL.EFModel;
using System;
using System.Linq;

namespace Network.DAL.Interfaces
{
    public interface IMemberGroup
    {
        void Add(MemberOfGroup member);
        void Delete(MemberOfGroup member);
        void Update(MemberOfGroup member);
        IQueryable<Guid> FindGroupsForUser(Guid userId);
        IQueryable<Guid> FindUsersForGroup(Guid groupId);

        bool UserIsMember(Guid userId,Guid groupId);
        IQueryable<Guid> GetListMemberId();
    }
}
