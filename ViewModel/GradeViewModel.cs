using FYPManagment.Models;
using System.Collections.Generic;

namespace FYPManagment.ViewModel
{
    public class GradeViewModel
    {
        public Group group { get; set; }
        public IEnumerable<ApplicationUser> students { get; set; }
        public IEnumerable<GroupMember> members { get; set; }
        public GroupMember CurrentUser { get; set; }

    }
}