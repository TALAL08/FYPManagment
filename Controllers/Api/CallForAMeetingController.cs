using FYPManagment.Models;
using FYPManagment.ViewModel;
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
    public class CallForAMeetingController : ApiController
    {
        private readonly ApplicationDbContext _Context;

        public CallForAMeetingController()
        {
            _Context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult RequestMeetUp(MeetUpViewModel MeetUp)
        {
            if (MeetUp.DateTime < DateTime.Now)
                return BadRequest();

            var userId = User.Identity.GetUserId();
            var group = _Context.Groups.Include(gm => gm.GroupMembers).Include(gm => gm.GroupMembers.Select(m => m.Member)).SingleOrDefault(g => g.Id == MeetUp.GroupId);
            var supervisor = group.GroupMembers.SingleOrDefault(gm => gm.MemberRole == MemberRole.SuperVisor).Member;

            group.CallForMeetUp(supervisor, supervisor, MeetUp.DateTime);

            var meeting = new MeetingAndPresentationDateTime
            {
                Group = group,
                MeetingDateTime = MeetUp.DateTime
            };

            _Context.MeetingAndPresentationDateTimes.Add(meeting);

            _Context.SaveChanges();

            return Ok();
        }
    }
}
