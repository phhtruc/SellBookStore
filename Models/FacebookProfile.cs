using System;
using System.Collections.Generic;

namespace SellBookStore.Models
{
    public partial class FacebookProfile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public int? RoleId { get; set; }

        public Roles Role { get; set; }
    }
}
