using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Models
{
    public class UserWithToken : Admin
    {

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserWithToken(Admin admin)
        {
            this.Id = admin.Id;
            this.Email = admin.Email;
            this.Role = admin.Role;
        }
    }
}
