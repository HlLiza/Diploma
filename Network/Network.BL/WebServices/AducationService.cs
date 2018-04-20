using System;
using System.Linq;
using Network.BL.Interfaces;
using Network.DAL.EFModel;
using Network.DAL.Interfaces;
using Network.DAL.Repositories;

namespace Network.BL.WebServices
{
    public class AducationService : IAducationService
    {
        private readonly IAducation _aducRepository;

        public AducationService(AducationRepository aducationRepository)
        {
            _aducRepository = aducationRepository;
        }

        public void AddAducation(Aducation adc)
        {
            adc.Id = Guid.NewGuid();
            _aducRepository.Add(adc);
        }

        public IQueryable<Aducation> GetAducation(Guid userId)
        {
           return _aducRepository.GetAdByUserId(userId);
        }

        public void UpdateAducation(Aducation adc)
        {
            _aducRepository.Update(adc);
        }
    }
}
