using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class Documenti
{
    public string IdinfoDocumenti { get; set; } = null!;

    public int IdinfoTabellaRegistri { get; set; }

    public string? Protocollo { get; set; }

    public int? IdinfoClientiFornitoriAgenti { get; set; }

    public int? IdinfoDestinazioneMerceClientiFornitoriAgenti { get; set; }

    public int IdinfoModalitaPagamento { get; set; }

    public int? IdinfoVettori { get; set; }

    public int? DocumentoRiferito { get; set; }

    public int? DocumentoGenerato { get; set; }

    public string? NumeroDocumento { get; set; }

    public string? LetteraDocumento { get; set; }

    public DateTime? DataDocumento { get; set; }

    public int? AnnoDocumento { get; set; }

    public string? Annotazioni { get; set; }

    public string? SpedizioneAmezzo { get; set; }

    public string? CausaleTrasporto { get; set; }

    public string? Porto { get; set; }

    public string? ColliAspetto { get; set; }

    public int? ColliNumero { get; set; }

    public double? Peso { get; set; }

    public int? NumeroDettagli { get; set; }

    public double? Sconto { get; set; }

    public double? TotaleIvaEsclusa { get; set; }

    public double? ImportoTotaleIva { get; set; }

    public double? TotaleDocumento { get; set; }

    public string? CodiceIva1 { get; set; }

    public int? AliquotaIva1 { get; set; }

    public string? DescrizioneIva1 { get; set; }

    public double? ImponibileIva1 { get; set; }

    public double? ImportoIva1 { get; set; }

    public string? CodiceIva2 { get; set; }

    public int? AliquotaIva2 { get; set; }

    public string? DescrizioneIva2 { get; set; }

    public double? ImponibileIva2 { get; set; }

    public double? ImportoIva2 { get; set; }

    public string? CodiceIva3 { get; set; }

    public int? AliquotaIva3 { get; set; }

    public string? DescrizioneIva3 { get; set; }

    public double? ImponibileIva3 { get; set; }

    public double? ImportoIva3 { get; set; }

    public string? CodiceIva4 { get; set; }

    public int? AliquotaIva4 { get; set; }

    public string? DescrizioneIva4 { get; set; }

    public double? ImponibileIva4 { get; set; }

    public double? ImportoIva4 { get; set; }

    public string? CodiceIva5 { get; set; }

    public int? AliquotaIva5 { get; set; }

    public string? DescrizioneIva5 { get; set; }

    public double? ImponibileIva5 { get; set; }

    public double? ImportoIva5 { get; set; }

    public string? NoteEsenzioneIva { get; set; }

    public string? NoteGeneriche { get; set; }

    public int? Annullato { get; set; }

    public int? Servizi { get; set; }

    public int? IdinfoMagazziniTrasferimentoOrigine { get; set; }

    public DateTime? DataOraEmissione { get; set; }

    public string? IntestazioneDocumento { get; set; }

    public int? IdinfoMagazziniTrasferimentoFine { get; set; }

    public int? IdinfoClientiFornitoriAgentiAgenti1 { get; set; }

    public int? IdinfoClientiFornitoriAgentiClientiAgentiProfiliProvv1 { get; set; }

    public int? IdinfoClientiFornitoriAgentiAgenti2 { get; set; }

    public int? IdinfoClientiFornitoriAgentiClientiAgentiProfiliProvv2 { get; set; }

    public int? IdinfoClientiFornitoriAgentiAgenti3 { get; set; }

    public int? IdinfoClientiFornitoriAgentiClientiAgentiProfiliProvv3 { get; set; }

    public int? IdinfoClientiFornitoriAgentiAgenti4 { get; set; }

    public int? IdinfoClientiFornitoriAgentiClientiAgentiProfiliProvv4 { get; set; }

    public int? StatoOrdine { get; set; }

    public string? NoteSpecifiche { get; set; }

    public int? ClientiFornitoriAgentiContiCorrentiContatoreUnivoco { get; set; }

    public int? NumeroOrdineWeb { get; set; }

    public int? StoreIdWeb { get; set; }

    public string? TrackingCorriere { get; set; }

    public string? TrackingNumber { get; set; }

    public string? TrackingLink { get; set; }

    public string? FenomeFileXml { get; set; }

    public string? FenumeroFattura { get; set; }

    public string? FeXml { get; set; }

    public string? ScontrinoNumero { get; set; }

    public DateTime? ScontrinoData { get; set; }

    public virtual ICollection<DocumentiRighe> DocumentiRighes { get; set; } = new List<DocumentiRighe>();

    public virtual ClientiFornitoriAgenti? ClientiFornitoriAgentis { get; set; }
}
