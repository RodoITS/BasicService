using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class Magazzini
{
    public int IdinfoMagazzini { get; set; }

    public string? Descrizione { get; set; }

    public int? Tipo { get; set; }

    public int? Stato { get; set; }

    public bool? Default { get; set; }
}
