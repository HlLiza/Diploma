using Network.DAL.EFModel;
using System;

namespace Network.DAL.Interfaces
{
    public interface IUser_sPersonalData
    {
        void AddData(User_sPersonalData data);
        void Update(User_sPersonalData data);
        User_sPersonalData Find(Guid id);
    }
}
