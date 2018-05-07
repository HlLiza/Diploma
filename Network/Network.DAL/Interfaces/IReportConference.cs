using Network.DAL.EFModel;
using System;
using System.Linq;


namespace Network.DAL.Interfaces
{
    public interface IReportConference
    {
        void Add(ReportConference report);
        ReportConference Find(Guid id);
        IQueryable<Guid> GetListReportsId(Guid confId);
    }
}
