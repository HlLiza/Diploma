using Network.DAL.EFModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Network.Views.ViewModels
{
    public class GroupViewModel
    {
        public Guid Id { get; set; }
        public int Number { get; set; }

        public string NameHead { get; set; }

        public string Specialization { get; set; }
    }


    public class AddGroup
    {
        public Guid Id { get; set; }

        public string NameProject { get; set; }

        public string Direction { get; set; }

        public IQueryable<User> Head { get; set; }
        public Guid SelectedHead { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateStart { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateFinish { get; set; }
    }

    public class IndexGroup
    {
        public Guid Id { get; set; }

        public string NameProject { get; set; }

        public string Direction { get; set; }

        public User Head { get; set; }

        public bool UserIsHead { get; set; }
    }
    
    public class GroupInfo
    {
        [DisplayName("НАПРАВЛЕНИЕ")]
        public string Direction { get; set; }

        [DisplayName("ПРОЕКТ")]
        public string NameProject { get; set; }

        [DisplayName("УЧАСТНИКИ")]
        public List<SimpleInfo> Members { get; set; }

        [DisplayName("РУКОВОЛИТЕЛЬ")]
        public SimpleInfo Head { get; set; }


        [DisplayName("ДАТА СТАРТА")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateStart { get; set; }

        [DisplayName("ДАТА ОКОНЧАНИЯ")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateFinish { get; set; }


    }

    public class OpenGroupViewModel
    {
        public Guid Id { get; set; }
        public string NameProject { get; set; }
        public string Direction { get; set; }
        public User Head { get; set; }


        [DisplayName("ДАТА СТАРТА")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateStart { get; set; }

        [DisplayName("ДАТА ОКОНЧАНИЯ")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateFinish { get; set; }

        [DisplayName("УЧАСТНИКИ")]
        public List<SimpleInfo> Members { get; set; }

        public bool UserIsHead { get; set; }

    }

    public class AddResource
    {
        public Guid GroupId { get; set; }
        public HttpPostedFileBase Resource { get; set; }
        public string Comments { get; set; }
    }

    public class ResourceListViewModel
    {
        public Guid ResourceId { get; set; }
        public string ResourceName { get; set; }
        public string Comments { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
    }

    public class GroupIdParam
    {
        public Guid GroupId { get; set; }
    }

    public class SelectMemberViewModel
    {
        public Guid Id { get; set; }

        public byte[] ImageUser { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
    }

    public class AddMemberViewModel
    {
        public Guid GroupId { get; set; }

        public string SelectedDirection { get; set; }

        public List<string> Directions { get; set; }

        public List<SelectMemberViewModel> SelectMember { get; set; }

    }

    public class SelectDirection
    {
        public Guid GroupId { get; set; }

        public string SelectedDirection { get; set; }
    }

}