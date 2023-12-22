using System;
using System.Collections.Generic;

namespace SalesSystem.Model;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public DateTime? RecordDate { get; set; }

    public virtual ICollection<AppUser> AppUsers { get; } = new List<AppUser>();

    public virtual ICollection<RoleMenu> RoleMenus { get; } = new List<RoleMenu>();
}
