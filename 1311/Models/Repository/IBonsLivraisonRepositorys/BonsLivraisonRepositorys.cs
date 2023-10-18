using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1311.Models.Repository.IBonsLivraisonRepositorys
{
    public class BonsLivraisonRepositorys:IBonsLivraisponRepository<BonsLivraison>
    {
        private readonly AppDbContext context;

        public BonsLivraisonRepositorys(AppDbContext context)
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
            Bons.Name = "Bons Livraison" + id;
            Bons.Date_creation = DateTime.Now;
            Bons.UserId = userid;
            this.context.BonsLivraison.Add(Bons);
            this.context.SaveChanges();
        }

        public int Add2(string userid)
        {
            BonsLivraison Bons = new BonsLivraison();
            int count = this.context.BonsLivraison.Count();
            int id;
            if (count == 0) { id = 1; }
            else { id = this.context.BonsLivraison.Max(colisa => colisa.id) + 1; }
            //int idx=this.context.BonsLivraison.Max(x=>x.id)+1;
            Bons.Name = "Bons Livraison" + id;
            Bons.Date_creation = DateTime.Now;
            Bons.UserId = userid;
            this.context.BonsLivraison.Add(Bons);
            this.context.SaveChanges();
            return id;
        }

        public void AddInBons(Colis coco, BonsLivraison Bonbon)
        {
            if (coco != null && Bonbon != null)
            {
                coco.BonsLivraisonId = Bonbon.id;
                this.context.SaveChanges();
            }
        }

        public IEnumerable<BonsLivraison> All(string userid)
        {
            var bons = this.context.BonsLivraison
                 .Include(c => c.Colis)
                 .Include(c => c.User)
                 .Where(a => a.UserId == userid)
                 .ToList();
            bons.Reverse();
            return bons;
        }

       

        public BonsLivraison Get(int id, string userid)
        {
            BonsLivraison bons = context.BonsLivraison
                 .Include(c => c.Colis)
                 .Include(c=>c.User)
                 .Include(c=>c.User.Boutique)
                 .Where(c => c.UserId == userid)
                 .SingleOrDefault(a => a.id == id);
            if (bons is null)
            {
                return null;
            }
            
            return bons;
        }

        public void RemoveInbons(Colis coco, BonsLivraison Bonbon)
        {
            if (coco != null && Bonbon != null)
            {
                coco.BonsLivraisonId = null;
                this.context.SaveChanges();
            }
        }
    }
}
