using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Models.Repository
{
    public class BonsLivraisonRepository : GlobalRepository<BonsLivraison>
    {
        private readonly AppDbContext context;

        public BonsLivraisonRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Add(string userid)
        {
            BonsLivraison Bons = new BonsLivraison();
            int count = this.context.BonsLivraison.Count();
            int id;
            if (count == 0) { id = 1; }
            else { id = this.context.BonsLivraison.Max(colisa => colisa.id) + 1; }
            //int idx=this.context.BonsLivraison.Max(x=>x.id)+1;
            Bons.Name ="Bons Livraison"+ id;
            Bons.Date_creation = DateTime.Now;
            Bons.UserId = userid;
            this.context.BonsLivraison.Add(Bons);
            this.context.SaveChanges();
           
        }

        public void Add(BonsLivraison entity, string userid)
        {
            throw new NotImplementedException();
        }

        
        public BonsLivraison Delete(int id, string userid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BonsLivraison> GetEntitiesforuser(string userid, bool ab)
        
        {
            var bons = this.context.BonsLivraison
                .Include(c => c.Colis)
                .Include(c=>c.User)
                
                .Where(a=>a.UserId==userid)
              
                .ToList();
            return bons;
        }

        public IEnumerable<BonsLivraison> GetEntitiesforuser(string userid, string ab)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BonsLivraison> GetEntitiesforuser(string userid, int ab)
        {
            throw new NotImplementedException();
        }

        public BonsLivraison getwithid(int id, string userid)
        {
            BonsLivraison bons = context.BonsLivraison
                 .Include(c => c.Colis)
                 .Where(c => c.UserId == userid)
                 .SingleOrDefault(a => a.id == id);
            if (bons is null)
            {
                return null;
            }
            if (bons.UserId == userid )
            {
                return bons;
            }
            return null;
        }

        

        public BonsLivraison Update(BonsLivraison entitychanges)
        {
            throw new NotImplementedException();
        }

        public void addinbons(Colis coco, BonsLivraison Bonbon)
        {

            if (coco != null && Bonbon != null)
            {
                coco.BonsLivraisonId = Bonbon.id;
                this.context.SaveChanges();
            }
        }

        public void removeinbons(Colis coco, BonsLivraison Bonbon)
        {
            if (coco != null && Bonbon != null)
            {
                coco.BonsLivraisonId = null;
                this.context.SaveChanges();
            }
        }
    }
}
