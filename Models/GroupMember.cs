using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FYPManagment.Models
{
    public class GroupMember
    {
        public Group Group { get; set; }

        public ApplicationUser Member { get; set; }

        [Key]
        [Column(Order = 1)]
        public int GroupId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string MemberId { get; set; }
        public MemberRole MemberRole { get; set; }

        public string PresentationGrade { get; set; }



    }


}