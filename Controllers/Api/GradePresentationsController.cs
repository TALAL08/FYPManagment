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
    public class GradePresentationsController : ApiController
    {
        private readonly ApplicationDbContext _Context;

        public GradePresentationsController()
        {
            _Context = new ApplicationDbContext();
        }

        
        public IHttpActionResult GetStudents(int Id)
        {
            var students = _Context.GroupMembers.Include(m => m.Member).Where(g => g.GroupId == Id && g.MemberRole == MemberRole.Student).ToList();

            return Ok(students);
        }

        [HttpPost]
        public IHttpActionResult GradePresentation(GradePresentationViewModel StudentGrade)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            var count = StudentGrade.StudentIds.Count;
            for (int i = 0; i < count; i++)
                keyValuePairs.Add(StudentGrade.StudentIds[i], StudentGrade.Grades[i]);
            

            var group = _Context.Groups.Include(gm => gm.GroupMembers).Include(gm => gm.GroupMembers.Select(m => m.Member)).Single(g => g.Id == StudentGrade.GroupId);
            var students = group.GroupMembers.Where(gm => StudentGrade.StudentIds.Contains(gm.MemberId)).ToList();

            foreach (var student in students)
                student.PresentationGrade = keyValuePairs[student.MemberId];


            group.SendGrade(group.GroupMembers.Single(gm => gm.MemberRole == MemberRole.SuperVisor).Member, group.GroupMembers.Single(c => c.MemberRole == MemberRole.CoOrdinator).Member);

            _Context.SaveChanges();

            return Ok();
        }
    }
}
