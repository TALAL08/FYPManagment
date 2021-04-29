using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYPManagment.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public Group Group { get; private set; }
        public ApplicationUser Supervisor { get; private set; }
        public ApplicationUser Sender { get; private set; }                
        public int GroupId { get; private set; }

        public string SenderId { get; private set; }
        public string SupervisorId { get; private set; }

        public NotificationType Type { get; private set; }

        public DateTime DateTime { get; set; }

        public DateTime? MeetUpDateTime { get; set; }



        protected Notification()
        {
        }

        public Notification(NotificationType type, Group group, ApplicationUser supervisor, ApplicationUser sender, DateTime? MeetUpdateTime = null)
        {

            if (group == null)
                throw new ArgumentNullException();
            Group = group;

            if (supervisor == null)
                    throw new ArgumentNullException();
            Supervisor = supervisor;
            
            if (sender == null)
                throw new ArgumentNullException();
            Sender = sender;

            Type = type;
            DateTime = DateTime.Now;
            MeetUpDateTime = MeetUpdateTime;
        }

        
    }
}