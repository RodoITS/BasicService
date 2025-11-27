using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class Matricole
{
    public int IdinfoMatricola { get; set; }

    public int? IdinfoArticoli { get; set; }

    public int? IdinfoVarianti { get; set; }

    public int? IdinfoVariantiComponenti { get; set; }

    public string? CodiceMagazzino { get; set; }

    public string? CodiceBarre { get; set; }
}
