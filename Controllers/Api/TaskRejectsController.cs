using FYPManagment.Models;
using FYPManagment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Web.Http;

namespace FYPManagment.Controllers.Api
{
    public class TaskRejectsController : ApiController
    {
        private readonly ApplicationDbContext _Context;

        public TaskRejectsController()
        {
            _Context = new ApplicationDbContext();
        }

        [HttpPost]

        public IHttpActionResult RejectWork(ReviewWork Ids)
        {

            var GroupId = Ids.GroupId;
            var DocumentId = Ids.DocumentId;

            var workProgress = _Context.WorkProgress.Include(sv => sv.SuperVisor).Include(g => g.Group).Include(gm => gm.Group.GroupMembers.Select(m => m.Member)).Single(wp => wp.GroupId == GroupId && wp.DocumentId == DocumentId);

            workProgress.Reject = true;

            var group = workProgress.Group;
            var supervisor = workProgress.SuperVisor;
            group.TaskReject(supervisor, supervisor);

            _Context.SaveChanges();

            return Ok();
        }
    }
}
