using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class Articoli
{
    public int IdinfoArticoli { get; set; }

    public int? Idconto { get; set; }

    public int? IdcontoAcquisti { get; set; }

    public int? IdcontoResoCliente { get; set; }

    public int? IdcontoResoFornitore { get; set; }

    public int? IdcontoDebitoCliente { get; set; }

    public int? IdcontoDebitoFornitore { get; set; }

    public int? IdinfoClientiFornitoriAgenti { get; set; }

    public string? CodiceMagazzino { get; set; }

    public string? CodiceMagazzinoFornitore { get; set; }

    public string? CodiceBarre { get; set; }

    public string? DescrizionePrincipale { get; set; }

    public string? DescrizioneSecondaria1 { get; set; }

    public string? DescrizioneSecondaria2 { get; set; }

    public string? UnitaMisura { get; set; }

    public int? IdinfoTabellaCodiciIva { get; set; }

    public string? Immagine { get; set; }

    public int IdinfoArticoliCategorie { get; set; }

    public int IdinfoArticoliTipologie { get; set; }

    public double Peso { get; set; }

    public double ProvvigioneAgente { get; set; }

    public short? AnnoRiferimentoRegistrazione { get; set; }

    public int IdinfoVarianti { get; set; }

    public int? TipoArticolo { get; set; }

    public double? PrezzoUnitarioDistintaBase { get; set; }

    public int? ArticoloAttivo { get; set; }

    public int? ArticoloInSaldo { get; set; }

    public string? CodiceEsportazione { get; set; }

    public int? TempId { get; set; }

    public string? RaggruppamentoEtichetta { get; set; }

    public int? RaggruppamentoPredefinito { get; set; }

    public int? Reparto { get; set; }

    public int? MagazzinoStatoId { get; set; }
}
