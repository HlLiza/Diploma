using Network.DAL.Interfaces;
using System;
using Network.DAL.EFModel;
using System.Linq;

namespace Network.DAL.Repositories
{
    public class UserRepository :RepositoryBase, IUser
    {
        public UserRepository(InstitutNetworkContext  context) : base(context) { }

        public void AddUser(User item)
        {
            _context.User.Add(item);
            base.Save();
        }

        public User Find(Guid id)
        {
            return _context.User.Find(id);
        }

        public User_sPersonalData FindData(Guid id)
        {
            return _context.User_sPersonalData.Find(id);
        }

        public IQueryable<string> GetListOfIds()
        {
            _context.User.ToArray();
            return _context.User.Where(w=>w.Visibility==true).Select(w => w.AspUserId);
        }

        public void Update(User item)
        {
            var reserch = _context.User.Find(item.Id);
            _context.Entry(reserch).CurrentValues.SetValues(item);
            base.Save();
        }

        public User GetUserByAspUserId(string id)
        {
            var user = _context.User.First(s => s.AspUserId == id);
            return user;
        }

        public IQueryable<string> GetAllLeadId()
        {
            var aspNetUserId = _context.AspNetUserRoles.Where(x => x.RoleId == "1").Select(x=>x.UserId);
            //var listUser = _context.User.Select(x=>x.Id);
            return aspNetUserId;
        }

        public IQueryable<string> GetAllMemberId()
        {
            var aspNetUserId = _context.AspNetUserRoles.Where(x => x.RoleId == "2").Select(x => x.UserId);
            //var listUser = _context.User.Select(x => x.Id);
            return aspNetUserId;
        }

      
        public string GetRoleId(string userId)
        {
            var roleId = _context.AspNetUserRoles.FirstOrDefault(x => x.UserId == userId).ToString();
            return roleId;
        }

        public string GetRole(string id)
        {
            var role = _context.AspNetRoles.Find(id);
            return role.Name;
        }

        public User GetByPersDataId(Guid id)
        {
            var user = _context.User.FirstOrDefault(x => x.PersonalDataId == id);
            return user;
        }
    }
}
