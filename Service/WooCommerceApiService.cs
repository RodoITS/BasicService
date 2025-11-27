using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BasicService.Modules.WooCommerce;

namespace BasicService.Service
{
    public class WooCommerceApiService
    {
        public HttpClient _httpClient { get; set; }
        public Uri _BaseUrl { get; private set; }
        public string _CustomerKey { get; private set; }
        public string _CustomerSecret { get; private set; }

        public WooCommerceApiService(string baseUrl, string custKey, string custSecret)
        {
            if (!baseUrl.EndsWith("/")) baseUrl += "/";
            _BaseUrl = new Uri($"{baseUrl}wp-json/wc/v3/");
            _CustomerKey = custKey;
            _CustomerSecret = custSecret;

            _httpClient ??= new HttpClient
            {
                BaseAddress = _BaseUrl,
                Timeout = TimeSpan.FromSeconds(60)
            };
        }

        #region Products
        public async Task<List<Product>> GetProductsAsync(string filter = "")
        {
            var products = new List<Product>();
            try
            {
                string endpoint = "products";
                if (!string.IsNullOrWhiteSpace(filter)) endpoint += "?" + filter;

                string jsonres = await CallApiAsync(HttpMethod.Get, endpoint);
                if (!string.IsNullOrEmpty(jsonres))
                    products = JsonConvert.DeserializeObject<List<Product>>(jsonres) ?? new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetProductsAsync error: {ex.Message}");
            }
            return products;
        }

        public async Task<Product> CreateNewProductAsync(Product product, IList<string> imageUrls = null)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            try
            {
                JObject payload = JObject.FromObject(product);
                if (imageUrls != null && imageUrls.Count > 0)
                    payload["images"] = JArray.FromObject(imageUrls.Select(u => new { src = u }));

                string jsonresponse = await CallApiAsync(HttpMethod.Post, "products", payload.ToString(Formatting.None));
                if (!string.IsNullOrEmpty(jsonresponse))
                    return JsonConvert.DeserializeObject<Product>(jsonresponse) ?? new Product();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CreateNewProductAsync error: {ex.Message}");
            }
            return new Product();
        }

