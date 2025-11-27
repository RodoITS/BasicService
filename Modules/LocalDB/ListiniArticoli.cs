using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class ListiniArticoli
{
    public int IdinfoListini { get; set; }

    public int IdinfoArticoli { get; set; }

    public double? Prezzo { get; set; }

    public int? ScontoMaggiorazione1 { get; set; }

    public int? ScontoMaggiorazione2 { get; set; }

    public int? ScontoMaggiorazione3 { get; set; }

    public virtual Listini Listinis { get; set; } = null!;
}
