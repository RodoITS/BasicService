using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class ClientiFornitoriAgenti
{
    public int IdinfoClientiFornitoriAgenti { get; set; }

    public string? Codice { get; set; }

    public string? Tipo { get; set; }

    public string? Cliente { get; set; }

    public string? ClienteSecondaRiga { get; set; }

    public string? Indirizzo { get; set; }

    public string? Citta { get; set; }

    public string? Provincia { get; set; }

    public string? Localita { get; set; }

    public string? Cap { get; set; }

    public string? Telefono { get; set; }

    public string? TelefonoCellulare { get; set; }

    public string? Fax { get; set; }

    public string? PostaElettronica { get; set; }

    public string? PartitaIva { get; set; }

    public string? Banca { get; set; }

    public string? Abi { get; set; }

    public string? Cab { get; set; }

    public int? IdinfoModalitaPagamento { get; set; }

    public int? IdinfoListini { get; set; }

    public double? ScontoMaggiorazione1 { get; set; }

    public double? ScontoMaggiorazione2 { get; set; }

    public double? ScontoMaggiorazione3 { get; set; }

    public string? Stato { get; set; }

    public bool EsenteIva { get; set; }

    public string? DescrizioneEsenzioneIva { get; set; }

    public string? EstremiEsenzioneIva { get; set; }

    public string? NoteGeneriche { get; set; }

    public bool? VisualizzaNoteSuDocumenti { get; set; }

    public string? CodiceFiscale { get; set; }

    public string? Riferimento { get; set; }

    public string? IntestazioneDocumento { get; set; }

    public int? IdinfoComuni { get; set; }

    public string? NoteCliente { get; set; }

    public int? IdinfoClientiFornitoriAgentiProvvigioniPeriodoCalcolo { get; set; }

    public int? IdinfoTabellaCodiciIva { get; set; }

    public string? Pec { get; set; }

    public string? Sdi { get; set; }

    public string? SitoWeb { get; set; }

    public bool? FlgPubblicaAmministrazione { get; set; }

    public virtual ICollection<Documenti> Documentis { get; set; } = new List<Documenti>();
}
