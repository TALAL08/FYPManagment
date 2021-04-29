using FYPManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYPManagment.ViewModel
{
    public class ReviewWorkViewModel
    {
        public Notification Notification { get; set; }
        public Document Document { get; set; }
        public Group Group { get; set; }

        public WorkProgress WorkProgress { get; set; }
        public IEnumerable<WorkProgress> AllDocuments { get;  set; }
    }
}