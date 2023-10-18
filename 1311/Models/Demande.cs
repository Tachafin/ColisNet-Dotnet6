using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1311.Models
{
    public class Demande
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public DateTime DateCreation { get; set; }
        public bool Resolu { get; set; }

        public string Adresse { get; set; }
        public string NumeroTelephone { get; set; }
        public string Note { get; set; }


        public AppUser User { get; set; }

        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public Livreur Livreur { get; set; }

        [ForeignKey("Livreur")]
        public int? LivreurId { get; set; }

    }
}
