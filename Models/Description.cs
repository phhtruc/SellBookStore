using System;
using System.Collections.Generic;

namespace SellBookStore.Models
{
    public partial class Description
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public string Description1 { get; set; }

        public Books Book { get; set; }
    }
}
