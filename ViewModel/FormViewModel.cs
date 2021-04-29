using FYPManagment.Models;
using System.Collections.Generic;

namespace FYPManagment.ViewModel
{

    public class FormViewModel
    {
        public IEnumerable<SubmitForm> forms { get; set; }
        public int GroupId { get;  set; }
        public int FormId { get;  set; }
    }






}