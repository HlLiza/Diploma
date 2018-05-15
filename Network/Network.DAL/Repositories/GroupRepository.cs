using Network.DAL.Interfaces;
using System;
using Network.DAL.EFModel;
using System.Linq;

namespace Network.DAL.Repositories
{
    public class GroupRepository : RepositoryBase, IGroup
    {
        public GroupRepository(InstitutNetworkContext context) : base(context) { }   


        public void Add(Group group)
        {
            _context.Group.Add(group);
            Save();
        }

        public void Delete(Group group)
        {
            _context.Group.Remove(group);
            Save();
        }

        public void Update(Group group)
        {
            var item = _context.Group.Find(group.Id);
            _context.Entry(item).CurrentValues.SetValues(group);
            Save();
        }

        public Group Find(Guid id)
        {
            return _context.Group.Find(id);
        }

        public IQueryable<Group> GetGroups()
        {
            return _context.Group;
        }

        public IQueryable<Group> GetGroupsByHead(Guid headId)
        {
            return _context.Group.Where(s => s.HeadId == headId);
        }

        public IQueryable<Guid> AllLeadId()
        {
            return _context.Group.Select(x => x.HeadId).Distinct();
        }

       
    }
}
