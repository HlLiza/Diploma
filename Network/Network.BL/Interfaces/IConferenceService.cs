using Network.DAL.EFModel;
using System;
using System.Linq;
using System.Web;

namespace Network.BL.Interfaces
{
    public interface IConferenceService
    {
        bool UserIsMember(Guid confId, Guid userId);
        IQueryable<Conference> GetConfIndex();
        IQueryable<Conference> CheckVisibleConf(IQueryable<Conference> list);
        byte[] ConvertFile(HttpPostedFileBase file);
        //void AddCovference(Conference conf);
        //Conference GetConferenceById(Guid id);
        //IQueryable<Guid> GetConferenceList();
        //List<Conference> GetConferList(IQueryable<Guid> listId);
        //void AddMembersToConference(MembersOfConference member);
        //void RemoveMembers(MembersOfConference member);
        //IQueryable<Guid> GetMembersListByConferenceId(Guid id);
        //bool CheckMemberInConference(Guid userId, Guid confId);
        //List<MembersOfConference> GetListMembersByListId(IQueryable<Guid> listId);
        //void DeleteMembersInConference(Guid confId);
        //IQueryable<Guid> ListConferIdsByMemberId(Guid userId);
        //MembersOfConference GetMembership(Guid confId, Guid memId);


    }
}
