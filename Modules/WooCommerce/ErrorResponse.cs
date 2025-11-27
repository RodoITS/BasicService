using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicService.Modules.WooCommerce
{
    public class Data
    {
        public int status { get; set; }
        public int resource_id { get; set; }
    }

    public class ErrorResponse
    {
        public string code { get; set; }
        public string message { get; set; }
        public Data data {  get; set; }
    }
}
