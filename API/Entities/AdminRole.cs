using System;
using System.Collections.Generic;

#nullable disable

namespace API.Entities
{
    public partial class AdminRole
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual Role Role { get; set; }
    }
}
