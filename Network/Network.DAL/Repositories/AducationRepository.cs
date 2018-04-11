using Network.DAL.Interfaces;
using System;
using Network.DAL.EFModel;
using System.Linq;

namespace Network.DAL.Repositories
{
    public class AducationRepository : RepositoryBase, IAducation
    {
        public AducationRepository(InstitutNetworkContext context) : base(context) { }

        public void Add(Aducation aducation)
        {
            _context.Aducation.Add(aducation);
            Save();
        }

        public Aducation Find(Guid id)
        {
            return _context.Aducation.Find(id);
        }

        public void Update(Aducation aducation)
        {
            var item = _context.Aducation.Find(aducation.Id);
            _context.Entry(item).CurrentValues.SetValues(aducation);
            Save();
        }

        public IQueryable<Guid> GetListOfId(Guid userId)
        {
            return _context.Aducation.Where(s => s.UserId == userId).Select(x => x.Id);
        }
   
    }
}

