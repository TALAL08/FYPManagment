using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYPManagment.Models
{
    public class Document
    {       
        public int Id { get; set; }

        [Required]
        [MaxLength(75)]
        public string Name { get; set; }

        public Group Group { get; set; }

        public ApplicationUser Sender { get; set; }
        public ApplicationUser SuperVisor { get; set; }

        public Notification Notification { get; set; }

        public int NotificationId { get; set; }

        public int GroupId { get; set; }
        public string SenderId { get; set; }

        public string SuperVisorId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        public string ConcatinateName { get;  set; }
    }
}