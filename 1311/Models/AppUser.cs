using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1311.Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BonsLivraison> BonsLivraisons { get; set; }

        public List<Colis> Colis { get; set; }
        public List<Facture> Facture { get; set; }
        public List<Demande> Demandes { get; set; }
        public List<ListeRamassage> ListeRamassages { get; set; }
        public List<Reclamation> reclamations { get; set; }
        public List<Comment> Comments { get; set; }
        public Boutique Boutique { get; set; }
        [ForeignKey("Boutique")]
        public int? BoutiqueId { get; set; }
        public Livreur Livreur { get; set; }

        public string Adresse { get; set; }

    }
}
