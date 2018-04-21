﻿using Network.Enums;
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
        public string AspUserId { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Введите свое имя")]
        public string Name { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Введите свою фамилию")]
        public string Surname { get; set; }

        [DisplayName("Skype")]
        public string Skype { get; set; }

        [DisplayName("Направление")]
        [Required(ErrorMessage = "Введите свое направление")]
        public string Direction { get; set; }

        [DisplayName("Университет")]
        public string University { get; set; }

        [DisplayName("Год начала обучения")]
        public int StartYear { get; set; }

        [DisplayName("Год окончания обучения (или предполагаемый)")]
        public int GradYear { get; set; }
    }

    public class UserListViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte[] Image { get; set; }
        public string Direction { get; set; }
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
        public string Role { get; set; }


        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Фамилия")]
        public string Surname { get; set; }

        [DisplayName("Направление")]
        public string Direction { get; set; }

        public byte[] Image { get; set; }

        [DisplayName("Номер телефона")]
        public string PhoneNumber { get; set; }

        [DisplayName("Skype")]
        public string Skype { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        public List<AducationInfo> Aducation { get; set; }

       

    }

    public class AducationInfo
    {
        [DisplayName("Образование")]
        [RegularExpression(@"[a-zA-Zа-яёА-ЯЁ\s\-]+$", ErrorMessage = "Некорректные данные")]
        public string University { get; set; }

        [DisplayName("Дата начала обучения")]
        [Range(1700, 2018, ErrorMessage = "Недопустимый год")]
        public int? StartYear { get; set; }

        [DisplayName("Дата окончания обучения")]
        [Range(1700, 2018, ErrorMessage = "Недопустимый год")]
        public int? GradYear { get; set; }

    }

    public class ChangePhotoViewModel
    {
        public Guid UserId { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }

    public class EditPersInfViewModel
    {
        //public byte[] Image { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Введите свое имя")]
        public string Name { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Введите свою фамилию")]
        public string Surname { get; set; }

        [DisplayName("Skype")]
        public string Skype { get; set; }

        [DisplayName("Направление")]
        [Required(ErrorMessage = "Введите свое направление")]
        public string Direction { get; set; }
    }
}
