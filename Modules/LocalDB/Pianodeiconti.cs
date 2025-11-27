using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class Pianodeiconti
{
    public int IdinfoPianoDeiConti { get; set; }

    public string? Descrizione { get; set; }

    public int? Idpadre { get; set; }

    public bool? Patrimoniale { get; set; }

    public int? Livello { get; set; }

    public int? IdinfoPianoDeiContiNome { get; set; }

    public string? Codice { get; set; }
}
