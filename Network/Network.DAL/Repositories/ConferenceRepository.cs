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

        public MembersOfConference GetMembership(Guid confId)
        {
            return _context.MembersOfConference.FirstOrDefault(x => x.ConferenceId == confId);
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

        
    }
}

//public IQueryable<MembersOfConference> CheckMember(Guid userId, Guid conferenceId)
//{
//    return _context.MembersOfConference.Where(x => x.ConferenceId == conferenceId && x.UserId == userId);
//}



//public MembersOfConference GetUserMemberByIdMember(Guid id)
//{
//    var list = _context.MembersOfConference.FirstOrDefault(x => x.Id == id);
//    return list;
//}




//public Guid GetConferenceIdByUserId(Guid userId)
//{
//    var list = _context.MembersOfConference.FirstOrDefault(s => s.UserId == userId);
//    return list.ConferenceId;
//}



//public IQueryable<Guid> GetMembersIdByConferenceId(Guid id)
//{
//    var list = _context.MembersOfConference.Where(s => s.ConferenceId == id).Select(s => s.UserId);
//    return list; 
//}




//public IQueryable<Guid> GetConferIdList(Guid memberId)
//{
//    var confIds = _context.MembersOfConference.Where(x => x.UserId == memberId).Select(x => x.ConferenceId);
//    return confIds;
//}

//public MembersOfConference GetMembership(Guid confId, Guid membId)
//{
//    var item = _context.MembersOfConference.SingleOrDefault(x=>x.UserId==membId && x.ConferenceId==confId);
//    return item;
//}
