using Network.DAL.EFModel;
using Network.DAL.Interfaces;
using System;
using System.Linq;

namespace Network.DAL.Repositories
{
    public class ConferenceRepository:RepositoryBase, IConference
    {
        public ConferenceRepository(InstitutNetworkContext context) : base(context) { }

        public void AddConference(Conference item)
        {
            _context.Conference.Add(item);
            base.Save();
        }

        public void AddMembers(MembersOfConference member)
        {
            _context.MembersOfConference.Add(member);
            base.Save();
        }

        public IQueryable<MembersOfConference> CheckMember(Guid userId, Guid conferenceId)
        {
            var member = _context.MembersOfConference.Where(x => x.ConferenceId == conferenceId && x.UserId == userId);
            return member;
        }

        public void DeleteConference(Conference con)
        {
            _context.Conference.Remove(con);
            base.Save();
        }

        public void DeleteMembers(Guid id)
        {
            var member = _context.MembersOfConference.Find(id);
            _context.MembersOfConference.Remove(member);
            base.Save();
        }

        public Conference Find(Guid id)
        {
            return _context.Conference.Find(id);
        }

        public IQueryable<Guid> GetAllConferencesId()
        {
            return _context.Conference.Select(x=>x.Id);
        }

        public Guid GetConferenceIdByUserId(Guid userId)
        {
            var list = _context.MembersOfConference.FirstOrDefault(s => s.UserId == userId);
            return list.ConferenceId;
        }

        public MembersOfConference GetUserMemberByIdMember(Guid id)
        {
            var list = _context.MembersOfConference.FirstOrDefault(x => x.Id == id);
            return list;
        }

        public IQueryable<Guid> GetMembersIdByConferenceId(Guid id)
        {
            var list = _context.MembersOfConference.Where(s => s.ConferenceId == id).Select(s => s.UserId);
            return list; 
        }

        public void Update(Conference item)
        {
            var confer = _context.Conference.Find(item.Id);
            _context.Entry(confer).CurrentValues.SetValues(item);
            base.Save();
        }

        public MembersOfConference GetUserByIdMember(Guid id)
        {
            throw new NotImplementedException();
        }
        
        public IQueryable<Guid> GetConferIdList(Guid memberId)
        {
            var confIds = _context.MembersOfConference.Where(x => x.UserId == memberId).Select(x => x.ConferenceId);
            return confIds;
        }

        public MembersOfConference GetMembership(Guid confId, Guid membId)
        {
            var item = _context.MembersOfConference.SingleOrDefault(x=>x.UserId==membId && x.ConferenceId==confId);
            return item;
        }
    }
}
