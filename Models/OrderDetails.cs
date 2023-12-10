using System;
using System.Collections.Generic;

namespace SellBookStore.Models
{
    public partial class OrderDetails
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? BookId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Subtotal { get; set; }

        public Books Book { get; set; }
        public Orders Order { get; set; }
    }
}
