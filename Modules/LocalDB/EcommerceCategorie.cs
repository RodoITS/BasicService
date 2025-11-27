using System;
using System.Collections.Generic;

namespace BasicService.Modules.LocalDB;

public partial class EcommerceCategorie
{
    public int IdStore { get; set; }

    public int IdInfoArticoliCategorie { get; set; }

    public int? Modificato { get; set; }

    public int? Pubblica { get; set; }

    public int? ImmaginiUploaded { get; set; }

    public long? Idremoto { get; set; }

    public DateTime Createdon { get; set; }

    public DateTime? Updatedon { get; set; }
}
