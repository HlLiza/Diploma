using System;

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
}