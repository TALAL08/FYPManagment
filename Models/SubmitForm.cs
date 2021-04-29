using System;
using System.ComponentModel.DataAnnotations;

namespace FYPManagment.Models
{
    public class SubmitForm
    {
        public int Id { get; set; }
        public Group Group { get; set; }
        public ApplicationUser Student { get; set; }
        public ApplicationUser CoOrdinator { get; set; }
        public int GroupId { get; set; }
        public string StudentId { get; set; }
        public string CoOrdinatorId { get; set; }

        [Required]
        public string FormName { get; set; }

        [Required]
        public string ConcatinateName { get; set; }

        public DateTime DateTime { get; set; }
        public bool IsCollected { get; set; }
    }
}