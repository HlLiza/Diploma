using Network.DAL.EFModel;
using System;
using System.Linq;

namespace Network.DAL.Interfaces
{
    public interface IAducation
    {
        void Add(Aducation aducation);
        void Update(Aducation aducation);
        Aducation Find(Guid id);
        IQueryable<Guid> GetListOfId(Guid userId);
    }
}
