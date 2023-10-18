using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1311.Models
{
    public class Facture
    {
        public int Id { get; set; }
        public string    Nom { get; set; }
        public DateTime DateCreation { get; set; }
        public int? Total { get; set; }
        public int? Frais { get; set; }
        public int? Net { get; set; }
        public string Statut { get; set; }
        public String Type { get; set; }
        public AppUser User { get; set; }

        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public Livreur Livreur { get; set; }

        [ForeignKey("Livreur")]
        public int? LivreurId { get; set; }

        public List<Colis> Colis { get; set; }
        //public byte[] Recu_Paiement { get; set; }

        [ForeignKey("Recu_Paiement")]
        public int? Recu_PaiementId { get; set; }
        public Recu_Paiement Recu_Paiement { get; set; }

    }
}
