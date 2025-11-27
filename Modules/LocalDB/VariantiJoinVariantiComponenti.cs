using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class VariantiJoinVariantiComponenti
{
    public int IdInfoJoinVariantiComponenti { get; set; }

    public int? IdinfoVarianti { get; set; }

    public int? IdinfoVariantiComponenti { get; set; }

    public int? Livello { get; set; }

    public int? Ordinamento { get; set; }
}
