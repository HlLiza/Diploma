using Network.DAL.Interfaces;
using System;
using Network.DAL.EFModel;
using System.Linq;

namespace Network.DAL.Repositories
{
    public class AducationRepository:RepositoryBase, IAducation
    {
        public AducationRepository(InstitutNetworkContext context) : base(context) { }

        public void AddAducation(Aducation aducation)
        {
            _context.Aducation.Add(aducation);
            base.Save();
        }

        public Aducation Find(Guid ?id)
        {
            return _context.Aducation.Find(id);
        }

        public void UpdateAducation(Aducation aducation)
        {
            var item = Find(aducation.Id);
            _context.Entry(item).CurrentValues.SetValues(aducation);
            base.Save();
        }

        public IQueryable<Guid?> GetAducationIdListByUserId(Guid id)
        {
           return  _context.User_sPersonalData.Where(s => s.Id == id).Select(x => x.AducationId);
        }
    }
}

