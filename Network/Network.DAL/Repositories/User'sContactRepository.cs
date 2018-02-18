using Network.DAL.Interfaces;
using Network.DAL.EFModel;
using System;

namespace Network.DAL.Repositories
{
    public class User_sContactRepository : RepositoryBase, IUser_sContact
    {
        public User_sContactRepository() { }

        public User_sContactRepository(InstitutNetworkContext context):base(context) { }

        public void AddContact(User_sContact contact)
        {
            _context.User_sContact.Add(contact);
            base.Save();
        }

        public User_sContact Find(Guid id)
        {
            return _context.User_sContact.Find(id);
        }

        public void Update(User_sContact contact)
        {
            //_context.User_sContact.Attach(contact);
            //_context.Entry(contact).State = System.Data.Entity.EntityState.Modified;
            var cont = _context.User_sContact.Find(contact.Id);
            _context.Entry(cont).CurrentValues.SetValues(contact);
            base.Save();
        }
    }
}
