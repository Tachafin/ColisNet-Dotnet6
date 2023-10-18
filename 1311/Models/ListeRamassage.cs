using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1311.Models
{
    public class ListeRamassage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreation { get; set; }
        public Livreur Livreur { get; set; }

        [ForeignKey("Livreur")]
        public int? LivreurId { get; set; }
        public List<Colis> Colis { get; set; }

        public AppUser User { get; set; }

        [ForeignKey("AppUser")]
        public String UserId { get; set; }

        public Ville Ville { get; set; }

        [ForeignKey("Ville")]
        public int? VilleId { get; set; }
        public string Etat { get; set; }
  
    }
}
