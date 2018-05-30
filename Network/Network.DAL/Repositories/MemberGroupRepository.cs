using Network.DAL.EFModel;
using Network.DAL.Interfaces;
using System;
using System.Linq;

namespace Network.DAL.Repositories
{
    public class MemberGroupRepository: RepositoryBase,IMemberGroup
    {
        public MemberGroupRepository(NetworkContext context) : base(context) { }

        public void Add(MemberOfGroup member)
        {
            _context.MemberOfGroup.Add(member);
            Save();
        }

        public void Delete(MemberOfGroup member)
        {
            _context.MemberOfGroup.Remove(member);
            Save();
        }

        public void Update(MemberOfGroup member)
        {
            var item = _context.MemberOfGroup.Find(member.Id);
            _context.Entry(item).CurrentValues.SetValues(member);
            Save();
        }

        public IQueryable<Guid> FindGroupsForUser(Guid userId)
        {
            return _context.MemberOfGroup.Where(x => x.UserId == userId).Select(d=>d.GroupId);
        }

        public IQueryable<Guid> FindUsersForGroup(Guid groupId)
        {
            return _context.MemberOfGroup.Where(x => x.GroupId == groupId).Select(d => d.UserId);
        }

        public bool UserIsMember(Guid userId, Guid groupId)
        {
            var item = _context.MemberOfGroup.FirstOrDefault(x => x.UserId ==userId && x.GroupId == groupId);
            if (item != null)
                return true;
            else return false;
        }
        public IQueryable<Guid> GetListMemberId()
        {
            return _context.MemberOfGroup.Select(x=>x.UserId).Distinct();
        }

    }
}
