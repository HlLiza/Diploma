using System;
using System.ComponentModel.DataAnnotations;
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
    
    public class ConferenceArchiveViewModel
    {
        public Guid Id { get; set; }
        public string Thema { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string Direction { get; set; }
        public byte[] Image { get; set; }
    }

    public class ConferenceAddViewModel
    {
        public string Thema { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Direction { get; set; }
        public string Details { get; set; }
        public Nullable<bool> Visibility { get; set; }
        public HttpPostedFileBase Requirements { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }

    public class GetMyReportViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public Guid ReportId { get; set; }
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

    public class OpenConferenceViewModel
    {
        public Guid Id { get; set; }
        public string Thema { get; set; }
        public string Direction { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public bool MemberConferenceStatus { get; set; }
        public string Details { get; set; }
        public byte[] Image { get; set; }
        public byte[] Requirements { get; set; }

    }

    public class ReportsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Guid ConfId { get; set; }
    }
}