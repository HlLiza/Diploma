using Network.DAL.EFModel;
using System;
using System.Linq;

namespace Network.BL.Interfaces
{
    public interface IAducationService
    {
        void AddAducation(Aducation adc);
        IQueryable<Aducation> GetAducation(Guid userId);
        void UpdateAducation(Aducation adc);
    }
}
