using System;
using System.Collections.Generic;

namespace SellBookStore.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }

        public Customers Customer { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
