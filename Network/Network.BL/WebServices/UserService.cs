using Network.DAL.Interfaces;
using Network.DAL.Repositories;
using System;
using Network.DAL.EFModel;
using System.Web;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Network.BL.Interfaces;

namespace Network.BL.WebServices
{
    public class UserService : IUserService
    {
        private readonly IUser _userRepository;
        private readonly IGroup _groupRepository;


        public UserService(UserRepository userRepository, GroupRepository groupRepository)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        public void AddUser(User user)
        {
            if (user != null)
            {
                user.Id = Guid.NewGuid();
                _userRepository.AddUser(user);
            }

        }

        public Guid GetIdByAspId(string id)
        {
            var user = _userRepository.GetUserByAspUserId(id);
            return user.Id;

        }


        public byte[] СonvertingImg(HttpPostedFileBase img)
        {
            byte[] imageData = null;

            if (img != null)
            {
                using (var binaryReader = new BinaryReader(img.InputStream))
                {
                    imageData = binaryReader.ReadBytes(img.ContentLength);
                }
            }
            return imageData;
        }



        public void UpdateUser(User user)
        {
            if (user != null)
            {
                _userRepository.Update(user);
            }
        }



        public List<User> GetUsersByListId(List<Guid> listId)
        {
            List<User> result = new List<User>();

            if (listId != null)
            {
                foreach (var id in listId)
                {
                    var user = _userRepository.Find(id);
                    result.Add(user);
                }
            }
            return result;
        }

        //public List<User> GetUsersByListId(List<Guid> listId)
        //{
        //    List<User> result = new List<User>();

        //    if (listId.Count()>)
        //    {
        //        foreach (var id in listId)
        //        {
        //            var user = _userRepository.Find(id);
        //            result.Add(user);
        //        }
        //    }
        //    return result;
        //}


        public IQueryable<User> GetUsersByDirection(string direction)
        {
            return _userRepository.GetByDirection(direction);
        }

        public AspNetUsers GetAspUser(string id)
        {
            return _userRepository.GetAspUser(id);
        }

        public IQueryable<Guid> AllUsersId()
        {
            return _userRepository.AllUsersId();
        }




















        public List<string> GetAllLeadListId()
        {
            List<string> result = new List<string>();
            var list = _groupRepository.AllLeadId();
            if (list != null)
            {
                foreach (var item in list)
                {
                    var user = _userRepository.Find(item);
                    result.Add(user.AspUserId);
                }
                return result;
            }
            else return result;
        }



        //public List<string> GetAllMemberListId()
        //{
        //    List<string> result = new List<string>();
        //    var list = _userRepository.GetAllMemberId();
        //    if (list != null)
        //    {
        //        foreach (var item in list)
        //        {
        //            var user = _userRepository.Find(item);

        //        }
        //    }
        //    if (list != null)
        //        return list;
        //    else return null;
        //}

        public User GetUserById(Guid id)
        {
            if (id != null)
            {
                return _userRepository.Find(id);
            }
            else return null;
        }



        public User GetUserByAspNetId(string id)
        {
            if (id != null)
            {
                return _userRepository.GetUserByAspUserId(id);
            }
            else return null;
        }

        public Guid GetUserIdByAspId(string id)
        {
            if (id != null)
            {
                var user = _userRepository.GetUserByAspUserId(id);
                return user.Id;
            }
            else return Guid.Empty;
        }

        public string GetTypeUser(string id)
        {
            if (id != null)
            {
                return _userRepository.GetRoleId(id);
            }
            return null;
        }

        public string GetRoleNameByAspId(string id)
        {
            if (id.Length != 0)
            {
                return _userRepository.GetRole(id);
            }
            return null;
        }

        public string GetRoleNameForUser(string id)
        {
            if (id.Length != 0)
            {
                var roleId = _userRepository.GetRoleId(id);
                return _userRepository.GetRole(roleId);
            }
            return null;
        }

        public List<Guid> ExcludeListIdInLIs(List<Guid> listIdsUsers, List<Guid> listIdGroupMem)
        {
            var dif = listIdsUsers.Where(x => !listIdGroupMem.Contains(x));
            var result = listIdsUsers.Where(x => dif.Contains(x)).ToList();

            return result;
        }

        public List<Guid> GetUserIdForListAspId(IQueryable<string> stringId)
        {
            List<Guid> list = new List<Guid>();

            if (stringId != null)
            {
                foreach (var item in stringId)
                {
                    var user = GetUserByAspNetId(item);
                    list.Add(user.Id);
                }
            }
            return list;
        }


        public IQueryable<User> GetAllUser()
        {
            return _userRepository.GetAll();
        }

        public List<User> GetUsersByListId(IQueryable<string> listId)
        {
            //listId is id of AspNetUsers

            List<User> result = new List<User>();

            if (listId != null)
            {
                foreach (var id in listId)
                {
                    var user = _userRepository.GetUserByAspUserId(id);
                    result.Add(user);
                }
            }
            return result;
        }
    }
}












//public byte[] GetImageByDataId(Guid id)
//{
//    if (id != null)
//    {
//        var data = _persDataRepository.Find(id);
//        if (data != null)
//        {
//            var img=_imgRepository.FindImg(data.ImageId);
//            return img.Image1;
//        }
//    }
//    return null;
//}

//public void AddAducation(Aducation aducation)
//{
//    if (aducation != null)
//    {
//        aducation.Id = Guid.NewGuid();
//        _aducationRepository.AddAducation(aducation);
//    }
//}

//public IQueryable<string> GetAllId()
//{
//    return _userRepository.GetListOfIds();
//}



