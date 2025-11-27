using BasicService.Modules.LocalDB;
using BasicService.Modules.WooCommerce;
using BasicService.Service;

namespace BasicService.Scheduler
{
    public class ManageProducts
    {
        private WooCommerceApiService _Service;
        private DblocaleContext _contextEF;

        public ManageProducts(WooCommerceApiService Service, DblocaleContext contextEF)
        {
            //costruttore
            _Service = Service;
            _contextEF = contextEF;
        }

        public async Task<bool> DoOperation()
        {
            try
            {
                //Get products
                var products_remoto = await _Service.GetProductsAsync();

                //Sincronizzazione articoli
                List<Articoli> prods_locale = _contextEF.Articolis.ToList();
                List<EcommerceArticoli> ecommprod_locale = _contextEF.EcommerceArticolis.ToList();

                //scorro tutti gli articoli
                foreach (Articoli product in prods_locale)
                {
                    //cerco in ecoommerce_articoli il prodotto corrente
                    var comprod = ecommprod_locale.FirstOrDefault(x => x.IdInfoArticoli == product.IdinfoArticoli);
                    //Esiste ed è stata modifica?
                    if (comprod != null && comprod.Modificato == 1)
                    {
                        //è da pubblicare?
                        if (comprod.Pubblica == 1)
                        {
                            //aggiornamento o creazione prodotto
                            //recupero prezzo prodotto
                            decimal productprice = 0;
                            decimal productsaleprice = 0;
                            int vatcode = 0;

                            //recupero i dati da ecommercecategorie per prendere l'id remoto relativo all'id categoria del prodotto
                            var ecommcat = _contextEF.EcommerceCategories.FirstOrDefault(x => x.IdInfoArticoliCategorie == product.IdinfoArticoliCategorie);

                            //struttura prodotto
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
                            if (products_remoto.FirstOrDefault(x => x.id == comprod.Idremoto) == null)
                            {
                                //da creare                           
                                newproduct = await _Service.CreateNewProductAsync(newproduct);
                                //salvo l'id remoto nella tabella locale
                                comprod.Idremoto = (int)newproduct.id;
                            }
                            else
                            {
                                //da modificare
                                await _Service.UpdateProductAsync((long)comprod.Idremoto, newproduct);
                            }
                            //l'entità corrente è stata aggiornata o creata e non è più da modificare
                            comprod.Modificato = 0;
                            //aggiorno e salvo l'entità corrente
                            _contextEF.Update(comprod);
                            _contextEF.SaveChanges();
                        }
                        else
                        {
                            //da eliminare se prensente su wooc
                            await _Service.DeleteProductAsync((long)comprod.Idremoto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
            return true;
        }
    }
}
