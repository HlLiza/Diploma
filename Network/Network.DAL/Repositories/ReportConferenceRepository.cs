using Network.DAL.Interfaces;
using System;
using Network.DAL.EFModel;
using System.Linq;
using System.Collections.Generic;

namespace Network.DAL.Repositories
{
    public class ReportConferenceRepository : RepositoryBase, IReportConference
    {
        public ReportConferenceRepository(NetworkContext context) : base(context) { }

        public void Add(ReportConference report)
        {
            _context.ReportConference.Add(report);
            Save();
        }

        public ReportConference Find(Guid id)
        {
            return _context.ReportConference.Find(id);
        }

        public IQueryable<Guid> GetListReportsId(Guid confId)
        {
            return _context.MembersOfConference.Where(x => x.ConferenceId == confId).Select(f => f.ReportId);
        }

        //public  List<ReportConference> GetReportsFTS(string keyWord)
        //{
        //    return _context.SearchFTS(keyWord).ToList();
        //}
    }
}
