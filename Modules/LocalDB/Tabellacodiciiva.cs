using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class Tabellacodiciiva
{
    public int IdinfoTabellaCodiciIva { get; set; }

    public string? CodiceIva { get; set; }

    public int? AliquotaIva { get; set; }

    public string? Descrizione { get; set; }

    public short? Indetraibile { get; set; }

    public string? CodNatura { get; set; }

    public string? Esigibilita { get; set; }
}
