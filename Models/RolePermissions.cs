using System;
using System.Collections.Generic;

namespace SellBookStore.Models
{
    public partial class RolePermissions
    {
        public int RolePermissionId { get; set; }
        public int? RoleId { get; set; }
        public int? PermissionId { get; set; }

        public Permissions Permission { get; set; }
        public Roles Role { get; set; }
    }
}
