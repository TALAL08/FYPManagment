using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FYPManagment.Models
{
    public class WorkProgress
    {
        public Group Group { get; set; }
        public Document Document { get; set; }
        public ApplicationUser SuperVisor { get; set; }
        public bool TaskAssign { get; set; }
        public bool TaskSubmit { get; set; }
        public bool Approve { get; set; }

        public bool Reject { get; set; }

        public bool OtherFile { get; set; }

        [Key]
        [Column(Order =1)]
        public int GroupId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int DocumentId { get; set; }
        public string SuperVisorId { get; set; }

    }
}