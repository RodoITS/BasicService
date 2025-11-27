using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BasicService.Modules.LocalDB;
using BasicService.Modules.WooCommerce;
using BasicService.Service;

namespace BasicService.Scheduler
{
    public class ManageProducts
    {
        private WooCommerceApiService _Service;
        private DblocaleContext _contextEF;
        private int _DefaultPriceList;

        public ManageProducts(WooCommerceApiService Service, DblocaleContext contextEF, int DefaultPriceList)
        {
            _Service = Service;
            _contextEF = contextEF;
            _DefaultPriceList = DefaultPriceList;
        }

        public async Task<bool> DoOperation()
        {
            try
            {
                var products_remoto = await _Service.GetProductsAsync();

                List<Articoli> prods_locale = _contextEF.Articolis.ToList();
                List<EcommerceArticoli> ecommprod_locale = _contextEF.EcommerceArticolis.ToList();

                // carico in memoria i listini per poter usare riflessione (evito traduzioni Linq->SQL che fallirebbero)
                var listiniAll = _contextEF.ListiniArticolis?.ToList();

                foreach (Articoli product in prods_locale)
                {
                    var comprod = ecommprod_locale.FirstOrDefault(x => x.IdInfoArticoli == product.IdinfoArticoli);
                    if (comprod != null && comprod.Modificato == 1)
                    {
                        if (comprod.Pubblica == 1)
                        {
                            // recupero prezzo prodotto usando reflection sui possibili nomi delle proprietà
                            object listino = null;
                            if (listiniAll != null)
                            {
                                listino = listiniAll.FirstOrDefault(x =>
                                {
                                    var idListinoVal = GetIntProp(x, "IdinfoListiniPrezzi", "IdinfoListiniPrezzi", "IdListiniPrezzi", "IdListino", "IdListinoPrezzi");
                                    var idArtVal = GetIntProp(x, "IdinfoArticoli", "IdInfoArticoli", "IdArticolo", "IdArticoli");
                                    return idListinoVal == _DefaultPriceList && idArtVal == product.IdinfoArticoli;
                                });
                            }

                            decimal productprice = GetDecimalProp(listino, "prezzo", "Prezzo", "prezzo_unitario", "price", "Price");
                            decimal productsaleprice = GetDecimalProp(listino, "prezzoScontato", "PrezzoScontato", "prezzo_scontato", "salePrice", "SalePrice");
                            if (productsaleprice == 0) productsaleprice = productprice;
                            int vatcode = product.IdinfoTabellaCodiciIva != null ? (int)product.IdinfoTabellaCodiciIva : 0;

                            var ecommcat = _contextEF.EcommerceCategories.FirstOrDefault(x => x.IdInfoArticoliCategorie == product.IdinfoArticoliCategorie);
                            var currcat = _contextEF.ArticoliCategories.FirstOrDefault(x => x.IdinfoArticoliCategorie == product.IdinfoArticoliCategorie);

                            Product newproduct = new()
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
                                categories = new List<Category>() { new Category() { id = ecommcat != null ? ecommcat.IdInfoArticoliCategorie : 0 } }
                            };

                            IList<string> imageUrls = null; // se vuoi aggiungere URL prendi dalle tue tabelle

                            if (products_remoto.FirstOrDefault(x => x.id == comprod.Idremoto) == null)
                            {
                                var created = await _Service.CreateNewProductAsync(newproduct, imageUrls);
                                if (created != null && created.id != null)
                                    comprod.Idremoto = (int)created.id;
                            }
                            else
                            {
                                await _Service.UpdateProductAsync((long)comprod.Idremoto, newproduct, imageUrls);
                            }

                            comprod.Modificato = 0;
                            _contextEF.Update(comprod);
                            _contextEF.SaveChanges();
                        }
                        else
                        {
                            if (comprod.Idremoto != null && comprod.Idremoto != 0)
                                await _Service.DeleteProductAsync((long)comprod.Idremoto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ManageProducts.DoOperation error: {ex.Message}");
                return false;
            }
            return true;
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
    }
}