using System;
using System.Linq;
using Network.DAL.EFModel;
using Network.DAL.Interfaces;
using System.Collections.Generic;

namespace Network.DAL.Repositories
{
    public class DataGroupRepository:RepositoryBase,IDataGroup
    {
        public DataGroupRepository(NetworkContext context) : base(context) { }

        public void Add(ResourceGroup data)
        {
            _context.ResourceGroup.Add(data);
            Save();
        }

        public List<ResourceGroup> DataForGroup(Guid groupId)
        {
            var res=_context.ResourceGroup.Where(x => x.GroupId == groupId).OrderByDescending(x=>x.Date);
            return res.ToList();
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
