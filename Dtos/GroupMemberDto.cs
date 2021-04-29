using FYPManagment.Models;

namespace FYPManagment.Dtos
{
    public class GroupMemberDto
    {
        public GroupDto Group { get; set; }

        public UserDto Member { get; set; }
        public int GroupId { get; set; }

        public string MemberId { get; set; }
        public MemberRole MemberRole { get; set; }
    }
}