using System;
using System.Collections.Generic;

namespace SellBookStore.Models
{
    public partial class Books
    {
        public Books()
        {
            Description = new HashSet<Description>();
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }
        public int? CateId { get; set; }
        public string Mota { get; set; }
        public string FileBook { get; set; }

        public Catetory Cate { get; set; }
        public ICollection<Description> Description { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
