using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AdminCreateRequest
    {
        [Required(ErrorMessage = "name is required.")]
        [MaxLength(255, ErrorMessage = "name cannot be greater than 255")]
        public string Name { get; set; }
        [Required(ErrorMessage = "email is required.")]
        [EmailAddress(ErrorMessage = "Enter valid email address.")]
        [MaxLength(255, ErrorMessage = "email cannot be greater than 255")]
        public string Email { get; set; }
        [Required(ErrorMessage = "role is required.")]
        public string Role { get; set; }
    }
}
