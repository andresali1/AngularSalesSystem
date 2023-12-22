using System;
using System.Collections.Generic;

namespace SalesSystem.Model;

public partial class Sale
{
    public int SaleId { get; set; }

    public string? DocumentNumber { get; set; }

    public string? PaymentType { get; set; }

    public decimal? Total { get; set; }

    public DateTime? RecordDate { get; set; }

    public virtual ICollection<SaleDetail> SaleDetails { get; } = new List<SaleDetail>();
}
