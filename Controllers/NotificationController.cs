using FYPManagment.Models;
using FYPManagment.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYPManagment.Controllers
{
    public class NotificationController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public NotificationController()
        {
            _Context = new ApplicationDbContext();
        }
        // GET: Notification
        public ActionResult Index(int Id)
        {
            var userId = User.Identity.GetUserId();

            var notification = _Context.Notifications.Include(g => g.Group).SingleOrDefault(n => n.Id == Id && n.SupervisorId == userId );
            var document = _Context.Documents.Include(s => s.Sender).SingleOrDefault(d => d.SenderId == notification.SenderId && d.SuperVisorId == userId && d.NotificationId == notification.Id) ;
            var group = notification.Group;

            var ViewModel = new NotifiacationViewModel
            {
                Notification = notification,
                Document = document,
                Group = group
            };

            return View(ViewModel);
        }

        public ActionResult ReviewWork(int Id)
        {
            
            var groupMembers = _Context.GroupMembers.Include(g => g.Group).Include(u => u.Member).Where(g => g.GroupId == Id).ToList();
            var supervisorId = groupMembers.Single(s => s.MemberRole == MemberRole.SuperVisor).MemberId;
            var Group = groupMembers.FirstOrDefault(g => g.GroupId == Id).Group;

            var workProgress = _Context.WorkProgress.Include(d => d.Document).Where(d => d.GroupId == Id).ToList();

            var AssignWorks = workProgress.Where(wp => wp.Document.SenderId == supervisorId).ToList();
            var submitedWork = workProgress.Where(wp => wp.Document.SenderId != supervisorId).ToList();

            var ViewModel = new NewGroupViewModel
            {
                GroupMembers = groupMembers,
                Group = Group,
                SupervisorId = supervisorId,
                AssignWorks = AssignWorks,
                submitedWork = submitedWork

            };


            return View(ViewModel);
        }

        public ActionResult RequestForMeetUp(int Id)
        {
            var userId = User.Identity.GetUserId();

            var notification = _Context.Notifications.Include(g => g.Group).Include(s => s.Sender).SingleOrDefault(n => n.Id == Id && n.SupervisorId == userId);

            var ViewModel = new NotifiacationViewModel
            {
                Notification = notification,                
            };


            return View(ViewModel);
        }
    }
}