using System;
using System.Linq;
using Network.DAL.EFModel;
using Network.DAL.Interfaces;

namespace Network.DAL.Repositories
{
    public class DataGroupRepository:RepositoryBase,IDataGroup
    {
        public DataGroupRepository(InstitutNetworkContext context) : base(context) { }

        public void Add(ResourceGroup data)
        {
            _context.ResourceGroup.Add(data);
            Save();
        }

        public IQueryable<ResourceGroup> DataForGroup(Guid groupId)
        {
            return _context.ResourceGroup.Where(x => x.GroupId == groupId);
        }

        public void Delete(ResourceGroup data)
        {
            _context.ResourceGroup.Remove(data);
            Save();
        }

        public ResourceGroup Find(Guid id)
        {
            return _context.ResourceGroup.Find(id);
        }
    }
}
