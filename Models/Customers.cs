using System;
using System.Collections.Generic;

namespace SellBookStore.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Orders = new HashSet<Orders>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public Roles Role { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