        public async Task<Product> UpdateProductAsync(long id, Product product, IList<string> imageUrls = null)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            try
            {
                var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                JObject payload = JObject.FromObject(product, JsonSerializer.Create(settings));
                if (imageUrls != null && imageUrls.Count > 0)
                    payload["images"] = JArray.FromObject(imageUrls.Select(u => new { src = u }));

                string jsonresponse = await CallApiAsync(new HttpMethod("PATCH"), $"products/{id}", payload.ToString(Formatting.None));
                if (!string.IsNullOrEmpty(jsonresponse))
                    return JsonConvert.DeserializeObject<Product>(jsonresponse) ?? new Product();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateProductAsync error: {ex.Message}");
            }
            return new Product();
        }

        public async Task<bool> DeleteProductAsync(long id)
        {
            try
            {
                string jsonresponse = await CallApiAsync(HttpMethod.Delete, $"products/{id}?force=true");
                return !string.IsNullOrEmpty(jsonresponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeleteProductAsync error: {ex.Message}");
                return false;
            }
        }
        #endregion

        #region Categories
        public async Task<List<Category>> GetCategoriesAsync(string filter = "")
        {
            var categories = new List<Category>();
            try
            {
                string endpoint = "products/categories";
                if (!string.IsNullOrWhiteSpace(filter)) endpoint += "?" + filter;
                string jsonres = await CallApiAsync(HttpMethod.Get, endpoint);
                if (!string.IsNullOrEmpty(jsonres))
                    categories = JsonConvert.DeserializeObject<List<Category>>(jsonres) ?? new List<Category>();

                var topCats = categories.Where(c => c.parent == 0).ToList();
                foreach (var cat in topCats)
                {
                    string childEndpoint = $"products/categories?parent={cat.id}";
                    string childJson = await CallApiAsync(HttpMethod.Get, childEndpoint);
                    if (!string.IsNullOrEmpty(childJson))
                    {
                        var childs = JsonConvert.DeserializeObject<List<Category>>(childJson) ?? new List<Category>();
                        foreach (var ch in childs)
                            if (!categories.Any(x => x.id == ch.id))
                                categories.Add(ch);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetCategoriesAsync error: {ex.Message}");
            }
            return categories;
        }

        public async Task<Category> CreateNewCategoryAsync(Category category)
        {
            var newcategory = new Category();
            try
            {
                string jsoncat = JsonConvert.SerializeObject(category);
                string jsonresponse = await CallApiAsync(HttpMethod.Post, "products/categories", jsoncat);
                if (!string.IsNullOrEmpty(jsonresponse))
                    newcategory = JsonConvert.DeserializeObject<Category>(jsonresponse) ?? new Category();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CreateNewCategoryAsync error: {ex.Message}");
            }
            return newcategory;
        }

        public async Task<Category> UpdateCategoryAsync(long id, Category category)
        {
            var updated = new Category();
            try
            {
                var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                string jsoncat = JsonConvert.SerializeObject(category, settings);
                string jsonresponse = await CallApiAsync(new HttpMethod("PATCH"), $"products/categories/{id}", jsoncat);
                if (!string.IsNullOrEmpty(jsonresponse))
                    updated = JsonConvert.DeserializeObject<Category>(jsonresponse) ?? new Category();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateCategoryAsync error: {ex.Message}");
            }
            return updated;
        }

        public async Task<bool> DeleteCategoryAsync(long id)
        {
            try
            {
                string jsonresponse = await CallApiAsync(HttpMethod.Delete, $"products/categories/{id}?force=true");
                return !string.IsNullOrEmpty(jsonresponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeleteCategoryAsync error: {ex.Message}");
                return false;
            }
        }
        #endregion

        #region Brands (usiamo il modello Brand)
        public async Task<List<Brand>> GetBrandsAsync(string filter = "")
        {
            var list = new List<Brand>();
            try
            {
                string endpoint = "products/brands";
                if (!string.IsNullOrWhiteSpace(filter)) endpoint += "?" + filter;
                string jsonres = await CallApiAsync(HttpMethod.Get, endpoint);
                if (!string.IsNullOrEmpty(jsonres))
                    list = JsonConvert.DeserializeObject<List<Brand>>(jsonres) ?? new List<Brand>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetBrandsAsync error: {ex.Message}");
            }
            return list;
        }

        public async Task<Brand> CreateNewBrandAsync(Brand brand)
        {
            var created = new Brand();
            try
            {
                string json = JsonConvert.SerializeObject(brand);
                string jsonres = await CallApiAsync(HttpMethod.Post, "products/brands", json);
                if (!string.IsNullOrEmpty(jsonres))
                    created = JsonConvert.DeserializeObject<Brand>(jsonres) ?? new Brand();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CreateNewBrandAsync error: {ex.Message}");
            }
            return created;
        }

        public async Task<Brand> UpdateBrandAsync(long id, Brand brand)
        {
            var updated = new Brand();
            try
            {
                string json = JsonConvert.SerializeObject(brand);
                string jsonres = await CallApiAsync(new HttpMethod("PATCH"), $"products/brands/{id}", json);
                if (!string.IsNullOrEmpty(jsonres))
                    updated = JsonConvert.DeserializeObject<Brand>(jsonres) ?? new Brand();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdateBrandAsync error: {ex.Message}");
            }
            return updated;
        }

        public async Task<bool> DeleteBrandAsync(long id)
        {
            try
            {
                string jsonres = await CallApiAsync(HttpMethod.Delete, $"products/brands/{id}?force=true");
                return !string.IsNullOrEmpty(jsonres);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DeleteBrandAsync error: {ex.Message}");
                return false;
            }
        }
        #endregion

        public async Task<long?> UploadImageAsync(string filename, byte[] bytes, string mimeType = "image/jpeg")
        {
            if (string.IsNullOrWhiteSpace(filename) || bytes == null || bytes.Length == 0)
                return null;

            try
            {
                string baseStr = _BaseUrl.ToString();
                string mediaBase = baseStr.Replace("/wp-json/wc/v3/", "/wp-json/wp/v2/");
                string mediaEndpoint = mediaBase + "media";

                if (!string.IsNullOrWhiteSpace(_CustomerKey) && !string.IsNullOrWhiteSpace(_CustomerSecret))
                    mediaEndpoint += $"?consumer_key={_CustomerKey}&consumer_secret={_CustomerSecret}";

                using var request = new HttpRequestMessage(HttpMethod.Post, mediaEndpoint);
                var content = new ByteArrayContent(bytes);
                content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = filename
                };
                request.Content = content;

                var resp = await _httpClient.SendAsync(request);
                resp.EnsureSuccessStatusCode();
                var json = await resp.Content.ReadAsStringAsync();
                var jobj = JObject.Parse(json);
                var idToken = jobj["id"];
                if (idToken != null && long.TryParse(idToken.ToString(), out long id))
                    return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UploadImageAsync error: {ex.Message}");
            }
            return null;
        }

        private async Task<string> CallApiAsync(HttpMethod method, string endpoint, string body = null)
        {
            try
            {
                string uriString;
                if (Uri.IsWellFormedUriString(endpoint, UriKind.Absolute))
                    uriString = endpoint;
                else
                    uriString = _BaseUrl.ToString().TrimEnd('/') + "/" + endpoint.TrimStart('/');

                if (!string.IsNullOrWhiteSpace(_CustomerKey) && !string.IsNullOrWhiteSpace(_CustomerSecret))
                    uriString += uriString.Contains("?") ? $"&consumer_key={_CustomerKey}&consumer_secret={_CustomerSecret}" : $"?consumer_key={_CustomerKey}&consumer_secret={_CustomerSecret}";

                using var request = new HttpRequestMessage(method, uriString);
                if (!string.IsNullOrEmpty(body))
                    request.Content = new StringContent(body, System.Text.Encoding.UTF8, "application/json");

                var resp = await _httpClient.SendAsync(request);
                resp.EnsureSuccessStatusCode();
                return await resp.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException hre)
            {
                Console.WriteLine($"CallApiAsync HTTP error: {hre.Message}");
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CallApiAsync error: {ex.Message}");
                return string.Empty;
            }
        }
    }
}