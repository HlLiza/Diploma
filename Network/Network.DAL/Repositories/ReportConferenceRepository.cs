using Network.DAL.Interfaces;
using System;
using Network.DAL.EFModel;
using System.Linq;

namespace Network.DAL.Repositories
{
    public class ReportConferenceRepository : RepositoryBase, IReportConference
    {
        public ReportConferenceRepository(InstitutNetworkContext context) : base(context) { }

        public void Add(ReportConference report)
        {
            _context.ReportConference.Add(report);
            Save();
        }

        public ReportConference Find(Guid id)
        {
            return _context.ReportConference.Find(id);
        }

        //public IQueryable<ReportConference> GetReportForConference(Guid confId)
        //{
        //    return _context.ReportConference.Where(s => s.ConferenceId == confId);
        //}
    }
}
