using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class ArticoliMateriePrime
{
    public int IdinfoArticoliProdottoFinito { get; set; }

    public int IdinfoArticoliMateriePrime { get; set; }

    public double? Quantita { get; set; }

    public double? PrezzoUnitario { get; set; }

    public int? Tipo { get; set; }
}
