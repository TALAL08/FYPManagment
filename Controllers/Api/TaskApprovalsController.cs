using FYPManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Web.Http;
using FYPManagment.ViewModel;

namespace FYPManagment.Controllers.Api
{
    public class TaskApprovalsController : ApiController
    {
        private readonly ApplicationDbContext _Context;

        public TaskApprovalsController()
        {
            _Context = new ApplicationDbContext();
        }


        [HttpPost]

        public IHttpActionResult AcceptWork(ReviewWork Ids)
        {

            var GroupId = Ids.GroupId;
            var DocumentId = Ids.DocumentId;

            var workProgress = _Context.WorkProgress.Include(sv => sv.SuperVisor).Include(g => g.Group).Include(gm => gm.Group.GroupMembers.Select(m => m.Member)).Single(wp => wp.GroupId == GroupId && wp.DocumentId == DocumentId);

            workProgress.Approve = true;

            var group = workProgress.Group;
            var supervisor = workProgress.SuperVisor;
            group.TaskApprove(supervisor, supervisor);

            _Context.SaveChanges();

            return Ok();
        }




    }
}

