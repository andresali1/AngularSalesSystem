using System;
using System.Collections.Generic;

namespace SalesSystem.Model;

public partial class Menu
{
    public int MenuId { get; set; }

    public string? MenuName { get; set; }

    public string? Icon { get; set; }

    public string? MenuUrl { get; set; }

    public virtual ICollection<RoleMenu> RoleMenus { get; } = new List<RoleMenu>();
}
