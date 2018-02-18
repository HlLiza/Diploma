using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Network.Views.ViewModels
{
    public class AdvertismentViewModel
    {
        public Guid Id { get; set; }
        public string Information { get; set; }
        public DateTime Date { get; set; }
    }
}