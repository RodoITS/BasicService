using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class DocumentiRighe
{
    public string IdinfoDocumenti { get; set; } = null!;

    public int IdinfoTabellaRegistri { get; set; }

    public string? Protocollo { get; set; }

    public int NumeroRiga { get; set; }

    public int? IdinfoClientiFornitoriAgenti { get; set; }

    public int? IdinfoArticoli { get; set; }

    public string? CodiceMagazzino { get; set; }

    public int? IdinfoTabellaCodiciIva { get; set; }

    public int? IdinfoListini { get; set; }

    public DateTime? DataDocumento { get; set; }

    public string? Descrizione { get; set; }

    public double? Quantita { get; set; }

    public double? PrezzoUnitario { get; set; }

    public int? ScontoMaggiorazione1 { get; set; }

    public int? ScontoMaggiorazione2 { get; set; }

    public int? ScontoMaggiorazione3 { get; set; }

    public double? TotaleRiga { get; set; }

    public string? NumeroDocumento { get; set; }

    public string? LetteraDocumento { get; set; }

    public int? Idconto { get; set; }

    public short? Differita { get; set; }

    public string? NoteRiga { get; set; }

    public string? Raggruppamento { get; set; }

    public double Quantita2 { get; set; }

    public string? RiferimentoIdinfoDocumenti { get; set; }

    public int RiferimentoIdinfoTabellaRegistri { get; set; }

    public string? RiferimentoProtocollo { get; set; }

    public int RiferimentoNumeroRiga { get; set; }

    public bool RiferimentoScaricoTotale { get; set; }

    public string? UnitaMisura { get; set; }

    public string? TipoDocumentoRifPa { get; set; }

    public int? IddocumentoRifPa { get; set; }

    public DateTime? DataDocumentoRifPa { get; set; }

    public string? NumItemDocPa { get; set; }

    public string? Cig { get; set; }

    public string? Cup { get; set; }

    public string? CommessaConvenzionePa { get; set; }

    public virtual Documenti Idinfo { get; set; } = null!;
}
