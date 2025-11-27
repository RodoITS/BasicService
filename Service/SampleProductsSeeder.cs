using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BasicService.Modules.LocalDB;
using BasicService.Modules.WooCommerce;

namespace BasicService.Service
{
    // Seeder che popola il DB locale (Articoli + EcommerceArticolis) e fornisce helper per pubblicare singoli prodotti
    public static class SampleProductsSeeder
    {
        // Se il DB locale non contiene articoli/ecommerce articoli, aggiunge due articoli di esempio
        public static async Task SeedLocalDbIfEmpty(DblocaleContext contextEF)
        {
            if (contextEF == null) throw new ArgumentNullException(nameof(contextEF));

            try
            {
                var artikCount = contextEF.Articolis.Count();
                var ecommCount = contextEF.EcommerceArticolis.Count();

                if (artikCount > 0 && ecommCount > 0)
                {
                    Console.WriteLine("SampleProductsSeeder: DB locale già popolato, skip seeding.");
                    return;
                }

                Console.WriteLine("SampleProductsSeeder: popolo DB locale con prodotti di esempio...");

                var art1 = new Articoli
                {
                    DescrizionePrincipale = "Prodotto di Test A",
                    DescrizioneSecondaria1 = "Prodotto di prova A",
                    CodiceMagazzino = "TEST-A-001",
                    IdinfoArticoliCategorie = 0,
                    IdinfoTabellaCodiciIva = 0
                };

                var art2 = new Articoli
                {
                    DescrizionePrincipale = "Prodotto di Test B",
                    DescrizioneSecondaria1 = "Prodotto di prova B",
                    CodiceMagazzino = "TEST-B-001",
                    IdinfoArticoliCategorie = 0,
                    IdinfoTabellaCodiciIva = 0
                };

                contextEF.Articolis.AddRange(art1, art2);
                contextEF.SaveChanges();

                var e1 = new EcommerceArticoli
                {
                    IdStore = 1,
                    IdInfoArticoli = art1.IdinfoArticoli,
                    Modificato = 1,
                    Pubblica = 1,
                    ImmaginiUploaded = 0,
                    Idremoto = null,
                    Createdon = DateTime.Now
                };

                var e2 = new EcommerceArticoli
                {
                    IdStore = 1,
                    IdInfoArticoli = art2.IdinfoArticoli,
                    Modificato = 1,
                    Pubblica = 1,
                    ImmaginiUploaded = 0,
                    Idremoto = null,
                    Createdon = DateTime.Now
                };

                contextEF.EcommerceArticolis.AddRange(e1, e2);
                contextEF.SaveChanges();

                Console.WriteLine("SampleProductsSeeder: DB locale popolato con successo.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SampleProductsSeeder error (SeedLocalDbIfEmpty): {ex.Message}");
            }

            await Task.CompletedTask;
        }

