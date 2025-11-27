using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class PianodeicontiNome
{
    public int IdinfoPianoDeiContiNome { get; set; }

    public string? Nome { get; set; }

    public bool Attivo { get; set; }
}
