using BasicService.Configuration;
using BasicService.Modules;
using BasicService.Modules.LocalDB;
using BasicService.Modules.WooCommerce;
using BasicService.Service;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using System.Runtime.CompilerServices;

namespace BasicService.Scheduler
{
    [DisallowConcurrentExecution]
    public class SyncDataScheduler : IJob
    {
        private readonly ILogger _logger;

        public SyncDataScheduler(ILogger logger)
        {
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Avvio sincronizzazione: {DateTime.Now.ToLongTimeString()}");

            //Lettura dati passati dallo schedulatore
            var dataMap = context.MergedJobDataMap;
            string connectionString = dataMap.GetString("connectionString");
            string wcsettings_str = dataMap.GetString("wcsettings");
            WooCommerceSettings wcsettings = JsonConvert.DeserializeObject<WooCommerceSettings>(wcsettings_str);

            //Creo un oggetto di tipo DbLocaleContext
            using DblocaleContext contextEF = new DblocaleContext(connectionString);

            //SINCRONIZZAZIONE DATI

            //Create woocommerce service
            WooCommerceApiService Service = new WooCommerceApiService(wcsettings.Url, wcsettings.Key, wcsettings.Secret);

            //AGGIORNAMENTO CATEGORIE
            ManageCategories CategoryManager = new ManageCategories(Service, contextEF);
            var rescat = await CategoryManager.DoOperation();

            //AGGIORNAMENTO BRANDS
            ManageBrands BrandsManager = new ManageBrands(Service, contextEF);
            var resbrand = await BrandsManager.DoOperation();

            //AGGIORNAMENTO PRODUCTS
            ManageProducts ProductsManager = new ManageProducts(Service, contextEF, wcsettings.DefaultPriceList);
            var resprod = await ProductsManager.DoOperation();


            //Get products
            //var products = await Service.GetProductsAsync();
            ////Get categoreies
            //var categories = await Service.GetCategoriesAsync();
            ////Get brands
            //var brands = await Service.GetBrandsAsync();

            ////Creazione categoria
            //Category category = new Category()
            //{
            //    name = "create3 category",
            //    description = "create",
            //    parent = 0,
            //    slug = "create3category"
            //};
            //category = await Service.CreateNewCategoryAsync(category);

            ////Aggiornamento categoria
            //category.description += " - UPDATED";
            //long id = category.id ?? 0;
            //category.id = null;
            //category = await Service.UpdateCategoryAsync(id, category);

            //Eliminazione categoria
            //category = await Service.DeleteCategoryAsync(id, category);

            //Product product = new Product()
            //{
            //    id = 0,
            //    title = "test product",
            //    price = "10.0",
            //    sku = "TESTPRODUCT"
            //};
            //product = await Service.CreateNewProductAsync(product);

            //Brand brand = new Brand()
            //{
            //    name = "new brand",
            //    description = "brand",
            //    parent = 0,
            //    slug = "newbrand"
            //};
            //brand = await Service.CreateNewBrandAsync(brand);

            Console.WriteLine("TEST");
            /*
            //COMANDI LINQ (Language Integrated Query)
            //prendo tutti gli articoli
            var allproducts = contextEF.Articolis;

            //Esempio per recuperare tutti i comuni della provincia di Fermo
            var fermo = contextEF.Comunis.Where(x => x.Provincia == "FM");

            //Esempio per recuperare tutti i vettori
            var vettor1 = contextEF.Vettoris.Where(a => a.Cap != null);
            var vettor2 = contextEF.Vettoris.Where(a => string.IsNullOrEmpty(a.Cap));

            //Esempio per recuperare il primo elemento della tabella articoli
            Articoli test = contextEF.Articolis.First();

            //Esempio per recuperare tutte le tipologie che iniziano per S, scorrerle e visualizzarne la descrizione
            var res = contextEF.ArticoliTipologies.Where(x => x.Descrizione.StartsWith("S"));
            foreach (ArticoliTipologie articoliTipologie in res)
            {
                Console.WriteLine(articoliTipologie.Descrizione);
            }
              
            //Include è simile ad una join (carica tutti i dati delle tabelle incluse)
            var arts_tipocat = contextEF.Articolis.Include(x => x.Tipologies).Include(x => x.Categories);

            //Query alternativa in LINQ
            var arts1 = from Articoli in contextEF.Articolis
                       where Articoli.UnitaMisura != null
                       select Articoli;
            var arts2 = contextEF.Articolis.Where(b => b.UnitaMisura != null);

            //Prendo i primi 10 elementi di articoli
            var first_ten = contextEF.Articolis.Take(10);

            //Raggruppo gli articoli per categoria
            var artcat = contextEF.Articolis.GroupBy(x => x.IdinfoArticoliCategorie);
            foreach(var group in artcat)
            {
                Console.WriteLine("gruppo: " +  group.Key);
                foreach(var element in group)
                {
                    Console.WriteLine("articolo: " + element.DescrizionePrincipale);
                }
            }

            //Lista di stringhe creata in memoria, no db
            List<string> list = ["pippo", "pluto", "paperino"];
            var reslist = list.Where(x => x == "pippo");

            //prima categoria
            var first_categoria = contextEF.ArticoliCategories.First();

            //utilizzo di sql nativo
            var ressql = contextEF.Articolis.FromSqlRaw("SELECT * FROM ARTICOLI");
            */

            _logger.LogInformation($"Fine sincronizzazione: {DateTime.Now.ToLongTimeString()}");
        }

    }

}