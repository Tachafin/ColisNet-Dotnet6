using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _1311.Models
{
    public class Ville
    {
        public int Id { get; set; }
        [Display(Name = "Nom du ville")]
        public string Name { get; set; }
        public bool Statut { get; set; }

        public List<Client> Client { get; set; }
        public List<Livreur> Livreur { get; set; }
        public List<ListeRamassage> ListeRamassage { get; set; }
        public List<Boutique> Boutique { get; set; }

    }
}
