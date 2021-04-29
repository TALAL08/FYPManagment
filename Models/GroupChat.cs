using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYPManagment.Models
{
    public class GroupChat
    {
        public int Id { get; set; }

        public Group Group { get; set; }

        public int GroupId { get; set; }

        public string Message { get; set; }

        public ApplicationUser Sender { get; set; }

        public string SenderId { get; set; }

        public DateTime DateTime { get; set; }

    }
}