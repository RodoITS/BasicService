using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class Listini
{
    public int IdinfoListini { get; set; }

    public string? Descrizione { get; set; }

    public int? Idpadre { get; set; }

    public virtual ICollection<ListiniArticoli> ListiniArticolis { get; set; } = new List<ListiniArticoli>();
}
