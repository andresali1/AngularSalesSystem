using System;
using System.Collections.Generic;

namespace SalesSystem.Model;

public partial class RoleMenu
{
    public int RoleMenuId { get; set; }

    public int? MenuId { get; set; }

    public int? RoleId { get; set; }

    public virtual Menu? Menu { get; set; }

    public virtual UserRole? Role { get; set; }
}
