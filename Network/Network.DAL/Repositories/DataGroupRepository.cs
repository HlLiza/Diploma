using System;
using System.Linq;
using Network.DAL.EFModel;
using Network.DAL.Interfaces;

namespace Network.DAL.Repositories
{
    public class DataGroupRepository:RepositoryBase,IDataGroup
    {
        public DataGroupRepository(InstitutNetworkContext context) : base(context) { }

        public void Add(DataGroup data)
        {
            _context.DataGroup.Add(data);
            Save();
        }

        public IQueryable<DataGroup> DataForGroup(Guid groupId)
        {
            return _context.DataGroup.Where(x => x.GroupId == groupId);
        }

        public void Delete(DataGroup data)
        {
            _context.DataGroup.Remove(data);
            Save();
        }

        public DataGroup Find(Guid id)
        {
            return _context.DataGroup.Find(id);
        }
    }
}
