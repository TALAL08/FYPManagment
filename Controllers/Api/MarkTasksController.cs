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
    public class MarkTasksController : ApiController
    {
        private readonly ApplicationDbContext _Context;

        public MarkTasksController()
        {
            _Context = new ApplicationDbContext();
        }

        public IHttpActionResult MarkAsCompleted(ReviewWork Ids)
        {

            var GroupId = Ids.GroupId;
            var DocumentId = Ids.DocumentId;
            var workProgress = _Context.WorkProgress.Single(wp => wp.GroupId == GroupId && wp.DocumentId == DocumentId);

            workProgress.Approve = true;
            _Context.SaveChanges();

            return Ok();

        }
    }
}
