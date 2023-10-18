using System.Collections.Generic;

namespace _1311.Models.ViewModels.DemandeViewModels
{
    public class AllDemandeViewModel
    {
        public int today { get; set; }
        public int todaygood { get; set; }
        public int todaybad { get; set; }
        public int all { get; set; }
        public List<Demande> Demandess { get; set; }
    }
}
