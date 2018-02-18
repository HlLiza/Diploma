using Network.DAL.EFModel;
using Network.DAL.Interfaces;
using Network.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Network.BL.WebServices
{
    public class ConferenceService
    {
        private readonly IConference _conferRepository;

        public ConferenceService() { }

        public ConferenceService(ConferenceRepository conferRepository)
        {
            _conferRepository = conferRepository;
        }

        public void AddCovference(Conference conf)
        {
            if (conf != null)
            {
                conf.Id = Guid.NewGuid();
                _conferRepository.AddConference(conf);
            }
        }

        public Conference GetConferenceById(Guid id)
        {
            if (id != null)
            {
                var conf = _conferRepository.Find(id);
                return conf;
            }
            else return null;

        }
        public IQueryable<Guid> GetConferenceList()
        {
            var list = _conferRepository.GetAllConferencesId();
            return list;
        }

        public List<Conference> GetConferList(IQueryable<Guid> listId)
        {
            List<Conference> list = new List<Conference>();
            if (listId != null)
            {
                foreach (var id in listId)
                {
                    var item = _conferRepository.Find(id);
                    list.Add(item);
                }
            }
            return list;
        }

        public void AddMembersToConference(MembersOfConference member)
        {
            if (member != null)
            {
                member.Id = Guid.NewGuid();
                _conferRepository.AddMembers(member);
            }
        }

        public void RemoveMembers(MembersOfConference member)
        {
            if (member != null)
            {
                _conferRepository.DeleteMembers(member.Id);
            }
        }

        public IQueryable<Guid> GetMembersListByConferenceId(Guid id)
        {
            if (id != null)
            {
                var conf = _conferRepository.Find(id);
                if (conf != null)
                {
                    var listId = _conferRepository.GetMembersIdByConferenceId(conf.Id);
                    return listId;
                }
            }
            return null;
        }

        public bool CheckMemberInConference(Guid userId, Guid confId)
        {
            var members = _conferRepository.CheckMember(userId, confId);
            if (members.Count() != 0)
                return false;
            else return true;
        }

        public List<MembersOfConference> GetListMembersByListId(IQueryable<Guid> listId)
        {
            List<MembersOfConference> result = new List<MembersOfConference>();
            if (listId != null)
            {
                foreach (var id in listId)
                {
                    var item = _conferRepository.GetUserByIdMember(id);
                    result.Add(item);
                }
            }
            return result;
        }

        public void DeleteMembersInConference(Guid confId)
        {
            if (confId != null)
            {
                List<Guid> listMemId = new List<Guid>();
                var listMembersId = _conferRepository.GetMembersIdByConferenceId(confId);
                if (listMembersId.Count() != 0)
                {
                    foreach (var id in listMembersId)
                    {
                        var memberId = _conferRepository.GetConferenceIdByUserId(id);
                        listMemId.Add(memberId);
                    }
                    foreach (var i in listMemId)
                    {
                        _conferRepository.DeleteMembers(i);
                    }

                }
            }
        }

        public IQueryable<Guid> ListConferIdsByMemberId(Guid userId)
        {
            if (userId != null)
            {
                return _conferRepository.GetConferIdList(userId);
            }
            return null;
        }

        public MembersOfConference GetMembership(Guid confId, Guid memId)
        {
            var membership = _conferRepository.GetMembership(confId, memId);
            return membership;


        }
    }
}
