using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class Vettori
{
    public int IdinfoVettori { get; set; }

    public string? Descrizione { get; set; }

    public int? IdinfoComuni { get; set; }

    public string? Via { get; set; }

    public string? Cap { get; set; }

    public string? Telefono { get; set; }

    public string? Fax { get; set; }

    public string? Mail { get; set; }

    public string? Citta { get; set; }

    public string? Provincia { get; set; }
}
