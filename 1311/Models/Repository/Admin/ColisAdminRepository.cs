using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Models.Repository.Admin
{
    public class ColisAdminRepository : AdminRepository<Colis>
    {
        private readonly AppDbContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public ColisAdminRepository(AppDbContext context,
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IEnumerable<Colis> DetailsColis(string nom)
        {

            if (!string.IsNullOrEmpty(nom))
            {
                var colis = this.context.Colis
                  .Include(a => a.BonsLivraison)
                  .Include(a => a.ListeRamassage)
                  .Include(a => a.Client)
                  .Include(a => a.User)
                  .Include(c => c.Client.Ville)
                   .Include(c => c.ListeRamassage.Livreur)
                         .Include(c => c.ListeRamassage.Livreur.User)
                  .Where(a => a.Etat == nom);
                return colis;
            }
            var colis2 = this.context.Colis
              .Include(a => a.BonsLivraison)
              .Include(a => a.ListeRamassage)
              .Include(a => a.Client)
              .Include(a => a.User)
              .Include(c => c.Client.Ville)
              .Include(c => c.ListeRamassage.Livreur)
                    .Include(c => c.ListeRamassage.Livreur.User);
            return colis2;

        }

        public IEnumerable<Colis> DetailsColisforclientAsync(string client)
        {

            if (!string.IsNullOrEmpty(client))
            {
                var colis = this.context.Colis
                  .Include(a => a.BonsLivraison)
                  .Include(a => a.ListeRamassage)
                  .Include(a => a.Client)
                  .Include(a => a.User)
                  .Include(c => c.Client.Ville)
                  .Include(c => c.ListeRamassage.Livreur)
                  .Include(c => c.ListeRamassage.Livreur.User)
                  .Where(a => a.UserId == client);
                return colis;
            }
            else
            {
                return null;
            }


        }


        public IEnumerable<Colis> GetEntitiesforuser()
        {
            var colis = context.Colis
               .Include(c => c.Client)
               .Include(c => c.User)
               .Include(c => c.Client.Ville)
               .Include(c => c.ListeRamassage)
               .Include(c => c.ListeRamassage.Livreur)
               .Include(c => c.ListeRamassage.Livreur.User)

               .ToList();
            return (colis);
        }

        public Colis getwithid(int id)
        {
            Colis col = context.Colis
                .Include(c => c.Client)
                .Include(c => c.User)
                .Include(c => c.Client.Ville)
                .Include(c => c.BonsLivraison)
                .Include(c => c.ListeRamassage)
                .Include(c=>c.Comments.OrderByDescending(c=>c.DateCreation))
                .ThenInclude(c=>c.User)
                //.Include(c=>c.User.Boutique)
                .Include(c => c.ListeRamassage.Livreur)
                .Include(c => c.Client.Ville)
                .SingleOrDefault(a => a.id == id);
            if (col is null)
            {
                return null;
            }
            return (col);
        }

     

       
            //var userx = await userManager.FindByNameAsync(client);
            //if (!string.IsNullOrEmpty(client))
            //{
            //    var colis = this.context.Colis
            //      .Include(a => a.BonsLivraison)
            //      .Include(a => a.ListeRamassage)
            //      .Include(a => a.Client)
            //      .Include(a => a.User)
            //      .Include(c => c.Client.Ville)
            //      .Include(c => c.ListeRamassage.Livreur)
            //      .Include(c => c.ListeRamassage.Livreur.User)
            //      .Where(a => a.UserId == userx.Id);
            //    return colis;
            //}
            //else
            //{
            //    return null;
            //}
        



    }
}
