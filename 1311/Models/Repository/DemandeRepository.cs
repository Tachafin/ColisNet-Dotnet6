using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1311.Models.Repository
{
    public class DemandeRepository : GlobalRepository<Demande>
    {
        private readonly AppDbContext context;

        public DemandeRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Add(string userid)
        {
            




            
        }

        public void Add(Demande entity, string userid)
        {
            throw new NotImplementedException();
        }

        public void addinbons(Colis coco, BonsLivraison Bonbon)
        {
            throw new NotImplementedException();
        }

        public Demande Delete(int id, string userid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Demande> GetEntitiesforuser(string userid, bool ab)
        {
            var Dmd = this.context.Demande
                .Where(a => a.UserId == userid)
               .ToList();

            return Dmd;
        }

        public IEnumerable<Demande> GetEntitiesforuser(string userid, int ab)
        {
            throw new NotImplementedException();
        }

        public Demande getwithid(int id, string userid)
        {
            throw new NotImplementedException();
        }

        public void removeinbons(Colis coco, BonsLivraison Bonbon)
        {
            throw new NotImplementedException();
        }

        public Demande Update(Demande entitychanges)
        {
            throw new NotImplementedException();
        }
    }
}
