using System;
using System.Collections.Generic;

namespace SalesSystem.Model;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public int? CategoryId { get; set; }

    public int? Stock { get; set; }

    public decimal? Price { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? RecordDate { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<SaleDetail> SaleDetails { get; } = new List<SaleDetail>();
}
