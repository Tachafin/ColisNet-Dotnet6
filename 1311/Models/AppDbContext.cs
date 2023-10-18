
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Colis> Colis { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Reclamation> Reclamation { get; set; }
        public DbSet<BonsLivraison> BonsLivraison { get; set; }
        public DbSet<Client> Client { get; set; }

        public DbSet<Demande> Demande { get; set; }

        public DbSet<Livreur> Livreur { get; set; }
        public DbSet<ListeRamassage> ListeRamassage { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<Ville> Ville { get; set; }
        public DbSet<Boutique> Boutique { get; set; }
        public DbSet<Facture> Facture { get; set; }
        public DbSet<CodeBarre> CodeBarre { get; set; }
        public DbSet<Recu_Paiement> Recu_Paiement { get; set; }
       
    }
}
