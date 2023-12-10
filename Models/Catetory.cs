using System;
using System.Collections.Generic;

namespace SellBookStore.Models
{
    public partial class Catetory
    {
        public Catetory()
        {
            Books = new HashSet<Books>();
        }

        public int CateId { get; set; }
        public string Title { get; set; }

        public ICollection<Books> Books { get; set; }
    }
}
