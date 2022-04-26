using System;
using System.Collections.Generic;

#nullable disable

namespace API.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Bills = new HashSet<Bill>();
        }

        public int Id { get; set; }
        public string HashId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LockedDate { get; set; }
        public int? LockedCount { get; set; }
        public byte? IsActive { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
