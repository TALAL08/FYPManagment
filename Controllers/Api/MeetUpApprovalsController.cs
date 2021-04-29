    using FYPManagment.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using FYPManagment.ViewModel;

namespace FYPManagment.Controllers.Api
{
    public class MeetUpApprovalsController : ApiController
    {
        private readonly ApplicationDbContext _Context;

        public MeetUpApprovalsController()
        {
            _Context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult ApproveMeetUp(MeetUpViewModel MeetUp)
        {
            var userId = User.Identity.GetUserId();
            var group = _Context
                .Groups
                .Include(gm => gm.GroupMembers)
                .Include(gm => gm.GroupMembers
                .Select(m => m.Member))
                .SingleOrDefault(g => g.Id == MeetUp.GroupId);

            var supervisor = group
                .GroupMembers
                .SingleOrDefault(gm => gm.MemberRole == MemberRole.SuperVisor)
                .Member;
            var sender = group
                .GroupMembers
                .SingleOrDefault(m => m.MemberId == userId)
                .Member;
            
            group.ApprovedMeetUpRequest(supervisor, sender, MeetUp.DateTime);

            var meeting = new MeetingAndPresentationDateTime
            {
                Group = group,
                MeetingDateTime = MeetUp.DateTime,
                PresentationDateTime = null
            };

            _Context.MeetingAndPresentationDateTimes.Add(meeting);

            _Context.SaveChanges();

            return Ok();
            
        }
    }
}
