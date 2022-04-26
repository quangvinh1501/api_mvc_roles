using System;
using System.Collections.Generic;

#nullable disable

namespace API.Entities
{
    public partial class Image
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public int IdProduct { get; set; }

        public virtual Product IdProductNavigation { get; set; }
    }
}
