using Network.DAL.EFModel;
using System;
using System.Linq;

namespace Network.DAL.Interfaces
{
    public interface IConference
    {
        void Add(Conference item);
        void Update(Conference item);
        Conference Find(Guid id);
        void Delete(Conference gr);
        IQueryable<Conference> GetAll();


        void JoinToConference(MembersOfConference member);
        void GoOut(Guid id);
        MembersOfConference GetMemberships(Guid confId);
        MembersOfConference GetMembershipByIds(Guid confId, Guid userId);
        IQueryable<Guid> GetListMembersId(Guid confId);


       

    }
}
