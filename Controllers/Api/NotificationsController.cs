using AutoMapper;
using FYPManagment.Dtos;
using FYPManagment.Models;
using FYPManagment.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace FYPManagment.Controllers.Api
{
    public class NotificationsController : ApiController
    {
        private readonly ApplicationDbContext _Context;

        public NotificationsController()
        {
            _Context = new ApplicationDbContext();
        }



        public IEnumerable<Notifications> GetNewNotifications()
        {
            var UserId = User.Identity.GetUserId();

            var usernotifications = _Context.UserNotifications
            .Where(un => un.UserId == UserId);
           
       
            var  notifications =
            usernotifications
                .Select(un => un.Notification)
            .Include(n => n.Group)
            .Include(sv => sv.Supervisor)
            .Include(s => s.Sender)
            .ToList().OrderByDescending(n => n.DateTime);
            
            var count = usernotifications.Count(n => !n.IsRead);

            return notifications.Select(n => new Notifications()
            {
                notificationDto = new NotificationDto()
                {
                    Id = n.Id,
                    Group = new GroupDto
                    {
                        Id = n.GroupId,
                        Name = n.Group.Name,
                        PresentationDate = n.Group.PresentationDate
                    },
                    Supervisor = new UserDto
                    {
                        FirstName = n.Supervisor.FirstName
                    },
                    Sender = new UserDto
                    {
                        FirstName = n.Sender.FirstName
                    },

                    Type = n.Type,
                    DateTime = n.DateTime,
                    MeetUpdateTime = n.MeetUpDateTime
                },
               
                CountOfNewNotification = count               
            }
            );
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var UserId = User.Identity.GetUserId();

            var notifications = _Context.UserNotifications
            .Where(un => un.UserId == UserId && !un.IsRead)
            .ToList();

            notifications.ForEach(n => n.Read());

            _Context.SaveChanges();

            return Ok();

        }




    }
}
