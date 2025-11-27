using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class DestinazionemerceclientiFornitoriAgenti
{
    public int IdinfoDestinazioneMerceClientiFornitoriAgenti { get; set; }

    public int IdinfoClientiFornitoriAgenti { get; set; }

    public string? Destinazione { get; set; }

    public int? IdinfoComuni { get; set; }

    public string? Indirizzo { get; set; }

    public string? Cap { get; set; }

    public string? Citta { get; set; }

    public string? Provincia { get; set; }

    public string? Stato { get; set; }
}
