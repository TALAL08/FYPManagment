using FYPManagment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FYPManagment.Controllers.Api
{
    public class DocumentsController : ApiController
    {
        private readonly ApplicationDbContext _Context;

        public DocumentsController()
        {
            _Context = new ApplicationDbContext();
        }


        public IHttpActionResult TaskSubmit(int Id)
        {
            var group = _Context.Documents.Include(g => g.Group.GroupMembers.Select(m =>m.Member)).Single(d => d.Id == Id).Group;

           

            _Context.SaveChanges();

            return Ok();
        }


        public IHttpActionResult RejectProposal(int Id)
        {
            var group = _Context.Documents.Include(g => g.Group.GroupMembers.Select(m => m.Member)).Single(d => d.Id == Id).Group;
            _Context.SaveChanges();

            return Ok();
        }
    }
}
