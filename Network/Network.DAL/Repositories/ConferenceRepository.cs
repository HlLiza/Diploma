using Network.DAL.EFModel;
using Network.DAL.Interfaces;
using System;
using System.Linq;

namespace Network.DAL.Repositories
{
    public class ConferenceRepository:RepositoryBase, IConference
    {
        public ConferenceRepository(InstitutNetworkContext context) : base(context) { }

        public void Add(Conference item)
        {
            _context.Conference.Add(item);
            Save();
        }

        public IQueryable<Conference> GetAll()
        {
            return _context.Conference.Where(x=>x.Visibility==true).OrderBy(x=>x.Date);
        }

        public void Update(Conference item)
        {
            var confer = _context.Conference.Find(item.Id);
            _context.Entry(confer).CurrentValues.SetValues(item);
            Save();
        }

        public void Delete(Conference con)
        {
            _context.Conference.Remove(con);
            Save();
        }

        public Conference Find(Guid id)
        {
            return _context.Conference.Find(id);
        }

        public MembersOfConference GetMemberships(Guid confId)
        {
            return _context.MembersOfConference.FirstOrDefault(x => x.ConferenceId == confId);
        }

        public MembersOfConference GetMembershipByIds(Guid confId, Guid userId)
        {
            return _context.MembersOfConference.FirstOrDefault(x => x.ConferenceId == confId && x.UserId == userId);
        }

        public void JoinToConference(MembersOfConference member)
        {
            _context.MembersOfConference.Add(member);
            Save();
        }

        public void GoOut(Guid id)
        {
            var member = _context.MembersOfConference.Find(id);
            _context.MembersOfConference.Remove(member);
            Save();
        }

        public IQueryable<Guid> GetListMembersId(Guid confId)
        {
            var list = _context.MembersOfConference.Where(s => s.ConferenceId == confId).Select(s => s.UserId);
            return list;
        }

        public IQueryable<Conference> GetArchiveConference()
        {
            return _context.Conference.Where(x => x.Visibility == false);
        }


    }
}

