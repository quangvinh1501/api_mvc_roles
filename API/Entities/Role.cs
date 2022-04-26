using System;
using System.Collections.Generic;

#nullable disable

namespace API.Entities
{
    public partial class Role
    {
        public Role()
        {
            AdminRoles = new HashSet<AdminRole>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<AdminRole> AdminRoles { get; set; }
    }
}
