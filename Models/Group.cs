using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FYPManagment.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class Group
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }       
        public ICollection<GroupMember> GroupMembers { get; private set; }

        public bool ProposalAccepted { get; private set; }
        public bool ProposalRejected { get; private set; }
        public bool ProposalSend { get; private set; }

        public bool MeetUpRequestSend { get;  set; }
        public bool MeetUpRequestApproved { get;  set; }

        public bool MeetUpRequestReject { get; private set; }
        public bool FormPost { get; private set; }
        public DateTime? MeetUpDateTime { get;  set; }
        public DateTime? PresentationDate { get;  set; }


        public Group()
        {
            GroupMembers = new Collection<GroupMember>();
        }

        public void SchedulePresentation(ApplicationUser SuperVisor, ApplicationUser coOrdinator, DateTime dateTime)
        {
            PresentationDate = dateTime;
            var notification = new Notification(NotificationType.SchedulePresentation, this, SuperVisor , coOrdinator, dateTime);

            foreach (var member in GroupMembers.Where(m => m.MemberRole != MemberRole.CoOrdinator).Select(m => m.Member))
                member.Notify(notification);
        }

        public void SendGrade(ApplicationUser SuperVisor, ApplicationUser coOrdinator)
        {
            var notification = new Notification(NotificationType.SendGrade, this, SuperVisor, coOrdinator);

            foreach (var member in GroupMembers.Where(m => m.MemberRole != MemberRole.CoOrdinator).Select(m => m.Member))
                member.Notify(notification);

        }

        public void SendProposal(ApplicationUser superVisor, ApplicationUser sender, Notification notification)
        {
            foreach (var member in GroupMembers.Where(m => m.MemberRole != MemberRole.SuperVisor).Select(m => m.Member))
                member.Notify(notification);
        }
        public void ProposalSubmit(ApplicationUser supervisor,ApplicationUser Sender, Notification notification)
        {
            ProposalSend = true;
            ProposalRejected = false;
            ProposalAccepted = false;            
            
            supervisor.Notify(notification);
        }
        public void ProposalAccept(ApplicationUser supervisor, ApplicationUser sender)
        {
            var notification = new Notification(NotificationType.ProposalAccepted, this, supervisor, sender);
            ProposalAccepted = true;
            ProposalRejected = false;

            foreach (var member in GroupMembers.Where(m => m.MemberRole != MemberRole.SuperVisor).Select(m => m.Member))
                member.Notify(notification);
        }
        public void ProposalReject(ApplicationUser supervisor, ApplicationUser sender)
        {
            var notification = new Notification(NotificationType.ProposalRejeced, this, supervisor, sender);
            ProposalRejected = true;
            ProposalAccepted = false;

            foreach (var member in GroupMembers.Where(m => m.MemberRole != MemberRole.SuperVisor).Select(m => m.Member))
                member.Notify(notification);
        }


        public void AssignTask(Notification notification)
        {
            foreach (var member in GroupMembers.Where(m => m.MemberRole != MemberRole.SuperVisor).Select(m => m.Member))
                member.Notify(notification);
        }

        public void SubmitTask(ApplicationUser supervisor, Notification notification)
        {
            supervisor.Notify(notification);
        }
        public void SendFile(ApplicationUser supervisor, Notification notification)
        {
            supervisor.Notify(notification);
        }
        public void TaskApprove(ApplicationUser supervisor, ApplicationUser sender)
        {
            var notification = new Notification(NotificationType.TaskApproved, this, supervisor, sender);

            foreach (var member in GroupMembers.Where(m => m.MemberRole != MemberRole.SuperVisor).Select(m => m.Member))
                member.Notify(notification);
        }
        public void TaskReject(ApplicationUser supervisor, ApplicationUser sender)
        {
            var notification = new Notification(NotificationType.TaskReject, this, supervisor, sender);

            foreach (var member in GroupMembers.Where(m => m.MemberRole != MemberRole.SuperVisor).Select(m => m.Member))
                member.Notify(notification);
        }
        public void CallForMeetUp(ApplicationUser supervisor, ApplicationUser sender, DateTime dateTime)
        {
            MeetUpRequestSend = true;
            MeetUpRequestApproved = true;
            MeetUpRequestReject = false;
            MeetUpDateTime = dateTime;

            var notification = new Notification(NotificationType.CallForMeetUp, this, supervisor, sender, dateTime);

            foreach (var member in GroupMembers.Where(m => m.MemberRole != MemberRole.SuperVisor).Select(m => m.Member))
                member.Notify(notification);
        }

        public void RequestForMeetUp(ApplicationUser supervisor, ApplicationUser sender)
        {
            MeetUpRequestSend = true;
            MeetUpRequestApproved = false;
            MeetUpRequestReject = false;

            var notification = new Notification(NotificationType.RequestMeetUp, this, supervisor, sender);
            supervisor.Notify(notification);

        }

        public void ApprovedMeetUpRequest(ApplicationUser supervisor, ApplicationUser sender, DateTime dateTime)
        {
            MeetUpRequestSend = true;
            MeetUpRequestApproved = true;
            MeetUpRequestReject = false;
            MeetUpDateTime = dateTime;
            var notification = new Notification(NotificationType.MeetUpApproved, this, supervisor, sender, dateTime);
            
            foreach (var member in GroupMembers.Where(m => m.MemberRole != MemberRole.SuperVisor).Select(m => m.Member))
                member.Notify(notification);

        }
        public void RejectedMeetUpRequest(ApplicationUser supervisor, ApplicationUser sender)
        {
            MeetUpRequestSend = true;
            MeetUpRequestApproved = false;
            MeetUpRequestReject = true;

            var notification = new Notification(NotificationType.MeetUpRejected, this, supervisor, sender);
            
            foreach (var member in GroupMembers.Where(m => m.MemberRole != MemberRole.SuperVisor).Select(m => m.Member))
                member.Notify(notification);

        }
        public void PostForm(ApplicationUser superevisor, ApplicationUser coordinator)
        {

            FormPost = true;
            var notification = new Notification(NotificationType.PostForm, this, superevisor, coordinator);
            foreach (var member in GroupMembers.Where(m => m.MemberRole == MemberRole.Student).Select(m => m.Member))
                member.Notify(notification);
        }

        public void SubmitForm(ApplicationUser superVisor, ApplicationUser coOrdinator, ApplicationUser sender)
        {
            var notification = new Notification(NotificationType.SubmitForm, this, superVisor, sender);

            coOrdinator.Notify(notification);

        }
    }
}