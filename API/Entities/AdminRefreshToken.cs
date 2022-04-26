using System;
using System.Collections.Generic;

#nullable disable

namespace API.Entities
{
    public partial class AdminRefreshToken
    {
        public int TokenId { get; set; }
        public int AdminId { get; set; }
        public string Token { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public virtual Admin Admin { get; set; }
    }
}
