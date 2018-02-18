using Network.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Network.Views.ViewModels
{
    public class UserViewModel
    {
    }

    public class AddUserViewModel
    {
        public Guid Id { get; set; }
        public string AspUserId { get; set; }
        public Roles TypeUser { get; set; }

        public string Name { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string PhoneNumber { get; set; }
        public string Skype { get; set; }
    }

    public class UserListViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
    }

    public class UserListOfGroupViewModel
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }

        public List<UserListViewModel> Members { get; set; }
    }

    public class UserIndexViewModel
    {
        public Guid Id { get; set; }
        public string AspUserId { get; set; }

        [DisplayName("Имя и фамилия")]
        [Required(ErrorMessage = "Введите свое имя")]
        public string Name { get; set; }
        public byte[] Image { get; set; }

        [DisplayName("Номер телефона")]
        public string PhoneNumber { get; set; }
        public string Skype { get; set; }

        [DisplayName("Образование")]
        [RegularExpression(@"[a-zA-Zа-яёА-ЯЁ\s\-]+$", ErrorMessage = "Некорректные данные")]
        public string Type { get; set; }

        [DisplayName("Учреждение")]
        [RegularExpression(@"[a-zA-Zа-яёА-ЯЁ\s\-]+$", ErrorMessage = "Некорректные данные")]
        public string Institution { get; set; }

        [DisplayName("Специальность")]
        [RegularExpression(@"[a-zA-Zа-яёА-ЯЁ\s\-]+$", ErrorMessage = "Некорректные данные")]
        public string Specialization { get; set; }

        [DisplayName("Дата начала")]
        //[Range(1700, 2018, ErrorMessage = "Недопустимый год")]
        public Nullable<System.DateTime> StartYear { get; set; }
        [DisplayName("Дата окончания")]
        //[Range(1700, 2018, ErrorMessage = "Недопустимый год")]
        public Nullable<System.DateTime> GradYear { get; set; }

    }
}
