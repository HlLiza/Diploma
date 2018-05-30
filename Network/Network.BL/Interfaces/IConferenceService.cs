using Network.DAL.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Network.BL.Interfaces
{
    public interface IConferenceService
    {
        bool UserIsMember(Guid confId, Guid userId);
        IQueryable<Conference> GetConfIndex();
        List<Conference> CheckVisibleConf(List<Conference> list);
        byte[] ConvertFile(HttpPostedFileBase file);
        MembersOfConference GetMembership(Guid confId, Guid userId);
        //List<ReportConference> GetListFts(string keyWord);
    }
}
