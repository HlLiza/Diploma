using Network.DAL.EFModel;
using Network.DAL.Interfaces;
using System;

namespace Network.DAL.Repositories
{
    public class User_sPersonalDataRepository : RepositoryBase, IUser_sPersonalData
    {
        public User_sPersonalDataRepository() { }

        public User_sPersonalDataRepository(InstitutNetworkContext context) : base(context) { }

        public void AddData(EFModel.User_sPersonalData data)
        {
            _context.User_sPersonalData.Add(data);
            base.Save();
        }

        public EFModel.User_sPersonalData Find(Guid id)
        {
            return _context.User_sPersonalData.Find(id);
        }

        public void Update(EFModel.User_sPersonalData data)
        {
            var dt = _context.User_sPersonalData.Find(data.Id);
            _context.Entry(dt).CurrentValues.SetValues(data);
            base.Save();
        }
    }
}
