using System.Collections.Generic;

namespace _1311.Models.ViewModels.VilleViewModels
{
    public class StatistiqueVilleViewModel
    {
        public IEnumerable<Livreur> AllLivreur { get; set; } 
        public IEnumerable<Ville> AllVille { get; set; } 
        public IEnumerable<ListVilleViewModel> TableVille { get; set; } 

    }
}
