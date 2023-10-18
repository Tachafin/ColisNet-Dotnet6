using _1311.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _1311.Models.Repository
{
    public class LivreurRepository : ILivreurRepository<Livreur>
    {
        private readonly AppDbContext context;

        public LivreurRepository(AppDbContext context)
        {
            this.context = context;
        }

      

        public void Add(RegisterLivreurViewModel entity, string userx)
        {
            string username = entity.Nom + entity.Prenom;
            Livreur livreur = new Livreur()
            {
                VilleId = entity.VilleId,
                UserId = userx,
                Adresse = entity.Adresse,
                Name = username
            };
            this.context.Livreur.Add(livreur);
            this.context.SaveChanges();
        }

        public IEnumerable<Livreur> All()
        {
            var AllLivreurs = this.context.Livreur
                .Include(c => c.User)
                .Include(c => c.Ville)
                .ToList();
            return (AllLivreurs);
        }

        public Livreur Get(int id)
        {
            var check = this.context.Livreur
                .Include(c=>c.User)
                .Include(c=>c.Ville)
                .SingleOrDefault(c => c.Id==id);
            return (check);
        }

        public Livreur Update(Livreur entitychanges)
        {
            var Liv = this.context.Livreur.Attach(entitychanges);
            Liv.State = EntityState.Modified;
            this.context.SaveChanges();
            return entitychanges;
        }
    }
}
