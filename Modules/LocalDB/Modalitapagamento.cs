using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class Modalitapagamento
{
    public int IdinfoModalitaPagamento { get; set; }

    public string? ModalitaPagamento1 { get; set; }

    public short? NumeroRate { get; set; }

    public short? PrimaRata { get; set; }

    public int? GiornoScadenza { get; set; }

    public int? Tipo { get; set; }

    public string? MpfatturaElettronica { get; set; }
}
