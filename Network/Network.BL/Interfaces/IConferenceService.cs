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
        MembersOfConference GetMembership(Guid confId, Guid userId);
        IQueryable<Guid> GetListReports(Guid confId);
    }
}
