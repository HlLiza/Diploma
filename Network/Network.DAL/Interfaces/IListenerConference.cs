using Network.DAL.EFModel;
using System;
using System.Linq;

namespace Network.DAL.Interfaces
{
    public interface IListenerConference
    {
        void Add(ListenerConfer listener);
        void Delete(ListenerConfer listener);

        IQueryable<Guid> GetListener(Guid confId);
        ListenerConfer GetItem(Guid confId, Guid listenerId);

    }
}
