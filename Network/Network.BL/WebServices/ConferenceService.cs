using Network.BL.Interfaces;
using Network.DAL.EFModel;
using Network.DAL.Interfaces;
using Network.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Network.BL.WebServices
{
    public class ConferenceService: IConferenceService
    {
        private readonly IConference _conferRepository;
        private readonly IListenerConference _listenerRepository;
        private readonly IReportConference _reportRepository;


        public ConferenceService() { }

        public ConferenceService(ConferenceRepository conferRepository, ListenerConfRepository listenerRepository,
            ReportConferenceRepository reportRepository)
        {
            _conferRepository = conferRepository;
            _listenerRepository = listenerRepository;
            _reportRepository = reportRepository;
        }

        
        //conference
       
        public IQueryable<Conference> GetConfIndex()
        {
            var list = _conferRepository.GetAll();
            var result = CheckVisibleConf(list);
            if (result != null)
                return result;
            return null;
        }

        public IQueryable<Conference> CheckVisibleConf(IQueryable<Conference> list)
        {
            if (list != null)
            {
                foreach (var item in list)
                    if (item.Date < DateTime.Now)
                    {
                        item.Visibility = false;
                        _conferRepository.Update(item);
                    }
                return _conferRepository.GetAll();
            }
            return null;          
        }

        public void AddCovference(Conference conf)
        {
            if (conf != null)
            {
                conf.Id = Guid.NewGuid();
                conf.Visibility = true;
                _conferRepository.Add(conf);
            }
        }

        //public Conference GetConferenceById(Guid id)
        //{
        //    if (id != null)
        //    {
        //        var conf = _conferRepository.Find(id);
        //        return conf;
        //    }
        //    else return null;

        //}
        //listener


        public void AddListener(Guid listenerId, Guid confId)
        {
            ListenerConfer listener = new ListenerConfer()
            {
                Id = Guid.NewGuid(),
                UserId = listenerId,
                ConferenceId = confId
            };
            _listenerRepository.Add(listener);
        }

        public void DeleteListener(Guid listenerId, Guid confId)
        {
            var item = _listenerRepository.GetItem(confId, listenerId);
            if (item != null)
            _listenerRepository.Delete(item);
        }

        public bool UserIsListener(Guid confId, Guid listId)
        {
            var item = _listenerRepository.GetItem(confId, listId);
            if (item != null && item.UserId == listId)
                return true;
            else return false;
        }

        public IQueryable<Guid> GetListIdListeners(Guid confId)
        {
            if (confId != Guid.Empty)
            {
                return _listenerRepository.GetListener(confId);
            }
            return null;
        }

        //member
        public void AddMembersToConference(MembersOfConference member, ReportConference text)
        {
            if (member != null && text != null)
            {
                member.Id = Guid.NewGuid();
                _conferRepository.JoinToConference(member);
                text.Id = Guid.NewGuid();
                _reportRepository.Add(text);

            }
        }

        public byte[] ConvertFile(HttpPostedFileBase file)
        {
            if (file != null)
            {
                byte[] result = new byte[file.ContentLength];
                file.InputStream.Read(result, 0, result.Length);
                return result;
            }
            return null;           
        }
        
        public bool UserIsMember(Guid confId, Guid userId)
        {
            var membership = _conferRepository.GetMembership(confId);
            if (membership != null && membership.UserId == userId)
                return true;
            else return false;
        }

        public IQueryable<Guid> GetMembersList(Guid confId)
        {
            if (confId != null)
            {
                var conf = _conferRepository.Find(confId);
                if (conf != null)
                {
                    var listId = _conferRepository.GetListMembersId(conf.Id);
                    return listId;
                }
            }
            return null;
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
        //public IQueryable<Guid> GetConferenceList()
        //{
        //    var list = _conferRepository.GetAll();
        //    return list;
        //}

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

        
        //public void RemoveMembers(MembersOfConference member)
        //{
        //    if (member != null)
        //    {
        //        _conferRepository.GoOut(member.Id);
        //    }
        //}


        
    


        //public IQueryable<Guid> GetMembersListByConferenceId(Guid id)
        //{
        //    if (id != null)
        //    {
        //        var conf = _conferRepository.Find(id);
        //        if (conf != null)
        //        {
        //            var listId = _conferRepository.GetMembersIdByConferenceId(conf.Id);
        //            return listId;
        //        }
        //    }
        //    return null;
        //}

        //public bool CheckMemberInConference(Guid userId, Guid confId)
        //{
        //    var members = _conferRepository.CheckMember(userId, confId);
        //    if (members.Count() != 0)
        //        return false;
        //    else return true;
        //}

        //public List<MembersOfConference> GetListMembersByListId(IQueryable<Guid> listId)
        //{
        //    List<MembersOfConference> result = new List<MembersOfConference>();
        //    if (listId != null)
        //    {
        //        foreach (var id in listId)
        //        {
        //            var item = _conferRepository.GetUserByIdMember(id);
        //            result.Add(item);
        //        }
        //    }
        //    return result;
        //}

        //public void DeleteMembersInConference(Guid confId)
        //{
        //    if (confId != null)
        //    {
        //        List<Guid> listMemId = new List<Guid>();
        //        var listMembersId = _conferRepository.GetMembersIdByConferenceId(confId);
        //        if (listMembersId.Count() != 0)
        //        {
        //            foreach (var id in listMembersId)
        //            {
        //                var memberId = _conferRepository.GetConferenceIdByUserId(id);
        //                listMemId.Add(memberId);
        //            }
        //            foreach (var i in listMemId)
        //            {
        //                _conferRepository.GoOut(i);
        //            }

        //        }
        //    }
        //}

        //public IQueryable<Guid> ListConferIdsByMemberId(Guid userId)
        //{
        //    if (userId != null)
        //    {
        //        return _conferRepository.GetConferIdList(userId);
        //    }
        //    return null;
        //}

        //public MembersOfConference GetMembership(Guid confId, Guid memId)
        //{
        //    var membership = _conferRepository.GetMembership(confId, memId);
        //    return membership;


        //}
    }
}
