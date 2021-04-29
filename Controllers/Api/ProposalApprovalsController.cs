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
    public class ProposalApprovalsController : ApiController
    {
        private readonly ApplicationDbContext _Context;

        public ProposalApprovalsController()
        {
            _Context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult ApproveProposal(int Id)
        {
            var group = _Context.Groups.Include(gm => gm.GroupMembers).SingleOrDefault(g => g.Id == Id);
            var GroupMembers = _Context.GroupMembers.Include(g => g.Group).Include(m => m.Member).Where(g => g.GroupId == Id).ToList();
            var supervisor = GroupMembers.Single(sv => sv.MemberRole == MemberRole.SuperVisor).Member;

            group.ProposalAccept(supervisor, supervisor);
            _Context.SaveChanges();

            return Ok();
        }
    }
}
