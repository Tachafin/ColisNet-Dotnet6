using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _1311.Models.Repository.IVilleRepositorys
{
    public class VilleRepository : IVilleRepository<Ville>
    {
        private readonly AppDbContext context;

        public VilleRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Add(Ville entity)
        {
            Ville model = new Ville()
            {
                Name = entity.Name,
                Statut=true,
            };
            this.context.Ville.Add(model);
            this.context.SaveChanges(); 
        }

        public List<Ville> All()
        {
            var all = this.context.Ville.ToList();
            return all;
        }

        public List<Ville> All(string onorof)
        {
          
            if(onorof == "on")
            {
                return this.context.Ville.Where(c => c.Statut == true).ToList();
            }
           else if (onorof == "off")
            {
                return this.context.Ville.Where(c => c.Statut == true).ToList();
            }
            return this.context.Ville.ToList();
        }

      
        public Ville Get(int id)
        {
            var villex = this.context.Ville.FirstOrDefault(c => c.Id == id);
            return villex;
        }

        public Ville Update(Ville entity)
        {
            var colis = this.context.Ville.Attach(entity);
            colis.State = EntityState.Modified;
            this.context.SaveChanges();
            return entity;
        }
    }
}
