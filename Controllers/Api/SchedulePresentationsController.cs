using FYPManagment.Models;
using FYPManagment.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FYPManagment.Controllers.Api
{
    public class SchedulePresentationsController : ApiController
    {
        private readonly ApplicationDbContext _Context;

        public SchedulePresentationsController()
        {
            _Context = new ApplicationDbContext();
        }

        [HttpPost]

        public IHttpActionResult SchedulePresentations(SchedulePresentationViewModel schedulePresentation)
        {
            var group = _Context.Groups.Include(g => g.GroupMembers.Select(m => m.Member)).Single(g => g.Id == schedulePresentation.GroupId);

            var CoOrdinator = group.GroupMembers.Single(gm => gm.MemberRole == MemberRole.CoOrdinator).Member;
            var SuperVisor = group.GroupMembers.Single(gm => gm.MemberRole == MemberRole.SuperVisor).Member;

            if (schedulePresentation.DateTime < DateTime.Now)
                return BadRequest();

            group.SchedulePresentation(SuperVisor ,CoOrdinator, schedulePresentation.DateTime);

            var presentation = new MeetingAndPresentationDateTime
            {
                Group = group,
                PresentationDateTime = schedulePresentation.DateTime
            };

            _Context.MeetingAndPresentationDateTimes.Add(presentation);

            _Context.SaveChanges();


            return Ok();
        }
    }
}
