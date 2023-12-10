using System;
using System.Collections.Generic;

namespace SellBookStore.Models
{
    public partial class Roles
    {
        public Roles()
        {
            Customers = new HashSet<Customers>();
            FacebookProfile = new HashSet<FacebookProfile>();
            GoogleProfile = new HashSet<GoogleProfile>();
            RolePermissions = new HashSet<RolePermissions>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public ICollection<Customers> Customers { get; set; }
        public ICollection<FacebookProfile> FacebookProfile { get; set; }
        public ICollection<GoogleProfile> GoogleProfile { get; set; }
        public ICollection<RolePermissions> RolePermissions { get; set; }
    }
}
