using System.Collections.Generic;

namespace _1311.Models.ViewModels.HomeViewModels
{
    public class AdminHomeIndexViewModel
    {
        public List<AppUser> Listuser { get; set; }
        public List<Ville> Listville { get; set; }
        public IEnumerable<Livreur> ListLivreur { get; set; }
        public IEnumerable<Boutique> Boutique { get; set; }
        public IEnumerable<Colis> Colis { get; set; }
        public int allfacture { get; set; }          
        public int facturepaye { get; set; }          
        public int factureimpaye { get; set; }          
        public int totale { get; set; }                    
        public int Commission { get; set; }                    
        public int net { get; set; }                    
        public int totalenonpayout { get; set; }                    
        public int Commissionnonpayout { get; set; }                    
        public int netnonpayout { get; set; }                    
        public int all { get; set; }                    
        public int Encours { get; set; }                    
        public int Livre { get; set; }                    
        public int Retourne { get; set; }                    




    }
}
