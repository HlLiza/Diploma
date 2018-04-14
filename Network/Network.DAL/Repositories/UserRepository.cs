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
            Save();
        }

        public void Update(User item)
        {
            var reserch = _context.User.Find(item.Id);
            _context.Entry(reserch).CurrentValues.SetValues(item);
            Save();
        }

        public User Find(Guid id)
        {
            return _context.User.Find(id);
        }

        public User GetUserByAspUserId(string id)
        {
            return _context.User.FirstOrDefault(s => s.AspUserId == id);
        }

        public IQueryable<User> GetAll()
        {
            return _context.User;
        }




        public IQueryable<string> GetAllLeadId()
        {
            return _context.AspNetUserRoles.Where(x => x.RoleId == "1").Select(x => x.UserId);
        }

        public IQueryable<string> GetAllMemberId()
        {
            return _context.AspNetUserRoles.Where(x => x.RoleId == "2").Select(x => x.UserId);
        }

        //public IQueryable<Guid> GetListOfIds()
        //{
        //    return _context.User.Where(w => w.Visibility == true).Select(x => x.Id);
        //}



        public string GetRole(string id)
        {
            var item = _context.AspNetUserRoles.First(x=>x.UserId == id);
            var role = _context.AspNetRoles.FirstOrDefault(x => x.Id == item.RoleId);
            return role.Name;
        }

        public string GetRoleId(string userId)
        {
            return _context.AspNetUserRoles.FirstOrDefault(x => x.UserId == userId).ToString();
        }

        public AspNetUsers GetAspUser(string id)
        {
            return _context.AspNetUsers.Find(id);
        }
    }
}
