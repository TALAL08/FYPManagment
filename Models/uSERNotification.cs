using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FYPManagment.Models
{
    public class UserNotification
    {


        public Notification Notification { get; set; }

        public ApplicationUser User { get; set; }

        [Key]
        [Column(Order = 1)]
        public int NotificationId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string UserId { get; set; }

        public bool IsRead { get; private set; }

        public UserNotification()
        {
        }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException();
            User = user;

            if (notification == null)
                throw new ArgumentNullException();
            Notification = notification;
        }

        public void Read()
        {
            IsRead = true;

        }
    }
}