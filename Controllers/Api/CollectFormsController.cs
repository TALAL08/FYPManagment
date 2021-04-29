using FYPManagment.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FYPManagment.Controllers.Api
{
    public class CollectFormsController : ApiController
    {
        private readonly ApplicationDbContext _Context;

        public CollectFormsController()
        {
            _Context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CollectForms(int Id)
        {
            var userId = User.Identity.GetUserId();
            var SubmitForm = _Context.submitForms
                .Include(g => g.Group)
                .Include(gm => gm.Group.GroupMembers)
                .Include(gm => gm.Group.GroupMembers.Select(m => m.Member))
                .Include(s => s.Student)
                .Single(sf => sf.Id == Id);

            SubmitForm.IsCollected = true;
            var group = SubmitForm.Group;
            var student = SubmitForm.Student;
            var supervisor = group.GroupMembers.Single(sv => sv.MemberRole == MemberRole.SuperVisor).Member;
            var coordinator = group.GroupMembers.Single(sv => sv.MemberRole == MemberRole.CoOrdinator && sv.MemberId == userId).Member;

            var notification = new Notification(NotificationType.FormCollect,group, supervisor, coordinator);
            student.Notify(notification);

            _Context.SaveChanges();
            return Ok();
        }
    }
}
