using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BasicService.Modules.LocalDB;

public partial class DblocaleContext : DbContext
{
    public DblocaleContext()
    {
    }

    public DblocaleContext(DbContextOptions<DblocaleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Articoli> Articolis { get; set; }

    public virtual DbSet<ArticoliCategorie> ArticoliCategories { get; set; }

    public virtual DbSet<ArticoliMateriePrime> ArticoliMateriePrimes { get; set; }

    public virtual DbSet<ArticoliPrezzomedio> ArticoliPrezzomedios { get; set; }

    public virtual DbSet<ArticoliTipologie> ArticoliTipologies { get; set; }

    public virtual DbSet<ArticoliUm> Articolia { get; set; }

    public virtual DbSet<Banche> Banches { get; set; }

    public virtual DbSet<ClientiFornitoriAgenti> ClientiFornitoriAgentis { get; set; }

    public virtual DbSet<Comuni> Comunis { get; set; }

    public virtual DbSet<DestinazionemerceclientiFornitoriAgenti> DestinazionemerceclientiFornitoriAgentis { get; set; }

    public virtual DbSet<Documenti> Documentis { get; set; }

    public virtual DbSet<DocumentiAspettocolli> DocumentiAspettocollis { get; set; }

    public virtual DbSet<DocumentiCausali> DocumentiCausalis { get; set; }

    public virtual DbSet<DocumentiRighe> DocumentiRighes { get; set; }

    public virtual DbSet<EcommerceArticoli> EcommerceArticolis { get; set; }

    public virtual DbSet<EcommerceArticoliEliminati> EcommerceArticoliEliminatis { get; set; }

    public virtual DbSet<EcommerceArticoliInevidenza> EcommerceArticoliInevidenzas { get; set; }

    public virtual DbSet<EcommerceCategorie> EcommerceCategories { get; set; }

    public virtual DbSet<EcommerceTipologie> EcommerceTipologies { get; set; }

    public virtual DbSet<Listini> Listinis { get; set; }

    public virtual DbSet<ListiniArticoli> ListiniArticolis { get; set; }

    public virtual DbSet<Magazzini> Magazzinis { get; set; }

    public virtual DbSet<Matricole> Matricoles { get; set; }

    public virtual DbSet<Modalitapagamento> Modalitapagamentos { get; set; }

    public virtual DbSet<Pianodeiconti> Pianodeicontis { get; set; }

    public virtual DbSet<PianodeicontiNome> PianodeicontiNomes { get; set; }

    public virtual DbSet<Progressivi> Progressivis { get; set; }

    public virtual DbSet<Tabellacodiciiva> Tabellacodiciivas { get; set; }

    public virtual DbSet<Tabellaregistri> Tabellaregistris { get; set; }

    public virtual DbSet<TabellaregistriLetteredocumento> TabellaregistriLetteredocumentos { get; set; }

    public virtual DbSet<VarianteAbbigliamentoDonna> VarianteAbbigliamentoDonnas { get; set; }

    public virtual DbSet<VarianteAbbigliamentoDonnaDifferiti> VarianteAbbigliamentoDonnaDifferitis { get; set; }

    public virtual DbSet<VarianteAbbigliamentoDonnaMovimentiMagazzino> VarianteAbbigliamentoDonnaMovimentiMagazzinos { get; set; }

    public virtual DbSet<VarianteAbbigliamentoDonnaQuantitum> VarianteAbbigliamentoDonnaQuantita { get; set; }

    public virtual DbSet<VarianteAbbigliamentoUomo> VarianteAbbigliamentoUomos { get; set; }

    public virtual DbSet<VarianteAbbigliamentoUomoDifferiti> VarianteAbbigliamentoUomoDifferitis { get; set; }

    public virtual DbSet<VarianteAbbigliamentoUomoMovimentiMagazzino> VarianteAbbigliamentoUomoMovimentiMagazzinos { get; set; }

    public virtual DbSet<VarianteAbbigliamentoUomoQuantitum> VarianteAbbigliamentoUomoQuantita { get; set; }

    public virtual DbSet<Varianti> Variantis { get; set; }

    public virtual DbSet<VariantiComponenti> VariantiComponentis { get; set; }

    public virtual DbSet<VariantiJoinVariantiComponenti> VariantiJoinVariantiComponentis { get; set; }

    public virtual DbSet<Vettori> Vettoris { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;database=dblocale;user id=root;password=Alcor@23011999;port=3306", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.4.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Articoli>(entity =>
        {
            entity.HasKey(e => e.IdinfoArticoli).HasName("PRIMARY");

            entity.ToTable("articoli");

            entity.HasIndex(e => e.IdinfoArticoliCategorie, "articoli_articoli_categorie_FK");

            entity.HasIndex(e => e.IdinfoArticoliTipologie, "articoli_articoli_tipologie_FK");

            entity.Property(e => e.IdinfoArticoli)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_Articoli");
            entity.Property(e => e.ArticoloAttivo).HasColumnName("Articolo_Attivo");
            entity.Property(e => e.ArticoloInSaldo).HasColumnName("Articolo_In_Saldo");
            entity.Property(e => e.CodiceBarre).HasMaxLength(25);
            entity.Property(e => e.CodiceEsportazione)
                .HasMaxLength(50)
                .HasColumnName("Codice_Esportazione");
            entity.Property(e => e.CodiceMagazzino).HasMaxLength(15);
            entity.Property(e => e.CodiceMagazzinoFornitore).HasMaxLength(15);
            entity.Property(e => e.DescrizionePrincipale).HasMaxLength(50);
            entity.Property(e => e.DescrizioneSecondaria1).HasColumnType("text");
            entity.Property(e => e.DescrizioneSecondaria2).HasMaxLength(255);
            entity.Property(e => e.Idconto).HasColumnName("IDConto");
            entity.Property(e => e.IdcontoAcquisti).HasColumnName("IDConto_Acquisti");
            entity.Property(e => e.IdcontoDebitoCliente).HasColumnName("IDConto_Debito_Cliente");
            entity.Property(e => e.IdcontoDebitoFornitore).HasColumnName("IDConto_Debito_Fornitore");
            entity.Property(e => e.IdcontoResoCliente).HasColumnName("IDConto_Reso_Cliente");
            entity.Property(e => e.IdcontoResoFornitore).HasColumnName("IDConto_Reso_Fornitore");
            entity.Property(e => e.IdinfoArticoliCategorie).HasColumnName("IDInfo_Articoli_Categorie");
            entity.Property(e => e.IdinfoArticoliTipologie).HasColumnName("IDInfo_Articoli_Tipologie");
            entity.Property(e => e.IdinfoClientiFornitoriAgenti).HasColumnName("IDInfo_Clienti_Fornitori_Agenti");
            entity.Property(e => e.IdinfoTabellaCodiciIva).HasColumnName("IDInfo_TabellaCodiciIva");
            entity.Property(e => e.IdinfoVarianti).HasColumnName("IDInfo_Varianti");
            entity.Property(e => e.Immagine).HasColumnType("text");
            entity.Property(e => e.RaggruppamentoEtichetta)
                .HasMaxLength(50)
                .HasColumnName("Raggruppamento_Etichetta");
            entity.Property(e => e.RaggruppamentoPredefinito).HasColumnName("Raggruppamento_Predefinito");
            entity.Property(e => e.TempId).HasColumnName("TempID");
            entity.Property(e => e.TipoArticolo).HasColumnName("Tipo_Articolo");
            entity.Property(e => e.UnitaMisura).HasMaxLength(5);

        });

        modelBuilder.Entity<ArticoliCategorie>(entity =>
        {
            entity.HasKey(e => e.IdinfoArticoliCategorie).HasName("PRIMARY");

            entity.ToTable("articoli_categorie");

            entity.Property(e => e.IdinfoArticoliCategorie)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_Articoli_Categorie");
            entity.Property(e => e.Descrizione).HasMaxLength(50);
            entity.Property(e => e.Idpadre).HasColumnName("IDPadre");
            entity.Property(e => e.Immagine).HasColumnType("text");
            entity.Property(e => e.TempId).HasColumnName("TempID");
        });

        modelBuilder.Entity<ArticoliMateriePrime>(entity =>
        {
            entity.HasKey(e => e.IdinfoArticoliMateriePrime).HasName("PRIMARY");

            entity.ToTable("articoli_materie_prime");

            entity.Property(e => e.IdinfoArticoliMateriePrime)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_Articoli_Materie_Prime");
            entity.Property(e => e.IdinfoArticoliProdottoFinito).HasColumnName("IDInfo_Articoli_Prodotto_Finito");
        });

        modelBuilder.Entity<ArticoliPrezzomedio>(entity =>
        {
            entity.HasKey(e => e.IdinfoArticoli).HasName("PRIMARY");

            entity.ToTable("articoli_prezzomedio");

            entity.Property(e => e.IdinfoArticoli)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_Articoli");
            entity.Property(e => e.DataCalcolo).HasColumnType("timestamp");
        });

        modelBuilder.Entity<ArticoliTipologie>(entity =>
        {
            entity.HasKey(e => e.IdinfoArticoliTipologie).HasName("PRIMARY");

            entity.ToTable("articoli_tipologie");

            entity.Property(e => e.IdinfoArticoliTipologie)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_Articoli_Tipologie");
            entity.Property(e => e.Descrizione).HasMaxLength(50);
            entity.Property(e => e.Idpadre).HasColumnName("IDPadre");
            entity.Property(e => e.Immagine).HasColumnType("text");
            entity.Property(e => e.TempId).HasColumnName("TempID");
        });

        modelBuilder.Entity<ArticoliUm>(entity =>
        {
            entity.HasKey(e => e.IdinfoArticoliUm).HasName("PRIMARY");

            entity.ToTable("articoli_um");

            entity.Property(e => e.IdinfoArticoliUm)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_Articoli_UM");
            entity.Property(e => e.Descrizione).HasMaxLength(5);
        });

        modelBuilder.Entity<Banche>(entity =>
        {
            entity.HasKey(e => e.IdinfoBanche).HasName("PRIMARY");

            entity.ToTable("banche");

            entity.Property(e => e.IdinfoBanche)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_Banche");
            entity.Property(e => e.Abi)
                .HasMaxLength(5)
                .HasColumnName("ABI");
            entity.Property(e => e.NomeBanca).HasMaxLength(50);
            entity.Property(e => e.Note).HasColumnType("text");
        });

        modelBuilder.Entity<ClientiFornitoriAgenti>(entity =>
        {
            entity.HasKey(e => e.IdinfoClientiFornitoriAgenti).HasName("PRIMARY");

            entity.ToTable("clienti_fornitori_agenti");

            entity.Property(e => e.IdinfoClientiFornitoriAgenti)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_Clienti_Fornitori_Agenti");
            entity.Property(e => e.Abi)
                .HasMaxLength(8)
                .HasColumnName("ABI");
            entity.Property(e => e.Banca).HasMaxLength(255);
            entity.Property(e => e.Cab)
                .HasMaxLength(8)
                .HasColumnName("CAB");
            entity.Property(e => e.Cap).HasMaxLength(10);
            entity.Property(e => e.Citta).HasMaxLength(250);
            entity.Property(e => e.Cliente).HasMaxLength(100);
            entity.Property(e => e.ClienteSecondaRiga).HasMaxLength(100);
            entity.Property(e => e.Codice).HasMaxLength(10);
            entity.Property(e => e.CodiceFiscale).HasMaxLength(30);
            entity.Property(e => e.DescrizioneEsenzioneIva)
                .HasMaxLength(255)
                .HasColumnName("DescrizioneEsenzioneIVA");
            entity.Property(e => e.EsenteIva).HasColumnName("EsenteIVA");
            entity.Property(e => e.EstremiEsenzioneIva)
                .HasMaxLength(255)
                .HasColumnName("EstremiEsenzioneIVA");
            entity.Property(e => e.Fax).HasMaxLength(255);
            entity.Property(e => e.IdinfoClientiFornitoriAgentiProvvigioniPeriodoCalcolo).HasColumnName("IDInfo_Clienti_Fornitori_Agenti_Provvigioni_Periodo_Calcolo");
            entity.Property(e => e.IdinfoComuni).HasColumnName("IDInfo_Comuni");
            entity.Property(e => e.IdinfoListini).HasColumnName("IDInfo_Listini");
            entity.Property(e => e.IdinfoModalitaPagamento).HasColumnName("IDInfo_ModalitaPagamento");
            entity.Property(e => e.IdinfoTabellaCodiciIva).HasColumnName("IDInfo_TabellaCodiciIva");
            entity.Property(e => e.Indirizzo).HasMaxLength(50);
            entity.Property(e => e.IntestazioneDocumento).HasColumnType("text");
            entity.Property(e => e.Localita).HasMaxLength(255);
            entity.Property(e => e.NoteCliente).HasColumnType("text");
            entity.Property(e => e.NoteGeneriche).HasMaxLength(255);
            entity.Property(e => e.PartitaIva).HasMaxLength(20);
            entity.Property(e => e.Pec)
                .HasMaxLength(45)
                .HasColumnName("PEC");
            entity.Property(e => e.PostaElettronica).HasMaxLength(255);
            entity.Property(e => e.Provincia).HasMaxLength(5);
            entity.Property(e => e.Riferimento).HasMaxLength(100);
            entity.Property(e => e.Sdi)
                .HasMaxLength(20)
                .HasColumnName("SDI");
            entity.Property(e => e.SitoWeb).HasMaxLength(255);
            entity.Property(e => e.Stato).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(255);
            entity.Property(e => e.TelefonoCellulare).HasMaxLength(255);
            entity.Property(e => e.Tipo).HasMaxLength(1);
        });

        modelBuilder.Entity<Comuni>(entity =>
        {
            entity.HasKey(e => e.IdinfoComuni).HasName("PRIMARY");

            entity.ToTable("comuni");

            entity.Property(e => e.IdinfoComuni)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_Comuni");
            entity.Property(e => e.Cap)
                .HasMaxLength(10)
                .HasColumnName("CAP");
            entity.Property(e => e.CodiceFiscale).HasMaxLength(4);
            entity.Property(e => e.Comune).HasMaxLength(50);
            entity.Property(e => e.Prefisso).HasMaxLength(4);
            entity.Property(e => e.Provincia).HasMaxLength(2);
            entity.Property(e => e.UfficioIva)
                .HasMaxLength(3)
                .HasColumnName("UfficioIVA");
        });

        modelBuilder.Entity<DestinazionemerceclientiFornitoriAgenti>(entity =>
        {
            entity.HasKey(e => e.IdinfoDestinazioneMerceClientiFornitoriAgenti).HasName("PRIMARY");

            entity.ToTable("destinazionemerceclienti_fornitori_agenti");

            entity.Property(e => e.IdinfoDestinazioneMerceClientiFornitoriAgenti)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_DestinazioneMerceClienti_Fornitori_Agenti");
            entity.Property(e => e.Cap)
                .HasMaxLength(10)
                .HasColumnName("CAP");
            entity.Property(e => e.Citta).HasMaxLength(255);
            entity.Property(e => e.Destinazione).HasMaxLength(100);
            entity.Property(e => e.IdinfoClientiFornitoriAgenti).HasColumnName("IDInfo_Clienti_Fornitori_Agenti");
            entity.Property(e => e.IdinfoComuni).HasColumnName("IDInfo_Comuni");
            entity.Property(e => e.Indirizzo).HasMaxLength(100);
            entity.Property(e => e.Provincia).HasMaxLength(5);
            entity.Property(e => e.Stato).HasMaxLength(50);
        });

        modelBuilder.Entity<Documenti>(entity =>
        {
            entity.HasKey(e => new { e.IdinfoDocumenti, e.IdinfoTabellaRegistri })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("documenti");

            entity.HasIndex(e => e.IdinfoClientiFornitoriAgenti, "documenti_clienti_fornitori_agenti_FK");

            entity.Property(e => e.IdinfoDocumenti)
                .HasMaxLength(15)
                .HasColumnName("IDInfo_Documenti");
            entity.Property(e => e.IdinfoTabellaRegistri).HasColumnName("IDInfo_TabellaRegistri");
            entity.Property(e => e.Annotazioni).HasMaxLength(50);
            entity.Property(e => e.CausaleTrasporto).HasMaxLength(50);
            entity.Property(e => e.ClientiFornitoriAgentiContiCorrentiContatoreUnivoco).HasColumnName("Clienti_Fornitori_Agenti_Conti_Correnti_Contatore_Univoco");
            entity.Property(e => e.CodiceIva1).HasMaxLength(2);
            entity.Property(e => e.CodiceIva2).HasMaxLength(2);
            entity.Property(e => e.CodiceIva3).HasMaxLength(2);
            entity.Property(e => e.CodiceIva4).HasMaxLength(2);
            entity.Property(e => e.CodiceIva5).HasMaxLength(2);
            entity.Property(e => e.ColliAspetto).HasMaxLength(50);
            entity.Property(e => e.DataDocumento).HasColumnType("timestamp");
            entity.Property(e => e.DataOraEmissione)
                .HasColumnType("timestamp")
                .HasColumnName("Data_Ora_Emissione");
            entity.Property(e => e.DescrizioneIva1).HasMaxLength(30);
            entity.Property(e => e.DescrizioneIva2).HasMaxLength(30);
            entity.Property(e => e.DescrizioneIva3).HasMaxLength(30);
            entity.Property(e => e.DescrizioneIva4).HasMaxLength(30);
            entity.Property(e => e.DescrizioneIva5).HasMaxLength(30);
            entity.Property(e => e.FeXml)
                .HasColumnType("text")
                .HasColumnName("FE_XML");
            entity.Property(e => e.FenomeFileXml)
                .HasMaxLength(255)
                .HasColumnName("FENomeFileXML");
            entity.Property(e => e.FenumeroFattura)
                .HasMaxLength(15)
                .HasColumnName("FENumeroFattura");
            entity.Property(e => e.IdinfoClientiFornitoriAgenti).HasColumnName("IDInfo_Clienti_Fornitori_Agenti");
            entity.Property(e => e.IdinfoClientiFornitoriAgentiAgenti1).HasColumnName("IDInfo_Clienti_Fornitori_Agenti_AGENTI_1");
            entity.Property(e => e.IdinfoClientiFornitoriAgentiAgenti2).HasColumnName("IDInfo_Clienti_Fornitori_Agenti_AGENTI_2");
            entity.Property(e => e.IdinfoClientiFornitoriAgentiAgenti3).HasColumnName("IDInfo_Clienti_Fornitori_Agenti_AGENTI_3");
            entity.Property(e => e.IdinfoClientiFornitoriAgentiAgenti4).HasColumnName("IDInfo_Clienti_Fornitori_Agenti_AGENTI_4");
            entity.Property(e => e.IdinfoClientiFornitoriAgentiClientiAgentiProfiliProvv1).HasColumnName("IDInfo_Clienti_Fornitori_Agenti_CLIENTI_AGENTI_PROFILI_PROVV_1");
            entity.Property(e => e.IdinfoClientiFornitoriAgentiClientiAgentiProfiliProvv2).HasColumnName("IDInfo_Clienti_Fornitori_Agenti_CLIENTI_AGENTI_PROFILI_PROVV_2");
            entity.Property(e => e.IdinfoClientiFornitoriAgentiClientiAgentiProfiliProvv3).HasColumnName("IDInfo_Clienti_Fornitori_Agenti_CLIENTI_AGENTI_PROFILI_PROVV_3");
            entity.Property(e => e.IdinfoClientiFornitoriAgentiClientiAgentiProfiliProvv4).HasColumnName("IDInfo_Clienti_Fornitori_Agenti_CLIENTI_AGENTI_PROFILI_PROVV_4");
            entity.Property(e => e.IdinfoDestinazioneMerceClientiFornitoriAgenti).HasColumnName("IDInfo_DestinazioneMerceClienti_Fornitori_Agenti");
            entity.Property(e => e.IdinfoMagazziniTrasferimentoFine).HasColumnName("IDInfo_Magazzini_Trasferimento_Fine");
            entity.Property(e => e.IdinfoMagazziniTrasferimentoOrigine).HasColumnName("IDInfo_Magazzini_Trasferimento_Origine");
            entity.Property(e => e.IdinfoModalitaPagamento).HasColumnName("IDInfo_ModalitaPagamento");
            entity.Property(e => e.IdinfoVettori).HasColumnName("IDInfo_Vettori");
            entity.Property(e => e.IntestazioneDocumento).HasColumnType("text");
            entity.Property(e => e.LetteraDocumento).HasMaxLength(1);
            entity.Property(e => e.NoteEsenzioneIva)
                .HasColumnType("text")
                .HasColumnName("NoteEsenzioneIVA");
            entity.Property(e => e.NoteGeneriche).HasColumnType("text");
            entity.Property(e => e.NoteSpecifiche)
                .HasColumnType("text")
                .HasColumnName("Note_specifiche");
            entity.Property(e => e.NumeroDocumento).HasMaxLength(9);
            entity.Property(e => e.NumeroOrdineWeb).HasColumnName("NumeroOrdine_Web");
            entity.Property(e => e.Porto).HasMaxLength(50);
            entity.Property(e => e.Protocollo).HasMaxLength(10);
            entity.Property(e => e.ScontrinoData).HasColumnType("timestamp");
            entity.Property(e => e.ScontrinoNumero).HasMaxLength(255);
            entity.Property(e => e.SpedizioneAmezzo)
                .HasMaxLength(50)
                .HasColumnName("SpedizioneAMezzo");
            entity.Property(e => e.StatoOrdine).HasColumnName("Stato_Ordine");
            entity.Property(e => e.StoreIdWeb).HasColumnName("StoreId_Web");
            entity.Property(e => e.TrackingCorriere)
                .HasMaxLength(20)
                .HasColumnName("Tracking_Corriere");
            entity.Property(e => e.TrackingLink)
                .HasMaxLength(255)
                .HasColumnName("Tracking_Link");
            entity.Property(e => e.TrackingNumber)
                .HasMaxLength(20)
                .HasColumnName("Tracking_Number");

            entity.HasOne(d => d.IdinfoClientiFornitoriAgentiNavigation).WithMany(p => p.Documentis)
                .HasForeignKey(d => d.IdinfoClientiFornitoriAgenti)
                .HasConstraintName("documenti_clienti_fornitori_agenti_FK");
        });

        modelBuilder.Entity<DocumentiAspettocolli>(entity =>
        {
            entity.HasKey(e => e.IdinfoDocumentiAspettoColli).HasName("PRIMARY");

            entity.ToTable("documenti_aspettocolli");

            entity.Property(e => e.IdinfoDocumentiAspettoColli)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_Documenti_AspettoColli");
            entity.Property(e => e.Descrizione).HasMaxLength(50);
        });

        modelBuilder.Entity<DocumentiCausali>(entity =>
        {
            entity.HasKey(e => e.IdinfoDocumentiCausali).HasName("PRIMARY");

            entity.ToTable("documenti_causali");

            entity.Property(e => e.IdinfoDocumentiCausali)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_Documenti_Causali");
            entity.Property(e => e.Descrizione).HasMaxLength(50);
        });

        modelBuilder.Entity<DocumentiRighe>(entity =>
        {
            entity.HasKey(e => new { e.IdinfoDocumenti, e.IdinfoTabellaRegistri, e.NumeroRiga })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("documenti_righe");

            entity.Property(e => e.IdinfoDocumenti)
                .HasMaxLength(15)
                .HasColumnName("IDInfo_Documenti");
            entity.Property(e => e.IdinfoTabellaRegistri).HasColumnName("IDInfo_TabellaRegistri");
            entity.Property(e => e.Cig)
                .HasMaxLength(255)
                .HasColumnName("CIG");
            entity.Property(e => e.CodiceMagazzino).HasMaxLength(15);
            entity.Property(e => e.CommessaConvenzionePa)
                .HasMaxLength(255)
                .HasColumnName("CommessaConvenzionePA");
            entity.Property(e => e.Cup)
                .HasMaxLength(255)
                .HasColumnName("CUP");
            entity.Property(e => e.DataDocumento).HasColumnType("timestamp");
            entity.Property(e => e.DataDocumentoRifPa)
                .HasColumnType("timestamp")
                .HasColumnName("DataDocumentoRifPA");
            entity.Property(e => e.Descrizione).HasMaxLength(255);
            entity.Property(e => e.Idconto).HasColumnName("IDConto");
            entity.Property(e => e.IddocumentoRifPa).HasColumnName("IDDocumentoRifPA");
            entity.Property(e => e.IdinfoArticoli).HasColumnName("IDInfo_Articoli");
            entity.Property(e => e.IdinfoClientiFornitoriAgenti).HasColumnName("IDInfo_Clienti_Fornitori_Agenti");
            entity.Property(e => e.IdinfoListini).HasColumnName("IDInfo_Listini");
            entity.Property(e => e.IdinfoTabellaCodiciIva).HasColumnName("IDInfo_TabellaCodiciIva");
            entity.Property(e => e.LetteraDocumento).HasMaxLength(1);
            entity.Property(e => e.NoteRiga)
                .HasMaxLength(255)
                .HasColumnName("Note_Riga");
            entity.Property(e => e.NumItemDocPa)
                .HasMaxLength(255)
                .HasColumnName("NumItemDocPA");
            entity.Property(e => e.NumeroDocumento).HasMaxLength(9);
            entity.Property(e => e.Protocollo).HasMaxLength(10);
            entity.Property(e => e.Raggruppamento).HasMaxLength(255);
            entity.Property(e => e.RiferimentoIdinfoDocumenti)
                .HasMaxLength(15)
                .HasColumnName("Riferimento_IDInfo_Documenti");
            entity.Property(e => e.RiferimentoIdinfoTabellaRegistri).HasColumnName("Riferimento_IDInfo_TabellaRegistri");
            entity.Property(e => e.RiferimentoNumeroRiga).HasColumnName("Riferimento_NumeroRiga");
            entity.Property(e => e.RiferimentoProtocollo)
                .HasMaxLength(10)
                .HasColumnName("Riferimento_Protocollo");
            entity.Property(e => e.RiferimentoScaricoTotale).HasColumnName("Riferimento_Scarico_Totale");
            entity.Property(e => e.TipoDocumentoRifPa)
                .HasMaxLength(255)
                .HasColumnName("TipoDocumentoRifPA");
            entity.Property(e => e.UnitaMisura).HasMaxLength(5);

            entity.HasOne(d => d.Idinfo).WithMany(p => p.DocumentiRighes)
                .HasForeignKey(d => new { d.IdinfoDocumenti, d.IdinfoTabellaRegistri })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("documenti_righe_documenti_FK");
        });

        modelBuilder.Entity<EcommerceArticoli>(entity =>
        {
            entity.HasKey(e => new { e.IdStore, e.IdInfoArticoli })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("ecommerce_articoli");

            entity.Property(e => e.IdInfoArticoli).HasColumnName("IdInfo_Articoli");
            entity.Property(e => e.Createdon)
                .HasColumnType("datetime")
                .HasColumnName("createdon");
            entity.Property(e => e.DataOraModifica).HasColumnType("timestamp");
            entity.Property(e => e.Idremoto).HasColumnName("idremoto");
            entity.Property(e => e.Updatedon)
                .HasColumnType("datetime")
                .HasColumnName("updatedon");
        });

        modelBuilder.Entity<EcommerceArticoliEliminati>(entity =>
        {
            entity.HasKey(e => e.IdInfoArticoli).HasName("PRIMARY");

            entity.ToTable("ecommerce_articoli_eliminati");

            entity.Property(e => e.IdInfoArticoli)
                .ValueGeneratedNever()
                .HasColumnName("IdInfo_Articoli");
        });

        modelBuilder.Entity<EcommerceArticoliInevidenza>(entity =>
        {
            entity.HasKey(e => new { e.IdStore, e.IdInfoArticoli })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("ecommerce_articoli_inevidenza");

            entity.Property(e => e.IdInfoArticoli).HasColumnName("IdInfo_Articoli");
        });

        modelBuilder.Entity<EcommerceCategorie>(entity =>
        {
            entity.HasKey(e => new { e.IdInfoArticoliCategorie, e.IdStore })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("ecommerce_categorie");

            entity.Property(e => e.IdInfoArticoliCategorie).HasColumnName("IdInfo_Articoli_Categorie");
            entity.Property(e => e.Createdon)
                .HasColumnType("datetime")
                .HasColumnName("createdon");
            entity.Property(e => e.Idremoto).HasColumnName("idremoto");
            entity.Property(e => e.Updatedon)
                .HasColumnType("datetime")
                .HasColumnName("updatedon");
        });

        modelBuilder.Entity<EcommerceTipologie>(entity =>
        {
            entity.HasKey(e => new { e.IdStore, e.IdInfoArticoliTipologie })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("ecommerce_tipologie");

            entity.Property(e => e.IdInfoArticoliTipologie).HasColumnName("IdInfo_Articoli_Tipologie");
            entity.Property(e => e.Createdon)
                .HasColumnType("datetime")
                .HasColumnName("createdon");
            entity.Property(e => e.Idremoto).HasColumnName("idremoto");
            entity.Property(e => e.Updatedon)
                .HasColumnType("datetime")
                .HasColumnName("updatedon");
        });

        modelBuilder.Entity<Listini>(entity =>
        {
            entity.HasKey(e => e.IdinfoListini).HasName("PRIMARY");

            entity.ToTable("listini");

            entity.Property(e => e.IdinfoListini)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_Listini");
            entity.Property(e => e.Descrizione).HasMaxLength(50);
            entity.Property(e => e.Idpadre).HasColumnName("IDPadre");
        });

        modelBuilder.Entity<ListiniArticoli>(entity =>
        {
            entity.HasKey(e => new { e.IdinfoListini, e.IdinfoArticoli })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("listini_articoli");

            entity.Property(e => e.IdinfoListini).HasColumnName("IDInfo_Listini");
            entity.Property(e => e.IdinfoArticoli).HasColumnName("IDInfo_Articoli");

            entity.HasOne(d => d.IdinfoListiniNavigation).WithMany(p => p.ListiniArticolis)
                .HasForeignKey(d => d.IdinfoListini)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("listini_articoli_listini_FK");
        });

        modelBuilder.Entity<Magazzini>(entity =>
        {
            entity.HasKey(e => e.IdinfoMagazzini).HasName("PRIMARY");

            entity.ToTable("magazzini");

            entity.Property(e => e.IdinfoMagazzini)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_Magazzini");
            entity.Property(e => e.Descrizione).HasMaxLength(100);
        });

        modelBuilder.Entity<Matricole>(entity =>
        {
            entity.HasKey(e => e.IdinfoMatricola).HasName("PRIMARY");

            entity.ToTable("matricole");

            entity.Property(e => e.IdinfoMatricola)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_Matricola");
            entity.Property(e => e.CodiceBarre).HasMaxLength(50);
            entity.Property(e => e.CodiceMagazzino).HasMaxLength(30);
            entity.Property(e => e.IdinfoArticoli).HasColumnName("IDInfo_Articoli");
            entity.Property(e => e.IdinfoVarianti).HasColumnName("IDInfo_Varianti");
            entity.Property(e => e.IdinfoVariantiComponenti).HasColumnName("IDInfo_Varianti_Componenti");
        });

        modelBuilder.Entity<Modalitapagamento>(entity =>
        {
            entity.HasKey(e => e.IdinfoModalitaPagamento).HasName("PRIMARY");

            entity.ToTable("modalitapagamento");

            entity.Property(e => e.IdinfoModalitaPagamento)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_ModalitaPagamento");
            entity.Property(e => e.ModalitaPagamento1)
                .HasMaxLength(100)
                .HasColumnName("ModalitaPagamento");
            entity.Property(e => e.MpfatturaElettronica)
                .HasMaxLength(4)
                .HasColumnName("MPFatturaElettronica");
        });

        modelBuilder.Entity<Pianodeiconti>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("pianodeiconti");

            entity.Property(e => e.Codice).HasMaxLength(10);
            entity.Property(e => e.Descrizione).HasMaxLength(250);
            entity.Property(e => e.IdinfoPianoDeiConti).HasColumnName("IDInfo_PianoDeiConti");
            entity.Property(e => e.IdinfoPianoDeiContiNome).HasColumnName("IDInfo_PianoDeiConti_Nome");
            entity.Property(e => e.Idpadre).HasColumnName("IDPadre");
        });

        modelBuilder.Entity<PianodeicontiNome>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("pianodeiconti_nome");

            entity.Property(e => e.IdinfoPianoDeiContiNome).HasColumnName("IDInfo_PianoDeiConti_Nome");
            entity.Property(e => e.Nome).HasMaxLength(255);
        });

        modelBuilder.Entity<Progressivi>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("progressivi");

            entity.Property(e => e.IdinfoProgressivi).HasColumnName("IDInfo_Progressivi");
            entity.Property(e => e.NomeCampo).HasMaxLength(255);
            entity.Property(e => e.Valore).HasMaxLength(100);
        });

        modelBuilder.Entity<Tabellacodiciiva>(entity =>
        {
            entity.HasKey(e => e.IdinfoTabellaCodiciIva).HasName("PRIMARY");

            entity.ToTable("tabellacodiciiva");

            entity.Property(e => e.IdinfoTabellaCodiciIva)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_TabellaCodiciIva");
            entity.Property(e => e.CodNatura).HasMaxLength(5);
            entity.Property(e => e.CodiceIva).HasMaxLength(1);
            entity.Property(e => e.Descrizione).HasMaxLength(30);
            entity.Property(e => e.Esigibilita).HasMaxLength(1);
        });

        modelBuilder.Entity<Tabellaregistri>(entity =>
        {
            entity.HasKey(e => e.IdinfoTabellaRegistri).HasName("PRIMARY");

            entity.ToTable("tabellaregistri");

            entity.Property(e => e.IdinfoTabellaRegistri)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_TabellaRegistri");
            entity.Property(e => e.CodiceRegistro).HasMaxLength(5);
            entity.Property(e => e.Descrizione).HasMaxLength(50);
        });

        modelBuilder.Entity<TabellaregistriLetteredocumento>(entity =>
        {
            entity.HasKey(e => new { e.IdinfoTabellaRegistriLettereDocumento, e.IdinfoTabellaRegistri })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("tabellaregistri_letteredocumento");

            entity.Property(e => e.IdinfoTabellaRegistriLettereDocumento).HasColumnName("IDInfo_TabellaRegistri_LettereDocumento");
            entity.Property(e => e.IdinfoTabellaRegistri).HasColumnName("IDInfo_TabellaRegistri");
            entity.Property(e => e.Descrizione).HasMaxLength(255);
            entity.Property(e => e.Lettera).HasMaxLength(1);
        });

        modelBuilder.Entity<VarianteAbbigliamentoDonna>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("variante_abbigliamento_donna");

            entity.Property(e => e.IdinfoArticoli).HasColumnName("IDInfo_Articoli");
            entity.Property(e => e.IdinfoDocumenti)
                .HasMaxLength(15)
                .HasColumnName("IDInfo_Documenti");
            entity.Property(e => e.IdinfoMagazzini).HasColumnName("IDInfo_Magazzini");
            entity.Property(e => e.IdinfoTabellaRegistri).HasColumnName("IDInfo_TabellaRegistri");
            entity.Property(e => e.IdinfoVariantiComponenti).HasColumnName("IDInfo_Varianti_Componenti");
            entity.Property(e => e.IdinfoVariantiComponenti55).HasColumnName("IDInfo_Varianti_Componenti__55");
            entity.Property(e => e.IdinfoVariantiComponenti56).HasColumnName("IDInfo_Varianti_Componenti__56");
            entity.Property(e => e.IdinfoVariantiComponenti57).HasColumnName("IDInfo_Varianti_Componenti__57");
            entity.Property(e => e.IdinfoVariantiComponenti58).HasColumnName("IDInfo_Varianti_Componenti__58");
            entity.Property(e => e.IdinfoVariantiComponenti59).HasColumnName("IDInfo_Varianti_Componenti__59");
            entity.Property(e => e.IdinfoVariantiComponenti62).HasColumnName("IDInfo_Varianti_Componenti__62");
            entity.Property(e => e.Protocollo).HasMaxLength(10);
        });

        modelBuilder.Entity<VarianteAbbigliamentoDonnaDifferiti>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("variante_abbigliamento_donna_differiti");

            entity.Property(e => e.IdinfoArticoli).HasColumnName("IDInfo_Articoli");
            entity.Property(e => e.IdinfoDocumenti)
                .HasMaxLength(15)
                .HasColumnName("IDInfo_Documenti");
            entity.Property(e => e.IdinfoMagazzini).HasColumnName("IDInfo_Magazzini");
            entity.Property(e => e.IdinfoTabellaRegistri).HasColumnName("IDInfo_TabellaRegistri");
            entity.Property(e => e.IdinfoVariantiComponenti).HasColumnName("IDInfo_Varianti_Componenti");
            entity.Property(e => e.IdinfoVariantiComponenti55).HasColumnName("IDInfo_Varianti_Componenti__55");
            entity.Property(e => e.IdinfoVariantiComponenti56).HasColumnName("IDInfo_Varianti_Componenti__56");
            entity.Property(e => e.IdinfoVariantiComponenti57).HasColumnName("IDInfo_Varianti_Componenti__57");
            entity.Property(e => e.IdinfoVariantiComponenti58).HasColumnName("IDInfo_Varianti_Componenti__58");
            entity.Property(e => e.IdinfoVariantiComponenti59).HasColumnName("IDInfo_Varianti_Componenti__59");
            entity.Property(e => e.IdinfoVariantiComponenti62).HasColumnName("IDInfo_Varianti_Componenti__62");
            entity.Property(e => e.Protocollo).HasMaxLength(10);
        });

        modelBuilder.Entity<VarianteAbbigliamentoDonnaMovimentiMagazzino>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("variante_abbigliamento_donna_movimenti_magazzino");

            entity.Property(e => e.Carico).HasColumnName("CARICO");
            entity.Property(e => e.IdinfoArticoli).HasColumnName("IDInfo_Articoli");
            entity.Property(e => e.IdinfoMagazzini).HasColumnName("IDInfo_Magazzini");
            entity.Property(e => e.IdinfoMovimentiMagazzino).HasColumnName("IDInfo_Movimenti_Magazzino");
            entity.Property(e => e.IdinfoVariantiComponenti).HasColumnName("IDInfo_Varianti_Componenti");
            entity.Property(e => e.IdinfoVariantiComponenti55).HasColumnName("IDInfo_Varianti_Componenti__55");
            entity.Property(e => e.IdinfoVariantiComponenti56).HasColumnName("IDInfo_Varianti_Componenti__56");
            entity.Property(e => e.IdinfoVariantiComponenti57).HasColumnName("IDInfo_Varianti_Componenti__57");
            entity.Property(e => e.IdinfoVariantiComponenti58).HasColumnName("IDInfo_Varianti_Componenti__58");
            entity.Property(e => e.IdinfoVariantiComponenti59).HasColumnName("IDInfo_Varianti_Componenti__59");
            entity.Property(e => e.IdinfoVariantiComponenti62).HasColumnName("IDInfo_Varianti_Componenti__62");
        });

        modelBuilder.Entity<VarianteAbbigliamentoDonnaQuantitum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("variante_abbigliamento_donna_quantita");

            entity.Property(e => e.IdinfoArticoli).HasColumnName("IDInfo_Articoli");
            entity.Property(e => e.IdinfoMagazzini).HasColumnName("IDInfo_Magazzini");
            entity.Property(e => e.IdinfoVariantiComponenti).HasColumnName("IDInfo_Varianti_Componenti");
            entity.Property(e => e.IdinfoVariantiComponenti55).HasColumnName("IDInfo_Varianti_Componenti__55");
            entity.Property(e => e.IdinfoVariantiComponenti56).HasColumnName("IDInfo_Varianti_Componenti__56");
            entity.Property(e => e.IdinfoVariantiComponenti57).HasColumnName("IDInfo_Varianti_Componenti__57");
            entity.Property(e => e.IdinfoVariantiComponenti58).HasColumnName("IDInfo_Varianti_Componenti__58");
            entity.Property(e => e.IdinfoVariantiComponenti59).HasColumnName("IDInfo_Varianti_Componenti__59");
            entity.Property(e => e.IdinfoVariantiComponenti62).HasColumnName("IDInfo_Varianti_Componenti__62");
            entity.Property(e => e.Modificato).HasColumnType("timestamp");
        });

        modelBuilder.Entity<VarianteAbbigliamentoUomo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("variante_abbigliamento_uomo");

            entity.Property(e => e.IdinfoArticoli).HasColumnName("IDInfo_Articoli");
            entity.Property(e => e.IdinfoDocumenti)
                .HasMaxLength(15)
                .HasColumnName("IDInfo_Documenti");
            entity.Property(e => e.IdinfoMagazzini).HasColumnName("IDInfo_Magazzini");
            entity.Property(e => e.IdinfoTabellaRegistri).HasColumnName("IDInfo_TabellaRegistri");
            entity.Property(e => e.IdinfoVariantiComponenti).HasColumnName("IDInfo_Varianti_Componenti");
            entity.Property(e => e.IdinfoVariantiComponenti144).HasColumnName("IDInfo_Varianti_Componenti__144");
            entity.Property(e => e.IdinfoVariantiComponenti145).HasColumnName("IDInfo_Varianti_Componenti__145");
            entity.Property(e => e.IdinfoVariantiComponenti55).HasColumnName("IDInfo_Varianti_Componenti__55");
            entity.Property(e => e.IdinfoVariantiComponenti56).HasColumnName("IDInfo_Varianti_Componenti__56");
            entity.Property(e => e.IdinfoVariantiComponenti57).HasColumnName("IDInfo_Varianti_Componenti__57");
            entity.Property(e => e.IdinfoVariantiComponenti58).HasColumnName("IDInfo_Varianti_Componenti__58");
            entity.Property(e => e.IdinfoVariantiComponenti59).HasColumnName("IDInfo_Varianti_Componenti__59");
            entity.Property(e => e.IdinfoVariantiComponenti60).HasColumnName("IDInfo_Varianti_Componenti__60");
            entity.Property(e => e.IdinfoVariantiComponenti61).HasColumnName("IDInfo_Varianti_Componenti__61");
            entity.Property(e => e.Protocollo).HasMaxLength(10);
        });

        modelBuilder.Entity<VarianteAbbigliamentoUomoDifferiti>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("variante_abbigliamento_uomo_differiti");

            entity.Property(e => e.IdinfoArticoli).HasColumnName("IDInfo_Articoli");
            entity.Property(e => e.IdinfoDocumenti)
                .HasMaxLength(15)
                .HasColumnName("IDInfo_Documenti");
            entity.Property(e => e.IdinfoMagazzini).HasColumnName("IDInfo_Magazzini");
            entity.Property(e => e.IdinfoTabellaRegistri).HasColumnName("IDInfo_TabellaRegistri");
            entity.Property(e => e.IdinfoVariantiComponenti).HasColumnName("IDInfo_Varianti_Componenti");
            entity.Property(e => e.IdinfoVariantiComponenti144).HasColumnName("IDInfo_Varianti_Componenti__144");
            entity.Property(e => e.IdinfoVariantiComponenti145).HasColumnName("IDInfo_Varianti_Componenti__145");
            entity.Property(e => e.IdinfoVariantiComponenti55).HasColumnName("IDInfo_Varianti_Componenti__55");
            entity.Property(e => e.IdinfoVariantiComponenti56).HasColumnName("IDInfo_Varianti_Componenti__56");
            entity.Property(e => e.IdinfoVariantiComponenti57).HasColumnName("IDInfo_Varianti_Componenti__57");
            entity.Property(e => e.IdinfoVariantiComponenti58).HasColumnName("IDInfo_Varianti_Componenti__58");
            entity.Property(e => e.IdinfoVariantiComponenti59).HasColumnName("IDInfo_Varianti_Componenti__59");
            entity.Property(e => e.IdinfoVariantiComponenti60).HasColumnName("IDInfo_Varianti_Componenti__60");
            entity.Property(e => e.IdinfoVariantiComponenti61).HasColumnName("IDInfo_Varianti_Componenti__61");
            entity.Property(e => e.Protocollo).HasMaxLength(10);
        });

        modelBuilder.Entity<VarianteAbbigliamentoUomoMovimentiMagazzino>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("variante_abbigliamento_uomo_movimenti_magazzino");

            entity.Property(e => e.Carico).HasColumnName("CARICO");
            entity.Property(e => e.IdinfoArticoli).HasColumnName("IDInfo_Articoli");
            entity.Property(e => e.IdinfoMagazzini).HasColumnName("IDInfo_Magazzini");
            entity.Property(e => e.IdinfoMovimentiMagazzino).HasColumnName("IDInfo_Movimenti_Magazzino");
            entity.Property(e => e.IdinfoVariantiComponenti).HasColumnName("IDInfo_Varianti_Componenti");
            entity.Property(e => e.IdinfoVariantiComponenti144).HasColumnName("IDInfo_Varianti_Componenti__144");
            entity.Property(e => e.IdinfoVariantiComponenti145).HasColumnName("IDInfo_Varianti_Componenti__145");
            entity.Property(e => e.IdinfoVariantiComponenti55).HasColumnName("IDInfo_Varianti_Componenti__55");
            entity.Property(e => e.IdinfoVariantiComponenti56).HasColumnName("IDInfo_Varianti_Componenti__56");
            entity.Property(e => e.IdinfoVariantiComponenti57).HasColumnName("IDInfo_Varianti_Componenti__57");
            entity.Property(e => e.IdinfoVariantiComponenti58).HasColumnName("IDInfo_Varianti_Componenti__58");
            entity.Property(e => e.IdinfoVariantiComponenti59).HasColumnName("IDInfo_Varianti_Componenti__59");
            entity.Property(e => e.IdinfoVariantiComponenti60).HasColumnName("IDInfo_Varianti_Componenti__60");
            entity.Property(e => e.IdinfoVariantiComponenti61).HasColumnName("IDInfo_Varianti_Componenti__61");
        });

        modelBuilder.Entity<VarianteAbbigliamentoUomoQuantitum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("variante_abbigliamento_uomo_quantita");

            entity.Property(e => e.IdinfoArticoli).HasColumnName("IDInfo_Articoli");
            entity.Property(e => e.IdinfoMagazzini).HasColumnName("IDInfo_Magazzini");
            entity.Property(e => e.IdinfoVariantiComponenti).HasColumnName("IDInfo_Varianti_Componenti");
            entity.Property(e => e.IdinfoVariantiComponenti144).HasColumnName("IDInfo_Varianti_Componenti__144");
            entity.Property(e => e.IdinfoVariantiComponenti145).HasColumnName("IDInfo_Varianti_Componenti__145");
            entity.Property(e => e.IdinfoVariantiComponenti55).HasColumnName("IDInfo_Varianti_Componenti__55");
            entity.Property(e => e.IdinfoVariantiComponenti56).HasColumnName("IDInfo_Varianti_Componenti__56");
            entity.Property(e => e.IdinfoVariantiComponenti57).HasColumnName("IDInfo_Varianti_Componenti__57");
            entity.Property(e => e.IdinfoVariantiComponenti58).HasColumnName("IDInfo_Varianti_Componenti__58");
            entity.Property(e => e.IdinfoVariantiComponenti59).HasColumnName("IDInfo_Varianti_Componenti__59");
            entity.Property(e => e.IdinfoVariantiComponenti60).HasColumnName("IDInfo_Varianti_Componenti__60");
            entity.Property(e => e.IdinfoVariantiComponenti61).HasColumnName("IDInfo_Varianti_Componenti__61");
            entity.Property(e => e.Modificato).HasColumnType("timestamp");
        });

        modelBuilder.Entity<Varianti>(entity =>
        {
            entity.HasKey(e => e.IdinfoVarianti).HasName("PRIMARY");

            entity.ToTable("varianti");

            entity.Property(e => e.IdinfoVarianti)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_Varianti");
            entity.Property(e => e.Descrizione).HasMaxLength(100);
        });

        modelBuilder.Entity<VariantiComponenti>(entity =>
        {
            entity.HasKey(e => e.IdinfoVariantiComponenti).HasName("PRIMARY");

            entity.ToTable("varianti_componenti");

            entity.Property(e => e.IdinfoVariantiComponenti)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_Varianti_Componenti");
            entity.Property(e => e.Descrizione).HasMaxLength(50);
        });

        modelBuilder.Entity<VariantiJoinVariantiComponenti>(entity =>
        {
            entity.HasKey(e => e.IdInfoJoinVariantiComponenti).HasName("PRIMARY");

            entity.ToTable("varianti_join_varianti_componenti");

            entity.Property(e => e.IdInfoJoinVariantiComponenti)
                .ValueGeneratedNever()
                .HasColumnName("IdInfo_Join_Varianti_Componenti");
            entity.Property(e => e.IdinfoVarianti).HasColumnName("IDInfo_Varianti");
            entity.Property(e => e.IdinfoVariantiComponenti).HasColumnName("IDInfo_Varianti_Componenti");
        });

        modelBuilder.Entity<Vettori>(entity =>
        {
            entity.HasKey(e => e.IdinfoVettori).HasName("PRIMARY");

            entity.ToTable("vettori");

            entity.Property(e => e.IdinfoVettori)
                .ValueGeneratedNever()
                .HasColumnName("IDInfo_Vettori");
            entity.Property(e => e.Cap)
                .HasMaxLength(10)
                .HasColumnName("CAP");
            entity.Property(e => e.Citta).HasMaxLength(255);
            entity.Property(e => e.Descrizione).HasMaxLength(255);
            entity.Property(e => e.Fax).HasMaxLength(100);
            entity.Property(e => e.IdinfoComuni).HasColumnName("IDInfo_Comuni");
            entity.Property(e => e.Mail).HasMaxLength(100);
            entity.Property(e => e.Provincia).HasMaxLength(5);
            entity.Property(e => e.Telefono).HasMaxLength(100);
            entity.Property(e => e.Via).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
