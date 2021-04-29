using FYPManagment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FYPManagment.Dtos
{
    public class GroupDto
    {
        public string Name { get; set; }
        public ICollection<GroupMemberDto> GroupMembers { get;  set; }
        public int Id { get; internal set; }
        public DateTime? PresentationDate { get; internal set; }
    }
}