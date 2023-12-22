using System;
using System.Collections.Generic;

namespace SalesSystem.Model;

public partial class DocumentNumber
{
    public int DocumentNumberId { get; set; }

    public int LastNumber { get; set; }

    public DateTime? RecordDate { get; set; }
}
