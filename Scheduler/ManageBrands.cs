using BasicService.Modules.LocalDB;
using BasicService.Modules.WooCommerce;
using BasicService.Service;

namespace BasicService.Scheduler
{
    public class ManageBrands
    {
        private WooCommerceApiService _Service;
        private DblocaleContext _contextEF;

        public ManageBrands(WooCommerceApiService Service, DblocaleContext contextEF)
        {
            //costruttore
            _Service = Service;
            _contextEF = contextEF;
        }

        public async Task<bool> DoOperation()
        {
            try
            {
                //Get brands
                var brands_remoto = await _Service.GetBrandsAsync();

                //Sincronizzazione tipologie
                List<ArticoliTipologie> brands_locale = _contextEF.ArticoliTipologies.OrderBy(x => x.Idpadre).ToList();
                List<EcommerceTipologie> ecommtip_locale = _contextEF.EcommerceTipologies.ToList();

                //scorro tutte le tipologie
                foreach (ArticoliTipologie tipologia in brands_locale)
                {
                    //cerco in ecoommerce_articolitipologie la tipologia corrente
                    var combrand = ecommtip_locale.FirstOrDefault(x => x.IdInfoArticoliTipologie == tipologia.IdinfoArticoliTipologie);
                    //Esiste ed è stata modifica?
                    if (combrand != null && combrand.Modificato == 1)
                    {
                        //è da pubblicare?
                        if (combrand.Pubblica == 1)
                        {
                            //aggiornamento o creazione tipologia
                            //ricerco id padre
                            var parent = ecommtip_locale.FirstOrDefault(x => x.IdInfoArticoliTipologie == tipologia.Idpadre);
                            //struttura categoria
                            Brand newbrand = new()
                            {
                                name = tipologia.Descrizione,
                                parent = parent != null ? parent.Idremoto : 0,
                                slug = $"BRA_{tipologia.IdinfoArticoliTipologie}"
                            };
                            if (brands_remoto.FirstOrDefault(x => x.id == combrand.Idremoto) == null)
                            {
                                //da creare                           
                                newbrand = await _Service.CreateNewBrandAsync(newbrand);
                                //salvo l'id remoto nella tabella locale
                                combrand.Idremoto = (int)newbrand.id;
                            }
                            else
                            {
                                //da modificare
                                await _Service.UpdateBrandAsync((long)combrand.Idremoto, newbrand);
                            }
                            //l'entità corrente è stata aggiornata o creata e non è più da modificare
                            combrand.Modificato = 0;
                            //aggiorno e salvo l'entità corrente
                            _contextEF.Update(combrand);
                            _contextEF.SaveChanges();
                        }
                        else
                        {
                            //da eliminare se prensente su wooc
                            await _Service.DeleteBrandAsync((long)combrand.Idremoto);
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
