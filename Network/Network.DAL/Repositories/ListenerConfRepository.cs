using Network.DAL.Interfaces;
using Network.DAL.EFModel;
using System;
using System.Linq;



namespace Network.DAL.Repositories
{
    public class ListenerConfRepository : RepositoryBase, IListenerConference
    {
        public ListenerConfRepository(NetworkContext context) : base(context) { }

        public void Add(ListenerConfer listener)
        {
            _context.ListenerConfer.Add(listener);
            Save();
        }

        public void Delete(ListenerConfer listener)
        {
            _context.ListenerConfer.Remove(listener);
            Save();
        }
       
        public IQueryable<Guid> GetListener(Guid confId)
        {
            return _context.ListenerConfer.Where(x => x.ConferenceId == confId).Select(r => r.UserId);
        }

        public ListenerConfer GetItem(Guid confId, Guid listenerId)
        {
            return _context.ListenerConfer.FirstOrDefault(x=>x.ConferenceId==confId && x.UserId==listenerId);
        }

         

    }
}
