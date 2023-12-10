using System;
using System.Collections.Generic;

namespace SellBookStore.Models
{
    public partial class Permissions
    {
        public Permissions()
        {
            RolePermissions = new HashSet<RolePermissions>();
        }

        public int PermissionId { get; set; }
        public string PermissionName { get; set; }

        public ICollection<RolePermissions> RolePermissions { get; set; }
    }
}
