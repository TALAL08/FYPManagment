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
    public class MeetUpRejectController : ApiController
    {
        private readonly ApplicationDbContext _Context;

        public MeetUpRejectController()
        {
            _Context = new ApplicationDbContext();
        }

        public IHttpActionResult ApproveMeetUp(int Id)
        {
            var userId = User.Identity.GetUserId();
            var group = _Context.Groups.Include(gm => gm.GroupMembers).Include(gm => gm.GroupMembers.Select(m => m.Member)).SingleOrDefault(g => g.Id == Id);
            var supervisor = group.GroupMembers.SingleOrDefault(gm => gm.MemberRole == MemberRole.SuperVisor).Member;
            var sender = group.GroupMembers.SingleOrDefault(m => m.MemberId == userId).Member;

            group.RejectedMeetUpRequest(supervisor, sender);

            _Context.SaveChanges();

            return Ok();

        }
    }
}
