using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AuthenticateRequest
    {
        [Required(ErrorMessage = "email is required.")]
        [EmailAddress(ErrorMessage = "Enter valid email address.")]
        [MaxLength(255, ErrorMessage = "email cannot be greater than 255")]
        public string Email { get; set; }
        [Required(ErrorMessage = "password is required.")]
        [MaxLength(255, ErrorMessage = "password cannot be greater than 255")]
        [MinLength(8, ErrorMessage = "password cannot be less than 8")]
        public string Password { get; set; }
    }
}
