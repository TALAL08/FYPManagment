using FYPManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYPManagment.ViewModel
{
    public class NewGroupViewModel
    {
        

        public string Name { get; set; }
        public IList<string> MemberIds { get; set; }

        public string SupervisorId { get; set; }

        public string CoordinatorId { get; set; }
        public ApplicationUser user { get; set; }
        public Group Group { get; set; }
        public IEnumerable< GroupMember> GroupMembers { get; set; }
        public List<Document> Document { get; internal set; }

        public int DocumentId { get; set; }
        public IEnumerable<WorkProgress> WorkProgress { get;  set; }

        public DateTime? PresentationDate { get; set; }

        public bool GradeUpload { get; set; }
        public IEnumerable<WorkProgress> submitedWork { get;  set; }    
        public IEnumerable<WorkProgress> AssignWorks { get;  set; }
        public List<Form> Forms { get;  set; }
        public List<SubmitForm> SubmitForms { get;  set; }
        public List<GroupForm> GroupForms { get;  set; }
        public List<MeetingAndPresentationDateTime> MeetingAndPresendationDates { get;  set; }
        public List<GroupChat> GroupChat { get; internal set; }
    }

}