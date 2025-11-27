using BasicService.Modules.LocalDB;
using BasicService.Modules.WooCommerce;
using BasicService.Service;

namespace BasicService.Scheduler
{
    public class ManageCategories
    {
        private WooCommerceApiService _Service;
        private DblocaleContext _contextEF;

        public ManageCategories(WooCommerceApiService Service, DblocaleContext contextEF)
        {
            //costruttore
            _Service = Service;
            _contextEF = contextEF;
        }

        public async Task<bool> DoOperation()
        {
            try
            {
                //Get categories
                var cats_remoto = await _Service.GetCategoriesAsync();

                //Sincronizzazione categorie
                List<ArticoliCategorie> cats_locale = _contextEF.ArticoliCategories.OrderBy(x => x.Idpadre).ToList();
                List<EcommerceCategorie> ecommcat_locale = _contextEF.EcommerceCategories.ToList();

                //scorro tutte le categorie
                foreach (ArticoliCategorie categoria in cats_locale)
                {
                    //cerco in ecoommerce_articolicagegorie la categoria corrente
                    var commcat = ecommcat_locale.FirstOrDefault(x => x.IdInfoArticoliCategorie == categoria.IdinfoArticoliCategorie);
                    //Esiste ed è stata modifica?
                    if (commcat != null && commcat.Modificato == 1)
                    {
                        //è da pubblicare?
                        if (commcat.Pubblica == 1)
                        {
                            //aggiornamento o creazione categoria
                            //ricerco id padre
                            var parent = ecommcat_locale.FirstOrDefault(x => x.IdInfoArticoliCategorie == categoria.Idpadre);
                            //struttura categoria
                            Category newcat = new()
                            {
                                name = categoria.Descrizione,
                                parent = parent != null ? parent.Idremoto : 0,
                                slug = $"CAT_{categoria.IdinfoArticoliCategorie}"
                            };
                            if (cats_remoto.FirstOrDefault(x => x.id == commcat.Idremoto) == null)
                            {
                                //da creare                           
                                newcat = await _Service.CreateNewCategoryAsync(newcat);
                                //salvo l'id remoto nella tabella locale
                                commcat.Idremoto = (int)newcat.id;
                            }
                            else
                            {
                                //da modificare
                                await _Service.UpdateCategoryAsync((long)commcat.Idremoto, newcat);
                            }
                            //l'entità corrente è stata aggiornata o creata e non è più da modificare
                            commcat.Modificato = 0;
                            //aggiorno e salvo l'entità corrente
                            _contextEF.Update(commcat);
                            _contextEF.SaveChanges();
                        }
                        else
                        {
                            //da eliminare se prensente su wooc
                            await _Service.DeleteCategoryAsync((long)commcat.Idremoto);
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
