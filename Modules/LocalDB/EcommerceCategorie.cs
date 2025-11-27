using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicService.Modules.LocalDB;

public partial class EcommerceCategorie
{
    public int IdStore { get; set; }

    public int IdInfoArticoliCategorie { get; set; }

    public int? Modificato { get; set; }

    public int? Pubblica { get; set; }

    public int? ImmaginiUploaded { get; set; }

    public long? Idremoto { get; set; }

    [NotMapped]
    public DateTime Createdon { get; set; }

    [NotMapped]
    public DateTime? Updatedon { get; set; }
}