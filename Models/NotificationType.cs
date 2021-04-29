using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYPManagment.Models
{
    public enum NotificationType
    {
        SubmitProposal = 1,
        ProposalAccepted = 2,
        ProposalRejeced = 3,
        AssignTask = 4,
        AssignTaskSubmit = 5,
        TaskApproved = 6,
        TaskReject = 7,
        RequestMeetUp = 8,
        MeetUpApproved = 9,
        MeetUpRejected = 10,
        SendProposal = 11,
        SchedulePresentation = 12,
        CallForMeetUp = 13,
        SendGrade = 14,
        otherDocument = 15,
        PostForm = 16,
        SubmitForm = 17,
        FormCollect = 18
    }
}