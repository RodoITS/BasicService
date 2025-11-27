using BasicService.Modules.WooCommerce;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BasicService.Service
{
    public class WooCommerceApiService
    {
        public HttpClient _httpClient { get; set; }
        public Uri _BaseUrl { get; private set; }
        public string _CustomerKey { get; private set; }
        public string _CustomerSecret { get; private set; }

        public WooCommerceApiService(string baseUrl, string custKey, string custSercret)
        {
            _BaseUrl = new Uri($"{baseUrl}/wp-json/wc/v3/");
            _CustomerKey = custKey;
            _CustomerSecret = custSercret;
        }

        #region "Products"
        public async Task<List<Product>> GetProductsAsync(string filter = "")
        {
            List<Product> products = [];

            try
            {
                string jsonres = await CallApiAsync(HttpMethod.Get, "products");
                if (!string.IsNullOrEmpty(jsonres))
                {
                    products = JsonConvert.DeserializeObject<List<Product>>(jsonres);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return products!;
        }

        public async Task<Product> CreateNewProductAsync(Product product)
        {
            Product newproduct = new Product();
            try
            {
                string jsoncat = JsonConvert.SerializeObject(product);
                string jsonresponse = await CallApiAsync(HttpMethod.Post, "products", jsoncat);
                if (!string.IsNullOrEmpty(jsonresponse))
                    newproduct = JsonConvert.DeserializeObject<Product>(jsonresponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Product();
            }

            return newproduct;
        }

        public async Task<Product> UpdateProductAsync(long id, Product product)
        {
            Product updatedproduct = new Product();
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                string jsoncat = JsonConvert.SerializeObject(product, settings);
                string jsonresponse = await CallApiAsync(HttpMethod.Patch, $"product/{id}", jsoncat);
                if (!string.IsNullOrEmpty(jsonresponse))
                    updatedproduct = JsonConvert.DeserializeObject<Product>(jsonresponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Product();
            }

            return updatedproduct;
        }

        public async Task<bool> DeleteProductAsync(long id)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                string jsonresponse = await CallApiAsync(HttpMethod.Delete, $"products/{id}?force=true");
                return !string.IsNullOrEmpty(jsonresponse);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region "Categories"
        public async Task<Category> CreateNewCategoryAsync(Category category)
        {
            Category newcategory = new Category();
            try
            {
                string jsoncat = JsonConvert.SerializeObject(category);
                string jsonresponse = await CallApiAsync(HttpMethod.Post, "products/categories", jsoncat);
                if (!string.IsNullOrEmpty(jsonresponse))
                    newcategory = JsonConvert.DeserializeObject<Category>(jsonresponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Category();
            }

            return newcategory;
        }

        public async Task<Category> UpdateCategoryAsync(long id, Category category)
        {
            Category updatedcategory = new Category();
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                string jsoncat = JsonConvert.SerializeObject(category, settings);
                string jsonresponse = await CallApiAsync(HttpMethod.Patch, $"products/categories/{id}", jsoncat);
                if (!string.IsNullOrEmpty(jsonresponse))
                    updatedcategory = JsonConvert.DeserializeObject<Category>(jsonresponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Category();
            }

            return updatedcategory;
        }

        public async Task<bool> DeleteCategoryAsync(long id)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                string jsonresponse = await CallApiAsync(HttpMethod.Delete, $"products/categories/{id}?force=true");
                return !string.IsNullOrEmpty(jsonresponse);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<Category>> GetCategoriesAsync(string filter = "")
        {
            List<Category> categories = [];

            try
            {
                string jsonres = await CallApiAsync(HttpMethod.Get, "products/categories");
                if (!string.IsNullOrEmpty(jsonres))
                {
                    categories = JsonConvert.DeserializeObject<List<Category>>(jsonres);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return categories!;
        }
        #endregion

        #region "Brands"
        public async Task<Brand> CreateNewBrandAsync(Brand category)
        {
            Brand newbrand = new Brand();
            try
            {
                string jsoncat = JsonConvert.SerializeObject(category);
                string jsonresponse = await CallApiAsync(HttpMethod.Post, "products/brands", jsoncat);
                if (!string.IsNullOrEmpty(jsonresponse))
                    newbrand = JsonConvert.DeserializeObject<Brand>(jsonresponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Brand();
            }

            return newbrand;
        }

        public async Task<List<Brand>> GetBrandsAsync(string filter = "")
        {
            List<Brand> brands = [];

            try
            {
                string jsonres = await CallApiAsync(HttpMethod.Get, "products/brands");
                if (!string.IsNullOrEmpty(jsonres))
                {
                    brands = JsonConvert.DeserializeObject<List<Brand>>(jsonres);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return brands!;
        }

        public async Task<Brand> UpdateBrandAsync(long id, Brand brand)
        {
            Brand updatedBrand = new Brand();
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                string jsonbrand = JsonConvert.SerializeObject(brand, settings);
                string jsonresponse = await CallApiAsync(HttpMethod.Patch, $"products/brands/{id}", jsonbrand);
                if (!string.IsNullOrEmpty(jsonresponse))
                    updatedBrand = JsonConvert.DeserializeObject<Brand>(jsonresponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Brand();
            }

            return updatedBrand;
        }

        public async Task<bool> DeleteBrandAsync(long id)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                string jsonresponse = await CallApiAsync(HttpMethod.Delete, $"products/brands/{id}?force=true");
                return !string.IsNullOrEmpty(jsonresponse);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        /// <summary>
        /// Chiama api al servizio base di woocommerce
        /// </summary>
        /// <param name="httpmethod">Metodo da usare (POST, GET, PUT, DELETE)</param>
        /// <param name="partialUrl">Url da unire all'url base (es. product/categories)</param>
        /// <param name="jsonBody">Body da includere nella chiamata per i metodi POST</param>
        /// <param name="filters">eventuali filtri da eseguire nella GET</param>
        /// <returns></returns>
        private async Task<string> CallApiAsync(HttpMethod httpmethod, string partialUrl, string jsonBody = "", string filters = "")
        {
            // Build Url
            string url = $"{_BaseUrl}{partialUrl}";
            try
            {
                _httpClient = new HttpClient();
                var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_CustomerKey}:{_CustomerSecret}"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
                _httpClient.Timeout = TimeSpan.FromSeconds(30);

                HttpRequestMessage message = new HttpRequestMessage(httpmethod, url);
                if (!string.IsNullOrEmpty(jsonBody))
                {
                    message.Content = new StringContent(jsonBody, Encoding.UTF8, new MediaTypeHeaderValue("application/json"));
                    message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }

                HttpResponseMessage response = await _httpClient.SendAsync(message);
                string responsecontent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    ErrorResponse error = JsonConvert.DeserializeObject<ErrorResponse>(responsecontent);
                    Console.WriteLine(error.message);
                }
                response.EnsureSuccessStatusCode();
                return responsecontent;
            }
            catch (Exception ex)
            {
                Console.WriteLine("CallApiAsync error: " + ex.ToString());
                return string.Empty;
            }
        }

    }
}
















