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
        IQueryable<Guid> GetListMembersId(Guid confId);
        MembersOfConference GetMembership(Guid confId);
        //IQueryable<MembersOfConference> CheckMember(Guid userId, Guid conferenceId);

        //IQueryable<Guid> GetMembersIdByConferenceId(Guid id);

        //Guid GetConferenceIdByUserId(Guid userId);

        //IQueryable<Guid> GetConferIdList(Guid memberId);




    }
}
