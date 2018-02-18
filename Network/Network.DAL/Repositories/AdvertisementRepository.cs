using Network.DAL.EFModel;
using Network.DAL.Interfaces;
using System;
using System.Linq;

namespace Network.DAL.Repositories
{
    public class AdvertisementRepository:RepositoryBase, IAdvertisement
    {
        public AdvertisementRepository(InstitutNetworkContext context) : base(context) { }

        public void AddAdvertisement(Advertisement item)
        {
            _context.Advertisement.Add(item);
            base.Save();
        }

        public void DeleteAdvertisement(Guid id)
        {
            var item = Find(id);
            _context.Advertisement.Remove(item);
            base.Save();
        }

        public Advertisement Find(Guid id)
        {
            return _context.Advertisement.Find(id);
        }

        public void UpdateAdvertisement(Advertisement ad)
        {
            var item = _context.Advertisement.Find(ad.Id);
            _context.Entry(item).CurrentValues.SetValues(ad);
            base.Save();
        }

        public IQueryable<Guid> GetAdvertisement()
        {
            var list = _context.Advertisement.Select(x=>x.Id);
            return list;
        }
    }
}
