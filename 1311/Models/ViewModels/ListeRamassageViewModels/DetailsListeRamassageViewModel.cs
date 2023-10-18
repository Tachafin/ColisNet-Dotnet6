using System.Collections;
using System.Collections.Generic;

namespace _1311.Models.ViewModels.ListeRamassageViewModels
{
    public class DetailsListeRamassageViewModel
    {
        public IEnumerable<Ville> ListCity { get; set; }
        public IEnumerable<Colis> ListColisNullListe { get; set; }
        public IEnumerable<Colis> ListColis { get; set; }
        public ListeRamassage listRmassage { get; set; }

    }
}
