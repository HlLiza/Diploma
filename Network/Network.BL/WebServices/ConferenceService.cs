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
                    if (item.Date < DateTime.Now && item.Visibility==true)
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

        public Conference GetConferenceById(Guid id)
        {
            if (id != null)
            {
                var conf = _conferRepository.Find(id);
                return conf;
            }
            else return null;
        }

        public IQueryable<Conference> GetArchiveConferenc()
        {
            return _conferRepository.GetArchiveConference();
        }

        //member
        public void AddMembersToConference(MembersOfConference member, ReportConference text)
        {
            if (member != null && text != null)
            {
                text.Id = Guid.NewGuid();
                _reportRepository.Add(text);
                member.Id = Guid.NewGuid();
                member.ReportId = text.Id;
                _conferRepository.JoinToConference(member);
             

            }
        }

        public MembersOfConference GetMembership(Guid confId, Guid userId)
        {
            return _conferRepository.GetMembershipByIds(confId,userId);
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
            var membership = _conferRepository.GetMemberships(confId);
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


        //report
        public ReportConference GetReport(Guid reportId)
        {
            return _reportRepository.Find(reportId);
        }

        public IQueryable<Guid> GetListReports(Guid confId)
        {
            return _reportRepository.GetListReportsId(confId);
        }












       
      

        
    }
}
