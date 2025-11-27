using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BasicService.Modules.WooCommerce
{
    public class Category
    {
        public long? id { get; set; }
        public string? name { get; set; }
        public string? slug { get; set; }
        public long? parent { get; set; }
        public string? description { get; set; }
    }
}
