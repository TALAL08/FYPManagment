using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYPManagment.Models
{
    public class GroupForm
    {

        public Group Group { get; set; }
        public Form Form { get; set; }

        [Key]
        [Column(Order = 1)]
        public int GroupId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int FormId { get; set; }


    }
}