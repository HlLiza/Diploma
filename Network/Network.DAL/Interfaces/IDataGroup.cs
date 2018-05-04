using Network.DAL.EFModel;
using System;
using System.Linq;

namespace Network.DAL.Interfaces
{
    public interface IDataGroup
    {
        void Add(DataGroup data);
        void Delete(DataGroup data);
        DataGroup Find(Guid id);

        IQueryable<DataGroup> DataForGroup(Guid groupId);
    }
}
