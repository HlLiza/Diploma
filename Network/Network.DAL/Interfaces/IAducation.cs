using Network.DAL.EFModel;
using System;

namespace Network.DAL.Interfaces
{
    public interface IAducation
    {
        void AddAducation(Aducation aducation);
        void UpdateAducation(Aducation aducation);
        Aducation Find(Guid ?id);
    }
}
