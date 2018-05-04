using Network.BL.Interfaces;
using Network.DAL.Interfaces;
using Network.DAL.Repositories;

namespace Network.BL.WebServices
{
    public class GroupService : IGroupService
    {
        private readonly IGroup _groupRepository;
        private readonly IUser _userRepository;
        private readonly IMemberGroup _memberRepository;
        private readonly IDataGroup _dataRepository;


        public GroupService(GroupRepository groupRepository, UserRepository userRepository,
            MemberGroupRepository memberRepository, DataGroupRepository dataRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _memberRepository = memberRepository;
            _dataRepository = dataRepository;
        }




    }
}





        
        //public void AddGroup(Group gr)
        //{
        //    if (gr != null)
        //    {
        //        gr.Id = Guid.NewGuid();
        //        _groupRepository.CreateGroup(gr);
        //    }
        //}

        //public void DeleteGroup(Guid id)
        //{
        //    if (id != null)
        //    {
        //        var gr = _groupRepository.GetGroupById(id);
        //        if (gr != null)
        //        {
        //            _groupRepository.DeleteGroup(gr);
        //        }                
        //    }
        //}

        //public void EditGroup(Guid id)
        //{
        //    if (id != null)
        //    {
        //        var gr = _groupRepository.GetGroupById(id);
        //        if (gr != null)
        //        {
        //            _groupRepository.EditGroup(gr);
        //        }
        //    }           
        //}

        //public Group Find(Guid id)
        //{
        //    if (id != null)
        //    {
        //        var gr = _groupRepository.GetGroupById(id);
        //        return gr;
        //    }
        //    else return null;
        //}

        //public List<Group> GetGroupsForHead(Guid headId)
        //{
        //    if (headId != null)
        //    {
        //        var groupIdList = _groupRepository.GetGroupsIdByHeadId(headId);
        //        if (groupIdList != null)
        //        {
        //            var groupList = GetGroupList(groupIdList);
        //            return groupList;
        //        }
        //    }
        //    return null;
        //}

        //public List<Group> GetGroupsForUser(Guid userId)
        //{
        //    if (userId != null)
        //    {
        //        var groupIdList = _groupRepository.GetGroupsIdByMemberId(userId); 
        //        if (groupIdList != null)
        //        {
        //            var groupList = GetGroupList(groupIdList);
        //            return groupList;
        //        }
        //    }
        //    return null;
        //}
       

        //public IQueryable<Guid> GetListOfId()
        //{
        //    var list = _groupRepository.GetAllGroups();
        //    return list;
        //}
        //public List<Group> GetGroupList(IQueryable<Guid> listId)
        //{
        //    List<Group> list = new List<Group>();
        //    if (listId != null)
        //    {
        //        foreach (var id in listId)
        //        {
        //            var item = _groupRepository.GetGroupById(id);
        //            list.Add(item);
        //        }
        //    }
        //    return list;         

        //}

        //public void AddMembersToGroup(MembersOfGroup member)
        //{
        //    if (member != null)
        //    {
        //        member.Id = Guid.NewGuid();
        //        _groupRepository.AddMembers(member);
        //    }
        //}

        //public void RemoveMembers(MembersOfGroup member)
        //{
        //    if (member != null)
        //    {
        //        _groupRepository.DeleteMembers(member.Id);
        //    }
        //}

        //public IQueryable<Guid> GetmembersListByGroupId(Guid id)
        //{
        //    if (id != null)
        //    {
        //        var gr = _groupRepository.GetGroupById(id);
        //        if (gr != null)
        //        {
        //            var listId = _groupRepository.GetMembersIdByGroup(gr.Id);
        //            return listId;
        //        }
        //    }
        //    return null;
        //}

        //public bool CheckMemberInGroup(Guid userId, Guid groupId)
        //{
        //    var members = _groupRepository.CheckMember(userId, groupId);
        //        if (members.Count()!=0)
        //            return false;
        //        else return true;
        //}

        //public bool CheckHeadGroup(string headId, Guid groupId)
        //{

        //    var group = _groupRepository.GetGroupById(groupId);
        //    var user = _userRepository.GetUserByAspUserId(headId);
        //   // var data = _dataRepository.Find(user.User_sPersonalData.Id);
        //    if (data.Id == group.HeadId)
        //        return true;
        //    else return false;
        //}


        //public List<MembersOfGroup> GetListMembersByListId(IQueryable<Guid> listId)
        //{
        //    List<MembersOfGroup> result = new List<MembersOfGroup>();
        //    if (listId != null)
        //    {
        //        foreach (var id in listId)
        //        {
        //            var item = _groupRepository.GetMembersById(id);
        //            result.Add(item);
        //        }
        //    }
        //    return result;
        //}

        //public void DeleteMembersInGroup(Guid groupId)
        //{
        //    if (groupId != null)
        //    {
        //        List<Guid> lidtMemId = new List<Guid>();
        //        var listMembersId = _groupRepository.GetMembersIdByGroup(groupId);
        //        if (listMembersId.Count() != 0)
        //        {
        //            foreach (var id in listMembersId)
        //            {
        //                var memberId = _groupRepository.GetGroupIdByMembersId(id);
        //                lidtMemId.Add(memberId);
        //            }
        //            foreach (var i in lidtMemId)
        //            {
        //                _groupRepository.DeleteMembers(i);
        //            }
                    
        //        }               
        //    }           
        //}

        //public void DeleteMemberInGroup(Guid groupId, Guid memberId)
        //{
        //    var membership = _groupRepository.GetMembersByGroupUser(groupId, memberId);
        //    if (membership != null)
        //    {
        //        _groupRepository.DeleteMembers(membership.Id);
        //    }

        //}


