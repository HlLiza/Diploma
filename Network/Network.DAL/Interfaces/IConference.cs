using Network.DAL.EFModel;
using System;
using System.Linq;

namespace Network.DAL.Interfaces
{
    public interface IConference
    {
        void AddConference(Conference item);
        void Update(Conference item);
        Conference Find(Guid id);
        void DeleteConference(Conference gr);
        IQueryable<Guid> GetAllConferencesId();

        void AddMembers(MembersOfConference member);
        void DeleteMembers(Guid id);
        MembersOfConference GetUserByIdMember(Guid id);
        IQueryable<Guid> GetMembersIdByConferenceId(Guid id);
        IQueryable<MembersOfConference> CheckMember(Guid userId, Guid conferenceId);
        Guid GetConferenceIdByUserId(Guid userId);

        IQueryable<Guid> GetConferIdList(Guid memberId);

        MembersOfConference GetMembership(Guid confId, Guid memId);


    }
}
