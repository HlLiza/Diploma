using Network.DAL.EFModel;
using System;

namespace Network.DAL.Interfaces
{
    public interface IUser_sContact
    {
        void AddContact(User_sContact contact);
        void Update(User_sContact contact);
        User_sContact Find(Guid id);
    }
}
