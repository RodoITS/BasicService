using BasicService.Configuration;
using BasicService.Modules;
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

            // Lettura dati passati dallo schedulatore
            var dataMap = context.MergedJobDataMap;
            string connectionString = dataMap.GetString("connectionString");
            string wcsettings_str = dataMap.GetString("wcsettings");
            WooCommerceSettings wcsettings = JsonConvert.DeserializeObject<WooCommerceSettings>(wcsettings_str);

            // Creo un oggetto di tipo DbLocaleContext
            using DblocaleContext contextEF = new DblocaleContext(connectionString);

            // -----------------------------
            // SINCRONIZZAZIONE DATI
            // Create WooCommerce service
            WooCommerceApiService Service = new WooCommerceApiService(wcsettings.Url, wcsettings.Key, wcsettings.Secret);

            // Get products
            var products = await Service.GetProductsAsync();
            // Get categories
            var categories = await Service.GetCategoriesAsync();

            // -----------------------------
            // Creazione nuovo prodotto
            Product newProduct = new Product
            {
                name = "Prodotto Test",
                type = "simple",
                regular_price = "19.99",
                description = "Prodotto creato da API"
            };

            var createdProduct = await Service.CreateProductAsync(newProduct);
            if (createdProduct != null)
            {
                Console.WriteLine($"Creato prodotto con ID: {createdProduct.id}");

                // Aggiornamento prodotto
                Product updatedProduct = new Product
                {
                    name = "Prodotto Test Aggiornato",
                    regular_price = "24.99"
                };

                var updatedProductResult = await Service.UpdateProductAsync(createdProduct.id, updatedProduct);
                Console.WriteLine($"Aggiornato prodotto con ID: {updatedProductResult?.id}");
            }
            else
            {
                Console.WriteLine("Errore: creazione prodotto fallita");
            }

            // -----------------------------
            // Creazione nuova categoria
            Category newCategory = new Category
            {
                name = "Categoria Test",
                description = "Creata tramite API"
            };

            var createdCategory = await Service.CreateCategoryAsync(newCategory);
            if (createdCategory != null)
            {
                Console.WriteLine($"Categoria creata con ID: {createdCategory.id}");

                // Aggiornamento categoria
                Category updatedCategory = new Category
                {
                    name = "Categoria Test Aggiornata",
                    description = "Descrizione aggiornata"
                };

                var updatedCategoryResult = await Service.UpdateCategoryAsync(createdCategory.id, updatedCategory);
                Console.WriteLine($"Categoria aggiornata con ID: {updatedCategoryResult?.id}");
            }
            else
            {
                Console.WriteLine("Errore: creazione categoria fallita");
            }

            // -----------------------------
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
