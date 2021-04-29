using System.ComponentModel.DataAnnotations;

namespace FYPManagment.Models
{
    public class Form
    {
        public int Id { get; set; }
        
        [Required]
        public string FormName { get; set; }
       
    }
}