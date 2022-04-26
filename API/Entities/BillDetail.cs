using System;
using System.Collections.Generic;

#nullable disable

namespace API.Entities
{
    public partial class BillDetail
    {
        public int Id { get; set; }
        public int IdBill { get; set; }
        public int IdProduct { get; set; }
        public double? Quantity { get; set; }
        public double? Price { get; set; }

        public virtual Bill IdBillNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}
