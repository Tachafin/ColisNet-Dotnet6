using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1311.Models.Repository.IReclamationRepositorys
{
    public class ReclamationRepositoryy : IReclamationRepository<Reclamation>
    {
        private readonly AppDbContext context;

        public ReclamationRepositoryy(AppDbContext context)
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

        public void Add(string userid, Reclamation recx)
        {
            Reclamation rec = new Reclamation();

            int count = this.context.Reclamation.Count();
            int id;
            if (count == 0) { id = 1; }
            else { id = this.context.Reclamation.Max(a => a.Id) + 1; }

            rec.Sujet = recx.Sujet;
            rec.Message = recx.Message;
            rec.Name = "Reclamation  " + id;
            rec.DateCreation = DateTime.Now;
            rec.UserId = userid;
            this.context.Reclamation.Add(rec);
            this.context.SaveChanges();
        }



        public IEnumerable<Reclamation> AdminAll()
        {
            var All = this.context.Reclamation
                 .Include(c => c.User)
                 .ToList();
            return All;
        }

        public IEnumerable<Reclamation> All(string userid)
        {
            var all = this.context.Reclamation
                .Include(c => c.User)
                .Where(c => c.UserId == userid)
                 .ToList();
            return all;
        }



        public Reclamation Get(string userid, int id)
        {
            Reclamation recx = context.Reclamation
                .Include(c => c.User)
              .Where(c => c.UserId == userid)
              .SingleOrDefault(a => a.Id == id);
            if (recx is null)
            {
                return null;
            }
            return recx;
        }

        public IEnumerable<Reclamation> GetAllData(int? nb, string userid)
        {
            List<Reclamation> listex = new List<Reclamation>();

            if (userid != null) //Get Data with filter user
            {
                if (nb == 1) //Today RECLAMATION
                {
                    listex = this.context.Reclamation
                             .Where(c => c.DateCreation.Year == DateTime.Today.Year &&
                              c.DateCreation.Month == DateTime.Today.Month &&
                              c.DateCreation.Day == DateTime.Today.Day)
                             .Where(c=>c.UserId==userid)
                             .Include(c => c.User)
                              .ToList();
                }
                else if (nb == 2) //today reclamation success
                {
                    listex = this.context.Reclamation
                           .Where(c => c.DateCreation.Year == DateTime.Today.Year &&
                            c.DateCreation.Month == DateTime.Today.Month &&
                            c.DateCreation.Day == DateTime.Today.Day)
                           .Where(c => c.Statut == true)
                            .Where(c => c.UserId == userid)
                            .Include(c => c.User)
                            .ToList();
                }
                else if (nb == 3) //today reclamation Bad
                {
                    listex = this.context.Reclamation  
                            .Where(c => c.DateCreation.Year == DateTime.Today.Year &&
                             c.DateCreation.Month == DateTime.Today.Month &&
                             c.DateCreation.Day == DateTime.Today.Day)
                             .Where(c => c.UserId == userid)
                            .Where(c => c.Statut == false)
                            .Include(c => c.User)
                             .ToList();
                }
                else //all reclamation
                {
                    listex = this.context.Reclamation
                         .Where(c => c.UserId == userid)
                         .Include(c => c.User)
                          .ToList();
                }
            }
            else //Get Alldata without user
            {
                if (nb == 1) //Today RECLAMATION without user
                {
                    listex = this.context.Reclamation
                             .Where(c => c.DateCreation.Year == DateTime.Today.Year &&
                              c.DateCreation.Month == DateTime.Today.Month &&
                              c.DateCreation.Day == DateTime.Today.Day)
                                .Include(c => c.User)
                              .ToList();
                }
                else if (nb == 2) //today reclamation success without user
                {
                    listex = this.context.Reclamation
                           .Where(c => c.DateCreation.Year == DateTime.Today.Year &&
                            c.DateCreation.Month == DateTime.Today.Month &&
                            c.DateCreation.Day == DateTime.Today.Day)
                           .Where(c => c.Statut == true)
                              .Include(c => c.User)
                            .ToList();
                }
                else if (nb == 3) //today reclamation Bad without user
                {
                    listex = this.context.Reclamation
                            .Where(c => c.DateCreation.Year == DateTime.Today.Year &&
                             c.DateCreation.Month == DateTime.Today.Month &&
                             c.DateCreation.Day == DateTime.Today.Day)
                             .Include(c => c.User)
                            .Where(c => c.Statut == false)
                             .ToList();
                }
                else //all reclamation without user
                {
                    listex = this.context.Reclamation
                            .Include(c=>c.User)
                          .ToList();
                }
            }



        

          
            return listex;

        }

        public Reclamation GetForAdmin(int id)
        {

            Reclamation recx = context.Reclamation
                .Include(c => c.User)

              .SingleOrDefault(a => a.Id == id);
            if (recx is null)
            {
                return null;
            }
            return recx;
        }

        public Reclamation Update(Reclamation rec)
        {
            var rece = this.context.Reclamation.Attach(rec);
            rece.State = EntityState.Modified;
            this.context.SaveChanges();
            return rec;
        }
    }
}
