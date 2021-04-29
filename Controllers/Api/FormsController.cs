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
    public class FormsController : ApiController
    {
        private readonly ApplicationDbContext _Context;

        public FormsController()
        {
            _Context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult PostForm(FormViewModel PostForm)
        {

            var group = _Context.Groups.Include(gm => gm.GroupMembers).Include(gm => gm.GroupMembers.Select(m => m.Member)).Single(g => g.Id == PostForm.GroupId);
            string userId = User.Identity.GetUserId();
            var coordinator = group.GroupMembers.Single(co => co.MemberId == userId && co.MemberRole == MemberRole.CoOrdinator).Member;
            var superevisor = group.GroupMembers.Single(co =>  co.MemberRole == MemberRole.SuperVisor).Member;

            var form = _Context.Forms.Single(f => f.Id == PostForm.FormId);          

            var groupForm = new GroupForm
            {
                Form = form,
                Group = group
            };

            _Context.GroupForms.Add(groupForm);
            group.PostForm(superevisor,coordinator);

            _Context.SaveChanges();
            return Ok();
        }
    }
}
