using System;
using System.Threading.Tasks;
using BasicService.Modules.LocalDB;
using BasicService.Service;
using Quartz;

namespace BasicService.Scheduler
{
    // Scheduler che orchestri la sincronizzazione dei dati tra DB locale e WooCommerce
    // Ora implementa Quartz.IJob così può essere registrato con AddJob<T>
    public class SyncDataScheduler : IJob
    {
        private readonly WooCommerceApiService _Service;
        private readonly DblocaleContext _contextEF;
        private readonly dynamic _wcsettings; // mantenuto come nel progetto originale

        // Questo costruttore viene usato quando istanzi manualmente. Per IJob, Quartz costruisce l'istanza tramite DI.
        public SyncDataScheduler()
        {
        }

        // Costruttore utile quando viene creato manualmente (es. nei test)
        public SyncDataScheduler(WooCommerceApiService Service, DblocaleContext contextEF, dynamic wcsettings)
        {
            _Service = Service ?? throw new ArgumentNullException(nameof(Service));
            _contextEF = contextEF ?? throw new ArgumentNullException(nameof(contextEF));
            _wcsettings = wcsettings;
        }

        // Metodo IJob richiesto da Quartz: invoca l'iterazione di sincronizzazione
        public async Task Execute(IJobExecutionContext context)
        {
            // Proviamo a recuperare i servizi dalla JobDataMap se forniti
            try
            {
                // se Quartz/DI ha settato i servizi come job data, si possono estrarre qui; 
                // altrimenti si assume che la classe sia stata costruita tramite DI e _Service/_contextEF siano valorizzati.
                await RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SyncDataScheduler Execute error: {ex.Message}");
            }
        }

        // Metodo pubblico per eseguire una singola iterazione di sincronizzazione
        public async Task RunAsync()
        {
            try
            {
                Console.WriteLine($"Avvio sincronizzazione: {DateTime.Now:HH:mm:ss}");

                // Seed DB locale se vuoto (crea articoli di esempio e le righe ecommerce collegate)
                try
                {
                    await SampleProductsSeeder.SeedLocalDbIfEmpty(_contextEF);
                }
                catch (Exception exSeed)
                {
                    Console.WriteLine($"SampleProductsSeeder error: {exSeed.Message}");
                }

                // AGGIORNAMENTO CATEGORIES
                try
                {
                    var CategoryManager = new ManageCategories(_Service, _contextEF);
                    var rescat = await CategoryManager.DoOperation();
                    Console.WriteLine($"ManageCategories result: {rescat}");
                }
                catch (Exception exCat)
                {
                    Console.WriteLine($"ManageCategories error: {exCat.Message}");
                }

                // AGGIORNAMENTO BRANDS
                try
                {
                    var BrandsManager = new ManageBrands(_Service, _contextEF);
                    var resbrand = await BrandsManager.DoOperation();
                    Console.WriteLine($"ManageBrands result: {resbrand}");
                }
                catch (Exception exBrand)
                {
                    Console.WriteLine($"ManageBrands error: {exBrand.Message}");
                }

                // AGGIORNAMENTO PRODUCTS (legge dal DB locale e pubblica su remoto)
                try
                {
                    int defaultPriceList = 0;
                    try
                    {
                        if (_wcsettings != null)
                        {
                            defaultPriceList = _wcsettings.DefaultPriceList != null ? (int)_wcsettings.DefaultPriceList : 0;
                        }
                    }
                    catch { /* ignoriamo se wcsettings non ha la proprietà */ }

                    var ProductsManager = new ManageProducts(_Service, _contextEF, defaultPriceList);
                    var resprod = await ProductsManager.DoOperation();
                    Console.WriteLine($"ManageProducts result: {resprod}");
                }
                catch (Exception exProd)
                {
                    Console.WriteLine($"ManageProducts error: {exProd.Message}");
                }

                Console.WriteLine($"Fine sincronizzazione: {DateTime.Now:HH:mm:ss}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SyncDataScheduler RunAsync error: {ex.Message}");
            }
        }
    }
}