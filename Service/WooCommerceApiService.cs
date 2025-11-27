using BasicService.Modules.WooCommerce;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BasicService.Service
{
    public class WooCommerceApiService
    {
        public HttpClient client = new HttpClient(); //oggettohttpclient
        public Uri _BaseUrl { get; private set; }
        public string _CustomerKey { get; private set; }
        public string _CustomerSecret { get; private set; }

        public WooCommerceApiService(string baseUrl, string custKey, string custSercret)
        {
            _BaseUrl = new Uri($"{baseUrl}/wp-json/wc/v3/");
            _CustomerKey = custKey;
            _CustomerSecret = custSercret;
        }

        public async Task<List<Product>> GetProductsAsync(string filter = "")
        {
            List<Product> products = new();

            //write your code
            string json = await CallApiAsync(HttpMethod.Get, "products", filter); //chiamata api
            if (!string.IsNullOrEmpty(json))
            {
                products = JsonConvert.DeserializeObject<List<Product>>(json); //deserializzazione
            }
            return products!;
        }
        public async Task<Product?> CreateProductAsync(Product newProduct)
        {
            try
            {
                using HttpClient client = new HttpClient();

                string fullUrl = $"{_BaseUrl}products";

                var authToken = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{_CustomerKey}:{_CustomerSecret}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                //Serializzazione oggetto Product in JSON
                string jsonData = JsonConvert.SerializeObject(newProduct);

                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(fullUrl, content);

                response.EnsureSuccessStatusCode();

                string responseJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Product>(responseJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CreateProductAsync error: " + ex.ToString());
                return null;
            }
        }
        public async Task<Product?> UpdateProductAsync(int productId, Product updatedProduct)
        {
            try
            {
                using HttpClient client = new HttpClient();

                string fullUrl = $"{_BaseUrl}products/{productId}";

                var authToken = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{_CustomerKey}:{_CustomerSecret}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                string jsonData = JsonConvert.SerializeObject(updatedProduct);
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync(fullUrl, content);

                response.EnsureSuccessStatusCode();

                string responseJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Product>(responseJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateProductAsync error: " + ex.ToString());
                return null;
            }
        }


        public async Task<List<Category>> GetCategoriesAsync(string filter = "")
        {
            List<Category> categories = new();

            //write your code
            string json = await CallApiAsync(HttpMethod.Get, "products/categories", filter);
            if (!string.IsNullOrEmpty(json))
            {
                categories = JsonConvert.DeserializeObject<List<Category>>(json);
            }
            return categories!;
        }
        public async Task<Category?> CreateCategoryAsync(Category newCategory)
        {
            try
            {
                using HttpClient client = new HttpClient();

                string fullUrl = $"{_BaseUrl}products/categories";

                var authToken = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{_CustomerKey}:{_CustomerSecret}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                string jsonData = JsonConvert.SerializeObject(newCategory);
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(fullUrl, content);
                response.EnsureSuccessStatusCode();

                string responseJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Category>(responseJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CreateCategoryAsync error: " + ex.ToString());
                return null;
            }
        }
        public async Task<Category?> UpdateCategoryAsync(int categoryId, Category updatedCategory)
        {
            try
            {
                using HttpClient client = new HttpClient();

                string fullUrl = $"{_BaseUrl}products/categories/{categoryId}";

                var authToken = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{_CustomerKey}:{_CustomerSecret}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                string jsonData = JsonConvert.SerializeObject(updatedCategory);
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync(fullUrl, content);
                response.EnsureSuccessStatusCode();

                string responseJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Category>(responseJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateCategoryAsync error: " + ex.ToString());
                return null;
            }
        }


        private async Task<string> CallApiAsync(HttpMethod httpmethod, string partialUrl, string filters = "")
        {
            // Build Url
            try
            {
                //write your code
                //using HttpClient client = new HttpClient(); //oggettohttpclient
                string fullUrl = $"{_BaseUrl}{partialUrl}{filters}"; //endpoint
                var authToken = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{_CustomerKey}:{_CustomerSecret}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken); //credenziali
                var request = new HttpRequestMessage(httpmethod, fullUrl);
                var response = await client.SendAsync(request); //richiesta
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                return json;
                //return string.Empty; //remove it

            }
            catch (Exception ex)
            {
                Console.WriteLine("CallApiAsync error: " + ex.ToString());
                return string.Empty;
            }
        }

        internal async Task UpdateCategoryAsync(long? id, Category updatedCategory)
        {
            throw new NotImplementedException();
        }
    }
}
