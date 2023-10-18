using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Models
{
    public class Colis
    {
        public int id { get; set; }
        [Display(Name ="Numero Colis")]
        public String Numero_Colis { get; set; }
        
        [Display(Name = "Date Creation")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date_creation { get; set; }
        [Display(Name = "Date Ramassage")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date_Ramassage { get; set; }
        [Display(Name = "Date Livraison")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date_Livraison { get; set; }
        [Display(Name = "Statut")]
        public String Statut { get; set; }
        [Display(Name = "Etat")]
        public String Etat { get; set; }
        public bool Isverified { get; set; }
        [Display(Name = "Prix Totale")]
        [Required]
        public float Prix { get; set; }

        public bool Ouverture_Colis { get; set; }

        public string Produit { get; set; }
        public string Note { get; set; }


        public BonsLivraison BonsLivraison { get; set; }
        [ForeignKey("BonsLivraison")]
        public int? BonsLivraisonId { get; set; }
        public Client Client { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public AppUser User { get; set; }

        [ForeignKey("AppUser")]
        public string UserId { get; set; }

        public ListeRamassage ListeRamassage { get; set; }

        [ForeignKey("ListeRamassage")]
        public int? ListeRamassageId { get; set; }
        public Facture Facture { get; set; }

        [ForeignKey("Facture")]
        public int? FactureId { get; set; }
        public List<Comment> Comments { get; set; }
        [ForeignKey("CodeBarre")]
        public int? CodeBarreId { get; set; }
        public CodeBarre CodeBarre { get; set; }
    }
}
