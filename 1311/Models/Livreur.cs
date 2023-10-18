using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1311.Models
{
    public class Livreur
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Ville Ville { get; set; }

        [ForeignKey("Ville")]
        public int VilleId { get; set; }
        public String Adresse { get; set; }

        public List<ListeRamassage> ListeRamassage { get; set; }
        public List<Demande> Demande { get; set; }
        public List<Facture> Facture { get; set; }
        public AppUser User { get; set; }

        [ForeignKey("AppUser")]
        public string UserId { get; set; }

    }
}
