using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Models.Repository
{
    public class ColisRepository:GlobalRepository<Colis>
    {
        private readonly AppDbContext context;

        public ColisRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Add(Colis entity, string userid)
        {
            int count = this.context.Colis.Count();
            int id;
            if (count == 0) { id = 1; }
            else { id = this.context.Colis.Max(colisa => colisa.id) + 1; }
            string nom = "Colis " + id;

            entity.Numero_Colis = nom;
            entity.Date_creation = DateTime.Now;
            entity.Etat = "EN COURS";
            entity.Statut = "Non paye";
            Client cl = new Client();

            cl.NomComplet = entity.Client.NomComplet;
            cl.Telephone =  entity.Client.Telephone;
            cl.VilleId = entity.Client.VilleId;
            cl.adresse = entity.Client.adresse;

            entity.ClientId = cl.Id;
            entity.UserId= userid;

            //Ville Villeidcheck = context.Ville.Where(c => c.Name == entity.Client.Ville.Name).FirstOrDefault();

            //cl.VilleId= Villeidcheck.Id;

            this.context.Colis.Add(entity);
            this.context.SaveChanges();
            
        }

        public void Add(string userid)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public void addinbons(Colis coco, BonsLivraison Bonbon)
        {
            var col = this.context.Colis.SingleOrDefault(col => col.id == coco.id);

            if (col != null)
            {
                col.BonsLivraisonId = Bonbon.id;
                this.context.SaveChanges();
            }
            
        }

        public void addinbons(Colis coco, Colis Bonbon)
        {
            throw new NotImplementedException();
        }

        public Colis Delete(int id, string userid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Colis> GetEntitiesforuser(string userid, bool ab)
        {
          
            if (ab)//colis en attente
            {
                var colis = context.Colis
               .Include(c => c.Client)
                .Include(c => c.Client.Ville)
               .Where(a=>a.BonsLivraisonId == null)
               
               .Where(a => a.UserId == userid)
               .ToList();
                return (colis);
            }
            else
            {
                var colis = context.Colis
            .Include(c => c.Client)
             .Include(c => c.Client.Ville)
            //.Where(a => a.BonsLivraisonId != null)
            .Where(a=>a.Statut != "en cours")
            //.Where(a => a.Isverified == false)
            .Where(a => a.UserId == userid)
            .ToList();
                return (colis);
            }
        }

      

        public IEnumerable<Colis> GetEntitiesforuser(string userid, int ab)
        {
            if (ab == 0)
            {
                var colis = context.Colis
               .Include(c => c.Client)
               .Include(c=>c.Client.Ville)
               .Where(a => a.BonsLivraisonId == null)
               .Where(a => a.UserId == userid)
               .ToList();
                return (colis);
            }
            else
            {
                var coliss = context.Colis
                .Include(c => c.Client)
                 .Include(c => c.Client.Ville)
                .Where(a => a.BonsLivraisonId == ab)
                //.Where(a => a.Isverified == false)
                .Where(a => a.UserId == userid)
                .ToList();
                return (coliss);
            }
        }

        public Colis getwithid(int id, string userid)
        {
            Colis col = context.Colis
                .Include(c => c.Client)
                .Where(c=>c.UserId==userid)
                .SingleOrDefault(a => a.id == id);
            if (col is null)
            {
                return null;
            }
            if (col.UserId == userid && col.Isverified == false)
            {
                return col;
            }
            else
            {
                return null;
            }
        }

       

        public void removeinbons(Colis coco, BonsLivraison Bonbon)
        {
            var col = this.context.Colis.SingleOrDefault(col => col.id == coco.id);
            if (col != null)
            {
                col.BonsLivraisonId = null;
                this.context.SaveChanges();
            }
            
        }

        public void removeinbons(Colis coco, Colis Bonbon)
        {
            throw new NotImplementedException();
        }

        public Colis Update(Colis entitychanges)
        {
            var colis = this.context.Colis.Attach(entitychanges);
            colis.State = EntityState.Modified;
            this.context.SaveChanges();
            return entitychanges;
            
        }

        

    }
}
