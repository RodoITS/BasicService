using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class EcommerceTipologie
{
    public int IdStore { get; set; }

    public int IdInfoArticoliTipologie { get; set; }

    public int? Modificato { get; set; }

    public int? Pubblica { get; set; }

    public int? ImmaginiUploaded { get; set; }
}
