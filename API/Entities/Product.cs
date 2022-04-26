using System;
using System.Collections.Generic;

#nullable disable

namespace API.Entities
{
    public partial class Product
    {
        public Product()
        {
            BillDetails = new HashSet<BillDetail>();
            Images = new HashSet<Image>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdType { get; set; }
        public double? Price { get; set; }
        public string Color { get; set; }
        public string Material { get; set; }
        public string Description { get; set; }
        public byte? New { get; set; }

        public virtual ProductType IdTypeNavigation { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
