using Network.DAL.EFModel;
using Network.DAL.Interfaces;
using Network.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Network.BL.WebServices
{
    public class AdvertisementService
    {
        private readonly IAdvertisement _advertisementRepository;
        private readonly IUser _userRepository;

        public AdvertisementService(UserRepository userRepository, AdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
            _userRepository = userRepository;
        }

        public void AddAdvert(Advertisement adv)
        {
            if (adv != null)
            {
                adv.Id = Guid.NewGuid();
                adv.Date = DateTime.Now;
                _advertisementRepository.AddAdvertisement(adv);
            }
        }

        public List<Advertisement> GetAllAdvertisment()
        {
            List<Advertisement> listAdv = new List<Advertisement>();
            var listId = _advertisementRepository.GetAdvertisement();
            if (listId != null)
            {
                foreach (var id in listId)
                {
                    Advertisement item = _advertisementRepository.Find(id);
                    listAdv.Add(item);
                }
                return listAdv;
            }
            return null;
        }
    }
}
