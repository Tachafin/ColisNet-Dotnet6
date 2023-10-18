using System.Collections.Generic;

namespace _1311.Models.ViewModels.FactureViewModels
{
    public class DetailFactureViewModel
    {
        public IEnumerable<Livreur> ListLivreur { get; set; }
        public IEnumerable<Colis> ListColisFacture { get; set; }
        public IEnumerable<Colis> ListColisNonFacture { get; set; }
        public Facture fct { get; set; }
    }
}
