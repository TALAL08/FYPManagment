using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYPManagment.Models
{
    public class MeetingAndPresentationDateTime
    {
        public int Id { get; set; }

        public Group Group { get; set; }

        public int GroupId { get; set; }

        public DateTime? MeetingDateTime { get; set; }

        public DateTime? PresentationDateTime { get; set; }

    }
}