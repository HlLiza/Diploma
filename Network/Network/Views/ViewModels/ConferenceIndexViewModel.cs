using System;
using System.Web;

namespace Network.Views.ViewModels
{
    public class ConferenceIndexViewModel
    {
        public Guid Id { get; set; }
        public string Thema { get; set; }
        public DateTime Date { get; set; }
        public bool MemberConferenceStatus { get; set; }
        public bool ListenerConferenceStatus { get; set; }
    }

    public class UserAtConference
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Direction { get; set; }
        public byte[] Image { get; set; }
    }

    public class RegistrToConfer
    {
        public Guid ConfId { get; set; }
        public Guid UserId { get; set; }
        public string Topic { get; set; }
        public string Author { get; set; }
        public HttpPostedFileBase ReportText { get; set; }
    }
    
}