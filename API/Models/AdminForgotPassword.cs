using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AdminForgotPassword
    {
        [Required(ErrorMessage = "email is required.")]
        [EmailAddress(ErrorMessage = "Enter valid email address.")]
        [MaxLength(255, ErrorMessage = "email cannot be greater than 255")]
        public string Email { get; set; }
    }
}
