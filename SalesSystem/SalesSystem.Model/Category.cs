using System;
using System.Collections.Generic;

namespace SalesSystem.Model;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? RecordDate { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
