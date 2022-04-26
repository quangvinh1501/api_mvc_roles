using System;
using System.Collections.Generic;

#nullable disable

namespace API.Entities
{
    public partial class Admin
    {
        public Admin()
        {
            AdminRefreshTokens = new HashSet<AdminRefreshToken>();
            AdminRoles = new HashSet<AdminRole>();
        }

        public int Id { get; set; }
        public string HashId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LockedDate { get; set; }
        public int? LockedCount { get; set; }
        public byte? Isactive { get; set; }
        public string Role { get; set; }

        public virtual ICollection<AdminRefreshToken> AdminRefreshTokens { get; set; }
        public virtual ICollection<AdminRole> AdminRoles { get; set; }
    }
}
