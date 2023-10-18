using System.Collections.Generic;

namespace _1311.Models
{
    public class Etat
    {
        public int id { get; set; }
        public string  Nom { get; set; }

        public List<Colis> Colis { get; set; }
            public List<Facture> Facture { get; set; }
        public List<ListeRamassage> ListeRamassage { get; set; }
    }
}
