using Network.DAL.Interfaces;
using Network.DAL.Repositories;
using System;
using Network.DAL.EFModel;
using System.Web;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Network.BL.WebServices
{
    public class UserService 
    {
        private readonly IUser _userRepository;
        private readonly IUser_sContact _contactRepository;
        private readonly IUser_sPersonalData _persDataRepository;
        private readonly IImage _imgRepository;
        private readonly IAducation _aducationRepository;


        public UserService(UserRepository researcherRepository,User_sContactRepository contactRepository,
            User_sPersonalDataRepository persDataRepository, ImageRepository imgRepository, AducationRepository aducationRepository)
        {
            _userRepository = researcherRepository;
            _contactRepository = contactRepository;
            _persDataRepository = persDataRepository;
            _imgRepository = imgRepository;
            _aducationRepository = aducationRepository;
        }

        public void AddImage(Image img)
        {
            if (img!=null)
            {
                img.Id = Guid.NewGuid();
                _imgRepository.AddImage(img);
            }
            
        }

        public void AddPersData(User_sPersonalData data)
        {
            if (data != null)
            {
                data.Id = Guid.NewGuid();
                _persDataRepository.AddData(data);
            }
        }

        public void AddContact(User_sContact contact)
        {
            if (contact != null)
            {
                contact.Id = Guid.NewGuid();
                _contactRepository.AddContact(contact);
            }
               
        }

        public void AddUser(User user)
        {
            if (user != null)
            {
                user.Id = Guid.NewGuid();
                _userRepository.AddUser(user);
            }

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

        public IQueryable<string> GetAllUsersId()
        {
            var list = _userRepository.GetListOfIds();
            return list;
          
        }

        public IQueryable<string> GetAllLeadListId()
        {
            var list = _userRepository.GetAllLeadId();
            if (list != null)
                return list;
            else return null;
        }

        public IQueryable<string> GetAllMemberListId()
        {
            var list = _userRepository.GetAllMemberId();
            if (list != null)
                return list;
            else return null;
        }

        public User_sPersonalData GetDataByAspUserId(string id)
        {
            if (id != null)
            {
                var user = _userRepository.GetUserByAspUserId(id);
                if (user != null)
                {
                    var data = _persDataRepository.Find(user.PersonalDataId);
                    return data;
                }
            }
            return null;
        }

        public List<User_sPersonalData> GetDataForListOfUser(IQueryable<Guid> list)
        {
            List<User_sPersonalData> result = new List<User_sPersonalData>();

            if (list != null)
            {
                foreach (var id in list)
                {
                    var itemUser = _userRepository.Find(id);
                    var itemPersonalData = _persDataRepository.Find(itemUser.PersonalDataId);
                    result.Add(itemPersonalData);
                }
            }
            return result;
        }

        public List<User_sPersonalData> GetDataForListOfUser(List<Guid> list)
        {
            List<User_sPersonalData> result = new List<User_sPersonalData>();

            if (list != null)
            {
                foreach (var id in list)
                {
                    var itemUser = _userRepository.Find(id);
                    var itemPersonalData = _persDataRepository.Find(itemUser.PersonalDataId);
                    result.Add(itemPersonalData);
                }
            }
            return result;
        }

            public List<User_sPersonalData> GetDataForListOfUserByAspId(IQueryable<string> list)
        {
            List<User_sPersonalData> result = new List<User_sPersonalData>();

            if (list != null)
            {
                foreach (var id in list)
                {
                    var itemUser = _userRepository.GetUserByAspUserId(id);
                    var itemPersonalData = _persDataRepository.Find(itemUser.PersonalDataId);
                    result.Add(itemPersonalData);
                }
            }
            return result;
        }



        public byte[] GetImageByDataId(Guid id)
        {
            if (id != null)
            {
                var data = _persDataRepository.Find(id);
                if (data != null)
                {
                    var img=_imgRepository.FindImg(data.ImageId);
                    return img.Image1;
                }
            }
            return null;
        }

        public void AddAducation(Aducation aducation)
        {
            if (aducation != null)
            {
                aducation.Id = Guid.NewGuid();
                _aducationRepository.AddAducation(aducation);
            }
        }

        public User GetUserById(Guid id)
        {
            if (id != null)
            {
                var user = _userRepository.Find(id);
                return user;
            }
            else return null;
        }

        public User_sPersonalData GetUserPersData(Guid id)
        {
            if (id != null)
            {
                var user = _userRepository.FindData(id);
                return user;
            }
            else return null;
        }

        public List<User_sPersonalData> GetLeadAll()
        {
            var listId=_userRepository.GetAllLeadId();
            if (listId != null)
            {
                var listUser = GetDataForListOfUserByAspId(listId);
                return listUser;
            }
            else return null;        
        }

        public List<User_sPersonalData> GetGroupMemberAll()
        {
            var listId = _userRepository.GetAllMemberId();
            if (listId != null)
            {
                var listUser = GetDataForListOfUserByAspId(listId);
                return listUser;
            }
            else return null;

        }

        public User GetUserByAspNetId(string id)
        {
            if (id != null)
            {
                var user = _userRepository.GetUserByAspUserId(id);
                return user;
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
                var roleId = _userRepository.GetRoleId(id);
                 return roleId;
            }
            return null;
        }

        public User_sContact GetContactById(Guid id)
        {
            
            if (id != null)
            {
                var contact = _contactRepository.Find(id);
                return contact;
            }
            else return null;
        }

        public Aducation GetAducationInf(Guid id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                var persData = _persDataRepository.Find(user.PersonalDataId);
                if (persData.AducationId != null)
                {
                    var aducation = _aducationRepository.Find(persData.AducationId);
                    return aducation;
                }               
              
            }
            return null;
        }

        public void EditProfile(Aducation aducation, User_sContact contact, User_sPersonalData data, User user)
        {
            if (user != null)
            {
                var us = GetUserById(user.Id);
                var persData = GetUserPersData(us.PersonalDataId);
                var cont = GetContactById(us.ContactId);

                if (cont.Id != null)
                {
                    contact.Id = cont.Id;
                    _contactRepository.Update(contact);
                }
                else {
                    contact.Id = Guid.NewGuid();
                    _contactRepository.AddContact(contact);
                    us.ContactId = contact.Id;
                }



                if (persData.AducationId != null)
                {
                    aducation.Id = (Guid)persData.AducationId;
                    _aducationRepository.UpdateAducation(aducation);
                }
                else {
                    aducation.Id = Guid.NewGuid();
                    _aducationRepository.AddAducation(aducation);
                    persData.AducationId = aducation.Id;
                }

                data.Id = persData.Id;
                data.ImageId = persData.ImageId;
                data.AducationId = aducation.Id;
                _persDataRepository.Update(data);

                user.Id = us.Id;
                user.PersonalDataId = data.Id;
                user.ContactId = contact.Id;
                _userRepository.Update(user);           
            }
                       

        }

        public string GetRoleNameByAspId(string id)
        {
            var item = _userRepository.GetRole(id);
            return item;
        }

        public string GetRoleNameForUser(string id)
        {
            var roleId = _userRepository.GetRoleId(id);
            var item = _userRepository.GetRole(roleId);
            return item;
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

            foreach (var item in stringId)
            {
                var user = GetUserByAspNetId(item);
                list.Add(user.Id);
            }

            return list;
        }

        public User GetUserByUserPersDataId(Guid persDataId)
        {
            var user=_userRepository.GetByPersDataId(persDataId);
            return user;
        }
    }
}
