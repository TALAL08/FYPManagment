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

    public class GroupsController : ApiController
    {
        private readonly ApplicationDbContext _Context;

        public GroupsController()
        {
            _Context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateGroup(NewGroupViewModel newGroup)
        {
            var users = _Context.Users.ToList();
            var GroupMembers = users.Where(u => newGroup.MemberIds.Contains(u.Id)).ToList();
            var GroupSuperVisor = users.Single(u => u.Id == newGroup.SupervisorId);
            var GroupCoordinator = users.Single(u => u.Id == newGroup.CoordinatorId);



            var Group = new Group
            {
                Name = newGroup.Name
            };

            var groupSupervisor = new GroupMember
            {
                Group = Group,
                MemberId = GroupSuperVisor.Id,
                MemberRole = MemberRole.SuperVisor
            };

            var groupcoordinator = new GroupMember
            {
                Group = Group,
                MemberId = GroupCoordinator.Id,
                MemberRole = MemberRole.CoOrdinator
            };

            _Context.GroupMembers.Add(groupSupervisor);
            _Context.GroupMembers.Add(groupcoordinator);
            _Context.Groups.Add(Group);

            foreach (var Member in GroupMembers)
            {
                var GroupMember = new GroupMember
                {
                    MemberId = Member.Id,
                    Group = Group,
                    MemberRole = MemberRole.Student
                };

                _Context.GroupMembers.Add(GroupMember);
            }

            _Context.SaveChanges();

            return Ok();
        }

    
    }
}
