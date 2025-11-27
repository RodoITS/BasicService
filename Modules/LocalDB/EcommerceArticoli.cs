using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class EcommerceArticoli
{
    public int IdStore { get; set; }

    public int IdInfoArticoli { get; set; }

    public int? Modificato { get; set; }

    public int? Pubblica { get; set; }

    public int? ImmaginiUploaded { get; set; }

    public DateTime? DataOraModifica { get; set; }
}
