using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name is too short")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Details are too long")]
        public string Details { get; set; }
    }
}
