using Network.DAL.EFModel;
using System;
using System.Linq;

namespace Network.DAL.Interfaces
{
    public interface IDataGroup
    {
        void Add(ResourceGroup data);
        void Delete(ResourceGroup data);
        ResourceGroup Find(Guid id);

        IQueryable<ResourceGroup> DataForGroup(Guid groupId);
    }
}
