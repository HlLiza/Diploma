using Network.DAL.EFModel;
using System;
using System.Linq;

namespace Network.DAL.Interfaces
{
    public interface IGroup
    {
        void Add(Group gr);
        void Update(Group gr);
        Group Find(Guid id);
        void Delete(Group gr);
        IQueryable<Group> GetGroupsByHead(Guid id); 
        IQueryable<Group> GetGroups();
        IQueryable<Guid> AllLeadId();
        IQueryable<User> GetMembersId(string direction);
    }
}
