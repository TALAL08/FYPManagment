using FYPManagment.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace FYPManagment.Dtos
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public GroupDto Group { get; set; }
        public UserDto Supervisor { get; set; }
        public UserDto Sender { get; set; }
        public int GroupId { get; set; }
        public string SupervisorId { get; set; }
        public NotificationType Type { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime? MeetUpdateTime { get; set; }

        public int CountOfNewNotification { get; set; }


    }

    public class Notifications
    {
        public NotificationDto notificationDto { get; set; }

        public int CountOfNewNotification { get; set; }

    }
}