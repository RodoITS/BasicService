using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class ArticoliTipologie
{
    public int IdinfoArticoliTipologie { get; set; }

    public string? Descrizione { get; set; }

    public int? Idpadre { get; set; }

    public string? Immagine { get; set; }

    public int? TempId { get; set; }

    public virtual ICollection<Articoli> Articolis { get; set; } = new List<Articoli>();
}
