using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    public class AdminChangePassword
    {
        [Required(ErrorMessage = "token is required")]
        [MaxLength(255, ErrorMessage = "toke cannot be greater than 255")]
        public string Token { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [JsonIgnore]
        [NotMapped]
        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password are not match.")]
        public string ConfirmPassword { get; set; }
    }
}