        // Pubblica (crea o aggiorna) un singolo prodotto locale verso WooCommerce
        // Accepts optional imageUrls - if provided those URLs will be added as images (WooCommerce will import)
        public static async Task PublishLocalProductAsync(DblocaleContext contextEF, WooCommerceApiService service, int idInfoArticoli, IList<string> imageUrls = null)
        {
            if (contextEF == null) throw new ArgumentNullException(nameof(contextEF));
            if (service == null) throw new ArgumentNullException(nameof(service));

            try
            {
                var product = contextEF.Articolis.FirstOrDefault(x => x.IdinfoArticoli == idInfoArticoli);
                if (product == null)
                {
                    Console.WriteLine($"PublishLocalProductAsync: articolo {idInfoArticoli} non trovato.");
                    return;
                }

                var ecomm = contextEF.EcommerceArticolis.FirstOrDefault(x => x.IdInfoArticoli == idInfoArticoli);
                if (ecomm == null)
                {
                    ecomm = new EcommerceArticoli
                    {
                        IdStore = 1,
                        IdInfoArticoli = idInfoArticoli,
                        Modificato = 1,
                        Pubblica = 1,
                        Createdon = DateTime.Now
                    };
                    contextEF.EcommerceArticolis.Add(ecomm);
                    contextEF.SaveChanges();
                }

                // carico listini in memoria per reflection (se esistono)
                var listiniAll = contextEF.ListiniArticolis?.ToList();
                object listino = null;
                if (listiniAll != null)
                {
                    listino = listiniAll.FirstOrDefault(x =>
                    {
                        var idArtVal = GetIntProp(x, "IdinfoArticoli", "IdInfoArticoli", "IdArticolo", "IdArticoli");
                        return idArtVal == idInfoArticoli;
                    });
                }

                decimal productprice = GetDecimalProp(listino, "prezzo", "Prezzo", "price", "Price");
                decimal productsaleprice = GetDecimalProp(listino, "prezzoScontato", "PrezzoScontato", "salePrice", "SalePrice");
                if (productsaleprice == 0) productsaleprice = productprice;

                int vatcode = product.IdinfoTabellaCodiciIva != null ? (int)product.IdinfoTabellaCodiciIva : 0;

                var newproduct = new Product
                {
                    name = product.DescrizionePrincipale,
                    type = "simple",
                    status = "publish",
                    catalog_visibility = "visible",
                    description = product.DescrizioneSecondaria1,
                    sku = product.CodiceMagazzino,
                    price = productprice.ToString(),
                    regular_price = productprice.ToString(),
                    sale_price = productsaleprice.ToString(),
                    on_sale = productprice != productsaleprice,
                    purchaseable = true,
                    tax_status = "taxable",
                    tax_class = vatcode.ToString(),
                    manage_stock = true,
                    categories = new List<Category>() { new Category() { id = 0 } }
                };

                if (ecomm.Idremoto == null || ecomm.Idremoto == 0)
                {
                    var created = await service.CreateNewProductAsync(newproduct, imageUrls);
                    if (created != null)
                    {
                        ecomm.Idremoto = created.id != null ? (long?)created.id : null;
                        ecomm.Modificato = 0;
                        contextEF.Update(ecomm);
                        contextEF.SaveChanges();
                        Console.WriteLine($"PublishLocalProductAsync: prodotto locale {idInfoArticoli} pubblicato con id remoto={created.id}.");
                    }
                }
                else
                {
                    await service.UpdateProductAsync((long)ecomm.Idremoto, newproduct, imageUrls);
                    ecomm.Modificato = 0;
                    contextEF.Update(ecomm);
                    contextEF.SaveChanges();
                    Console.WriteLine($"PublishLocalProductAsync: prodotto locale {idInfoArticoli} aggiornato su remoto id={ecomm.Idremoto}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PublishLocalProductAsync error: {ex.Message}");
            }
        }

        // helper reflection: ritorna decimal da diverse possibili proprietà, altrimenti 0
        private static decimal GetDecimalProp(object obj, params string[] propNames)
        {
            if (obj == null) return 0m;
            var t = obj.GetType();
            foreach (var n in propNames)
            {
                var p = t.GetProperty(n, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                {
                    var val = p.GetValue(obj);
                    if (val == null) continue;
                    if (val is decimal d) return d;
                    if (val is double db) return Convert.ToDecimal(db);
                    if (val is float f) return Convert.ToDecimal(f);
                    if (val is int i) return Convert.ToDecimal(i);
                    if (decimal.TryParse(val.ToString(), out decimal parsed)) return parsed;
                }
            }
            return 0m;
        }

        private static int GetIntProp(object obj, params string[] propNames)
        {
            if (obj == null) return 0;
            var t = obj.GetType();
            foreach (var n in propNames)
            {
                var p = t.GetProperty(n, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p != null)
                {
                    var val = p.GetValue(obj);
                    if (val == null) continue;
                    if (val is int i) return i;
                    if (int.TryParse(val.ToString(), out int parsed)) return parsed;
                }
            }
            return 0;
        }

        // Crea prodotti direttamente sul sito (non tocca il DB locale), usando immagini via src URL
        public static async Task CreateRemoteSamplesAsync(WooCommerceApiService service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            try
            {
                var img1 = "https://via.placeholder.com/600x400.png?text=Test+A";
                var img2 = "https://via.placeholder.com/600x400.png?text=Test+B";

                var samples = new List<(Product p, IList<string> imgs)>
                {
                    (new Product
                    {
                        name = "Prodotto Remoto Test A",
                        type = "simple",
                        status = "publish",
                        catalog_visibility = "visible",
                        description = "Creato direttamente dal seeder remoto",
                        sku = "RTEST-A-001",
                        price = "9.90",
                        regular_price = "9.90",
                        on_sale = false,
                        purchaseable = true,
                        manage_stock = false,
                        tax_status = "taxable",
                        tax_class = "0",
                        categories = new List<Category>() { new Category() { id = 0 } }
                    }, new List<string> { img1 }),

                    (new Product
                    {
                        name = "Prodotto Remoto Test B",
                        type = "simple",
                        status = "publish",
                        catalog_visibility = "visible",
                        description = "Creato direttamente dal seeder remoto",
                        sku = "RTEST-B-001",
                        price = "14.90",
                        regular_price = "14.90",
                        on_sale = false,
                        purchaseable = true,
                        manage_stock = false,
                        tax_status = "taxable",
                        tax_class = "0",
                        categories = new List<Category>() { new Category() { id = 0 } }
                    }, new List<string> { img2 })
                };

                foreach (var entry in samples)
                {
                    var created = await service.CreateNewProductAsync(entry.p, entry.imgs);
                    Console.WriteLine($"CreateRemoteSamplesAsync: creato prodotto remoto id={created?.id} nome={created?.name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CreateRemoteSamplesAsync error: {ex.Message}");
            }
        }
    }
}