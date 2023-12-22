using System;
using System.Collections.Generic;

namespace SalesSystem.Model;

public partial class AppUser
{
    public int UserId { get; set; }

    public string? CompleteName { get; set; }

    public string? Email { get; set; }

    public int? RoleId { get; set; }

    public string? Pass { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? RecordDate { get; set; }

    public virtual UserRole? Role { get; set; }
}
