using FYPManagment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FYPManagment.ViewModel
{
    public class NewDocumentViewModel
    {
        public HttpPostedFileBase File { get; set; }

        public Group Group { get; set; }
        public int GroupId { get; set; }
        public int DocumentId { get; set; }

        public static string GetNewFileName(string fileName)
        {
            var name = Path.GetFileNameWithoutExtension(fileName);
            var extention = Path.GetExtension(fileName);
            var date = "_" + DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString().Replace(" ", "").Replace(":", "").Replace("/", "");
            var datetime = date.Replace(" ", "").Replace(":", "").Replace("/", "").Replace(",", "");
            var NewName = name + datetime;
            var array = new string[2];
            array[0] = NewName;
            array[1] = extention;
            return  string.Concat(array);


        }
    }
}