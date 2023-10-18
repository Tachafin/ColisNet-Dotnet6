using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Models.Repository
{
    public class ReclamationRepository : GlobalRepository<Reclamation>
    {
        private readonly AppDbContext context;

        public ReclamationRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Add(string userid)
        {
            Reclamation rec = new Reclamation();
            int count = this.context.Colis.Count();
            int id;
            if (count == 0) { id = 1; }
            else { id = this.context.Reclamation.Max(colisa => colisa.Id) + 1; }
           
            rec.Name = "Reclamation" + id;
            rec.DateCreation = DateTime.Now;
            rec.UserId = userid;
            this.context.Reclamation.Add(rec);
            this.context.SaveChanges();
        }

        public void Add(Reclamation entity, string userid)
        {
            Reclamation rec = new Reclamation();

            int count = this.context.Reclamation.Count();
            int id;
            if (count == 0) { id = 1; }
            else { id = this.context.Reclamation.Max(colisa => colisa.Id) + 1; }

            rec.Sujet = entity.Sujet;
            rec.Message = entity.Message;
            rec.Name = "Reclamation  " + id;
            rec.DateCreation = DateTime.Now;
            rec.UserId = userid;
            this.context.Reclamation.Add(rec);
            this.context.SaveChanges();
        }

        public void addinbons(Colis coco, BonsLivraison Bonbon)
        {
            throw new NotImplementedException();
        }

        public Reclamation Delete(int id, string userid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reclamation> GetEntitiesforuser(string userid, bool ab)
        {
            var rec = this.context.Reclamation
                .Where(a => a.UserId == userid)
                .ToList();
            return rec;
        }

        public IEnumerable<Reclamation> GetEntitiesforuser(string userid, int ab)
        {
            throw new NotImplementedException();
        }

        public Reclamation getwithid(int id, string userid)
        {
            Reclamation recx = context.Reclamation
                .Where(c => c.UserId == userid)
                .SingleOrDefault(a => a.Id == id);
            if (recx is null)
            {
                return null;
            }
            if (recx.UserId == userid)
            {
                return recx;
            }
            return null;
        }

        public void removeinbons(Colis coco, BonsLivraison Bonbon)
        {
            throw new NotImplementedException();
        }

        public Reclamation Update(Reclamation entitychanges)
        {
            throw new NotImplementedException();
        }
    }
}
