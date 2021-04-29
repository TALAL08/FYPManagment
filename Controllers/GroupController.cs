using FYPManagment.Models;
using FYPManagment.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace FYPManagment.Controllers
{
    public partial class GroupController : Controller
    {
        private readonly ApplicationDbContext _Context;

        public GroupController()
        {
            _Context = new ApplicationDbContext();
        }
        // GET: Group
        public ActionResult New()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index(NewDocumentViewModel document)
        {
            if (document.File.ContentLength > 0)
            {
                
                var userId = User.Identity.GetUserId();                


                var group = _Context.Groups.Include(gm => gm.GroupMembers.Select(m => m.Member)).Single(g => g.Id == document.Group.Id);
                var SuperVisor = group.GroupMembers.Single(g => g.GroupId == document.Group.Id && g.MemberRole == MemberRole.SuperVisor).Member;
                var sender = group.GroupMembers.Single(m => m.MemberId == userId).Member;

                var fileName = Path.GetFileName(document.File.FileName);
                var NewName = NewDocumentViewModel.GetNewFileName(fileName);

                var notificationType = NotificationType.SubmitProposal;

                if (SuperVisor.Id == userId)
                    notificationType = NotificationType.SendProposal;

                var notification = new Notification(notificationType , group, SuperVisor, group.GroupMembers.Single(u => u.MemberId == userId).Member);
                
                var NewDocument = new Document
                {
                    Group = group,
                    Name = fileName,
                    ConcatinateName = NewName,
                    SenderId = userId,
                    SuperVisorId = SuperVisor.Id,
                    Notification = notification,
                    DateTime = DateTime.Now
                };

                _Context.Documents.Add(NewDocument);

                if (SuperVisor.Id == userId)                    
                    group.SendProposal(SuperVisor, sender, notification);

                else
                    group.ProposalSubmit(SuperVisor, sender, notification);

                var path = Path.Combine(Server.MapPath("~/proposals"), NewName);

                document.File.SaveAs(path);

                _Context.SaveChanges();


            }
            return RedirectToAction("Index", "Home");


        }
        public ActionResult Details(int Id)
        {
            var userId = User.Identity.GetUserId();

            var Group = _Context.Groups.Include(gm => gm.GroupMembers).Include(gm => gm.GroupMembers.Select(m => m.Member)).Single(g => g.Id == Id);
            var groupMembers = Group.GroupMembers.Where(g => g.GroupId == Id).ToList();
            var currentUser = groupMembers.Single(m => m.MemberId == userId);
            var document = _Context.WorkProgress.Include(d => d.Document).Where(d => d.GroupId == Id  ).Select(d => d.Document).ToList();
            var meetingAndPresentationDates = _Context.MeetingAndPresentationDateTimes.Where(mp => mp.GroupId == Id).ToList();

            var groupChat = _Context.GroupChats.Include(s => s.Sender).Where(gc => gc.GroupId == Group.Id).ToList();


            if (Group.MeetUpDateTime < DateTime.Now)
            {
                Group.MeetUpDateTime = null;
                Group.MeetUpRequestSend = false;
                Group.MeetUpRequestApproved = false;
            }


            if (Group.PresentationDate < DateTime.Now)
                Group.PresentationDate = null;

            var ViewModel = new NewGroupViewModel
            {
                GroupMembers = groupMembers,
                Group = Group,
                Document = document,
                MeetingAndPresendationDates = meetingAndPresentationDates,
                user = currentUser.Member,
                GroupChat = groupChat
            };

            if (groupMembers.Where(s => s.MemberRole == MemberRole.Student).Any(g => g.PresentationGrade != null))
                ViewModel.GradeUpload = true;

            if (currentUser.MemberRole == MemberRole.SuperVisor)
                return View("SuperVisorView", ViewModel);

            else if (currentUser.MemberRole == MemberRole.CoOrdinator)
            {
                ViewModel.Forms = _Context.Forms.ToList();
                ViewModel.SubmitForms = _Context.submitForms.Where(f => f.GroupId == Group.Id).ToList();
                return View("CoordinatorView", ViewModel);
            }

            else
            {
                ViewModel.SubmitForms = _Context.submitForms.Where(sf => sf.StudentId == userId).ToList();
                ViewModel.Forms = _Context.GroupForms.Include(f => f.Form).Where(g => g.GroupId == Group.Id).Select(f => f.Form).ToList();
            }
            
            return View("StudentView", ViewModel);
        }

        public ActionResult ViewAssignWork(int Id)
        {

            var groupMembers = _Context.GroupMembers.Include(g => g.Group).Include(u => u.Member).Where(g => g.GroupId == Id).ToList();
            var supervisorId = groupMembers.Single(s => s.MemberRole == MemberRole.SuperVisor).MemberId;
            var Group = groupMembers.FirstOrDefault(g => g.GroupId == Id).Group;

            var workProgress = _Context.WorkProgress.Include(d => d.Document).Where(d => d.GroupId == Id ).ToList();

            var AssignWorks = workProgress.Where(wp => wp.Document.SenderId == supervisorId).ToList();
            var submitedWork = workProgress.Where(wp => wp.Document.SenderId != supervisorId).ToList();
          
            var ViewModel = new NewGroupViewModel
            {
                GroupMembers = groupMembers,
                Group = Group,
                SupervisorId = supervisorId,
                AssignWorks = AssignWorks,
                submitedWork = submitedWork

            };


            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult AssignWork(NewDocumentViewModel document)
        {
            if (document.File.ContentLength > 0)
            {
                var userId = User.Identity.GetUserId();
                var fileName = Path.GetFileName(document.File.FileName);
                var NewName = NewDocumentViewModel.GetNewFileName(fileName);
                var path = Path.Combine(Server.MapPath("~/AssignTask"), NewName);

                var group = _Context.Groups.Include(gm => gm.GroupMembers.Select(m => m.Member)).Single(g => g.Id == document.Group.Id);
                var SuperVisor = group.GroupMembers.Single(g => g.GroupId == document.Group.Id && g.MemberRole == MemberRole.SuperVisor).Member;

                var notification = new Notification(NotificationType.AssignTask, group, SuperVisor, SuperVisor);

                var NewDocument = new Document
                {
                    Group = group,
                    Name = fileName,
                    ConcatinateName = NewName,
                    SenderId = userId,
                    SuperVisor = SuperVisor,
                    Notification = notification,
                    DateTime = DateTime.Now

                };

                document.File.SaveAs(path);
                _Context.Documents.Add(NewDocument);

                var Assignwork = new WorkProgress
                {
                    Group = group,
                    Document = NewDocument,
                    SuperVisor = SuperVisor,
                    TaskAssign = true
                };

                _Context.WorkProgress.Add(Assignwork);

                group.AssignTask(notification);

                _Context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");


        }

        [HttpPost]
        public ActionResult SubmitAssignWork(NewDocumentViewModel document)
        {
            if (document.File.ContentLength > 0)
            {
                var userId = User.Identity.GetUserId();
                var fileName = Path.GetFileName(document.File.FileName);
                var NewName = NewDocumentViewModel.GetNewFileName(fileName);
                var path = Path.Combine(Server.MapPath("~/SubmitTask"), NewName);

                var group = _Context.Groups.Include(gm => gm.GroupMembers.Select(m => m.Member)).Single(g => g.Id == document.Group.Id);
                var SuperVisor = group.GroupMembers.Single(g => g.GroupId == document.Group.Id && g.MemberRole == MemberRole.SuperVisor).Member;
                var sender = group.GroupMembers.Single(g => g.GroupId == document.Group.Id && g.MemberId == userId).Member;

                var notification = new Notification(NotificationType.AssignTaskSubmit, group, SuperVisor, sender);

                var NewDocument = new Document
                {
                    Group = group,
                    Name = fileName,
                    ConcatinateName = NewName,
                    SenderId = userId,
                    SuperVisor = SuperVisor,
                    Notification = notification,
                    DateTime = DateTime.Now
                };

                document.File.SaveAs(path);
                _Context.Documents.Add(NewDocument);

                var SubmitTask = new WorkProgress
                {
                    Document = NewDocument,
                    Group = group,
                    SuperVisor = SuperVisor,
                    TaskSubmit = true
                };

                _Context.WorkProgress.Add(SubmitTask);

                group.SubmitTask(SuperVisor, notification);


                _Context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");


        }


        public ActionResult SendDocument(NewDocumentViewModel document)
        {
            if (document.File.ContentLength > 0)
            {
                var userId = User.Identity.GetUserId();
                var fileName = Path.GetFileName(document.File.FileName);
                var NewName = NewDocumentViewModel.GetNewFileName(fileName);
                var path = Path.Combine(Server.MapPath("~/otherFiles"), NewName);

                var group = _Context.Groups.Include(gm => gm.GroupMembers.Select(m => m.Member)).Single(g => g.Id == document.Group.Id);
                var SuperVisor = group.GroupMembers.Single(g => g.GroupId == document.Group.Id && g.MemberRole == MemberRole.SuperVisor).Member;
                var sender = group.GroupMembers.Single(g => g.GroupId == document.Group.Id && g.MemberId == userId).Member;

                var notification = new Notification(NotificationType.otherDocument, group, SuperVisor, sender);

                var NewDocument = new Document
                {
                    Group = group,
                    Name = fileName,
                    ConcatinateName = NewName,
                    SenderId = userId,
                    SuperVisor = SuperVisor,
                    Notification = notification,
                    DateTime = DateTime.Now
                };

                document.File.SaveAs(path);
                _Context.Documents.Add(NewDocument);

                var sendFile = new WorkProgress
                {
                    Document = NewDocument,
                    Group = group,
                    SuperVisor = SuperVisor,
                    OtherFile = true
                };

                _Context.WorkProgress.Add(sendFile);

                group.SendFile(SuperVisor, notification);


                _Context.SaveChanges();
            }

            return RedirectToAction("Details", "Group", document.Group.Id);
        }

        public ActionResult otherFiles(int Id)
        {
            var otherFiles = _Context.WorkProgress.Include(wp => wp.Document).Include(wp => wp.Group).Include(wp => wp.Document.Sender).Where(d => d.GroupId == Id && d.OtherFile).ToList();

            var viewmodel = new ReviewWorkViewModel
            {
                AllDocuments = otherFiles
            };
                
                
            

            return View(viewmodel);
        }

        public ActionResult Grade(int Id)
        {
            var userId = User.Identity.GetUserId();
            var group = _Context.Groups.Include(gm => gm.GroupMembers.Select(m => m.Member)).Single(g => g.Id == Id);
            var students = group.GroupMembers.Where(gm => gm.MemberRole == MemberRole.Student).Select(m => m.Member).ToList();

            var currentUser = group.GroupMembers.Single(gm => gm.MemberId == userId);

                var members = group.GroupMembers.Where(gm => gm.MemberRole == MemberRole.Student).ToList();
                var ViewModel = new GradeViewModel
                {
                    group = group,
                    students = students,
                    CurrentUser = currentUser,
                    members = members
                };

                return View(ViewModel);
        }

        public ActionResult SubmitForm(NewDocumentViewModel document)
        {
            if (document.File.ContentLength > 0)
            {
                var userId = User.Identity.GetUserId();
                var fileName = Path.GetFileName(document.File.FileName);
                var NewName = NewDocumentViewModel.GetNewFileName(fileName);
                var path = Path.Combine(Server.MapPath("~/submitForm"), NewName);

                var group = _Context.Groups.Include(gm => gm.GroupMembers.Select(m => m.Member)).Single(g => g.Id == document.Group.Id);
                var SuperVisor = group.GroupMembers.Single(g => g.GroupId == document.Group.Id && g.MemberRole == MemberRole.SuperVisor).Member;
                var sender = group.GroupMembers.Single(g => g.GroupId == document.Group.Id && g.MemberId == userId).Member;
                var CoOrdinator = group.GroupMembers.Single(g => g.GroupId == document.Group.Id && g.MemberRole == MemberRole.CoOrdinator).Member;


                var submitForm = new SubmitForm
                {
                    Group = group,
                    Student = sender,
                    CoOrdinator = CoOrdinator,
                    FormName = fileName,
                    ConcatinateName = NewName,
                    DateTime = DateTime.Now
                
                };

                _Context.submitForms.Add(submitForm);
                
                group.SubmitForm(SuperVisor, CoOrdinator, sender);
                _Context.SaveChanges();

                document.File.SaveAs(path);
            }

            return RedirectToAction("Details", "Group", new { document.Group.Id });
        }


        public ActionResult Forms(int Id)
        {
            var userId = User.Identity.GetUserId();
            var forms = _Context.submitForms.Include(g => g.Group).Include(s => s.Student).Where(sf => sf.GroupId == Id && sf.CoOrdinatorId == userId).ToList();

            var ViewModel = new FormViewModel
            {
                forms = forms
            };

            return View(ViewModel);
        }

        public ActionResult ViewSubmitForms(int Id)
        {

            var userId = User.Identity.GetUserId();
            var submitForms = _Context.submitForms.Where(sf => sf.GroupId == Id && sf.StudentId == userId).ToList();

            var viewmodel = new NewGroupViewModel
            {
                SubmitForms = submitForms
            };

            return View(viewmodel);
        }
    }


    


}