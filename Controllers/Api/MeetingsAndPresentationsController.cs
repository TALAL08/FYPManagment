using FYPManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FYPManagment.Controllers.Api
{
    public class MeetingsAndPresentationsController : ApiController
    {
        private readonly ApplicationDbContext _Context;

        public MeetingsAndPresentationsController()
        {
            _Context = new ApplicationDbContext();
        }

        public IHttpActionResult GetDates(int Id)
        {
            var Dates = _Context.MeetingAndPresentationDateTimes.Where(mp => mp.GroupId == Id).ToList();

            return Ok(Dates);
        }
    }
}
