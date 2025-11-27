
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
