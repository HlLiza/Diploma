using Network.DAL.EFModel;
using System;
using System.Collections.Generic;
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

    public class SelectLeadViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }

    public class CreateGroup
    {
        [Required]
        public int Number { get; set; }

        [Required]
        public string Specialization { get; set; }

        [Required]
        public SelectLeadViewModel Head { get; set; }

        [Required]
        public List<SelectLeadViewModel> ListLead { get; set; }
    }

    public class AddtoGroup
    {
        public List<Group> groupList { get; set; }
        public Guid selectedGroupId { get; set; }
        public string selectedSpec { get; set; }
        public Guid userId { get; set; }
    }

    public class ConferenceViewModel
    {
        public Guid Id { get; set; }
        public string Thema { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public bool MembersStatus { get; set; }
    }

    public class AddToGroupMember
    {
        public Guid grId { get; set; }
        public List<UserListViewModel> ListUser { get; set; }
    }
}