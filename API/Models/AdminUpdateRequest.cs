using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AdminUpdateRequest
    {
        [Required(ErrorMessage = "name is required.")]
        [MaxLength(255, ErrorMessage = "name cannot be greater than 255")]
        public string Name { get; set; }
        [Required(ErrorMessage = "role is required.")]
        public string Role { get; set; }
    }
}
