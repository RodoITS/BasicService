using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicService.Modules.WooCommerce
{
    public class Image
    {
        public int id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string src { get; set; }
        public string title { get; set; }
        public string alt { get; set; }
        public int position { get; set; }
    }
}
