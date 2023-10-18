using _1311.Models.ViewModels.DemandeViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1311.Models.Repository.IDemandeRepositorys
{
    public class DemandeRepositoryy : IDemandeRepository<Demande>
    {
        private readonly AppDbContext context;
        private readonly UserManager<AppUser> userManager;

        public DemandeRepositoryy(AppDbContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        //public bool Add(string user)
        //{

        //    var checkdemande = this.context.Demande
        //         .Where(x => x.UserId == user)
        //         .Count();

        //    if (checkdemande == 0)
        //    {
        //        Demande demande = new Demande();
        //        int count = this.context.Demande.Count();
        //        int id;
        //        if (count == 0) { id = 1; }
        //        else { id = this.context.Demande.Max(De => De.Id) + 1; }
        //        demande.UserId = user;
        //        demande.Nom = "Demande " + id;
        //        demande.DateCreation = DateTime.Now;

        //        this.context.Demande.Add(demande);
        //        this.context.SaveChanges();
        //        //ViewBag.Message = "Demande Successfuly";
        //        return true;
        //    }
        //    else
        //    {
        //        var lastid = this.context.Demande
        //            .Where(x => x.UserId == user)
        //            .Max(f => f.Id);

        //        Demande dmd = this.context.Demande.FirstOrDefault(a => a.Id == lastid);



        //        int res = DateTime.Compare(dmd.DateCreation, DateTime.Now);


        //        var day = DateTime.Now.Day;
        //        var Year = DateTime.Now.Year;
        //        var Month = DateTime.Now.Month;

        //        if (dmd.DateCreation.Day == day && dmd.DateCreation.Month == Month && dmd.DateCreation.Year == Year)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            Demande demande = new Demande();
        //            int count = this.context.Demande.Count();
        //            int id;
        //            if (count == 0) { id = 1; }
        //            else { id = this.context.Demande.Max(De => De.Id) + 1; }
        //            demande.UserId = user;
        //            demande.Nom = "Demande " + id;
        //            demande.DateCreation = DateTime.Now;
        //            demande.UserId = user;
        //            this.context.Demande.Add(demande);
        //            this.context.SaveChanges();
        //            return true;


        //        }
        //    }
        //}

        public string Add(string user, CreateDemandeViewModel model)
        {
            var existingDemande = context.Demande
             .FirstOrDefault(c => c.UserId == user && c.DateCreation.Date == DateTime.Now.Date);

            if (existingDemande != null)
            {
                return "Demande déjà créée pour aujourd'hui";
            }

            Demande newDemande = new Demande
            {
                UserId = user,
                Nom = "Demande-" + (DateTime.Now.Ticks / TimeSpan.TicksPerSecond), // Unique name based on timestamp
                Adresse = model.Adresse,
                DateCreation = DateTime.Now,
                Note = model.Note,
                NumeroTelephone = model.Numero_Telephone,
                Resolu = false
            };

            context.Demande.Add(newDemande);

            try
            {
                context.SaveChanges();
                return "Demande a été créée avec succès";
            }
            catch (Exception ex)
            {
                
                return "Une erreur est survenue lors de la création de la demande : " + ex.Message;
            }
        }

        public List<Demande> AdminAll(int? etat)
        {
            List<Demande> all = new List<Demande>();
            if (!etat.HasValue)
            {
                all = this.context.Demande
             .Include(c => c.User)
             //.Include(c=>c.User.Boutique.Ville)
             .Include(c => c.Livreur)
             .Include(c => c.Livreur.User)
             .ToList();
                return all;
            }
            else if (etat == 1)
            {
                all = this.context.Demande
          .Include(c => c.User)
          //.Include(c=>c.User.Boutique.Ville)
          .Include(c => c.Livreur)
          .Include(c => c.Livreur.User)
          .Where(c => c.DateCreation.Year == DateTime.Today.Year && c.DateCreation.Month == DateTime.Today.Month && c.DateCreation.Day == DateTime.Today.Day)
          .ToList();
                return all;
            }
            else if (etat == 2)
            {
                all = this.context.Demande
          .Include(c => c.User)
          //.Include(c=>c.User.Boutique.Ville)
          .Include(c => c.Livreur)
          .Include(c => c.Livreur.User)
             .Where(c => c.DateCreation.Year == DateTime.Today.Year && c.DateCreation.Month == DateTime.Today.Month && c.DateCreation.Day == DateTime.Today.Day)
          .Where(c => c.Resolu == true)
          .ToList();
                return all;
            }
            else if (etat == 3)
            {
                all = this.context.Demande
          .Include(c => c.User)
          //.Include(c=>c.User.Boutique.Ville)
          .Include(c => c.Livreur)
          .Include(c => c.Livreur.User)
              .Where(c => c.DateCreation.Year == DateTime.Today.Year && c.DateCreation.Month == DateTime.Today.Month && c.DateCreation.Day == DateTime.Today.Day)
          .Where(c => c.Resolu == false)
          .ToList();
                return all;
            }
            else
            {
                all = this.context.Demande
          .Include(c => c.User)
          //.Include(c=>c.User.Boutique.Ville)
          .Include(c => c.Livreur)
          .Include(c => c.Livreur.User)

          .ToList();
                return all;
            }

        }

        public List<Demande> All(string user, int? etat)
        {
            List<Demande> listex = new List<Demande>();
            if (etat.HasValue)
            {
                if (etat == 1)
                {
                    listex = this.context.Demande
              .Include(c => c.User)
              .Include(c => c.Livreur)
              .Include(c => c.Livreur.User)
               .Where(c => c.DateCreation.Year == DateTime.Today.Year &&
               c.DateCreation.Month == DateTime.Today.Month &&
               c.DateCreation.Day == DateTime.Today.Day)
                 .Where(a => a.Livreur.UserId == user)
              .ToList();
                }
                else if (etat == 2)
                {
                    listex = this.context.Demande
             .Include(c => c.User)
             .Include(c => c.Livreur)
             .Include(c => c.Livreur.User)
              .Where(c => c.DateCreation.Year == DateTime.Today.Year &&
              c.DateCreation.Month == DateTime.Today.Month &&
              c.DateCreation.Day == DateTime.Today.Day)
              .Where(c => c.Resolu == true)
                 .Where(a => a.Livreur.UserId == user)
             .ToList();
                }
                else if (etat == 3)
                {

                    listex = this.context.Demande
             .Include(c => c.User)
             .Include(c => c.Livreur)
             .Include(c => c.Livreur.User)
              .Where(c => c.DateCreation.Year == DateTime.Today.Year &&
              c.DateCreation.Month == DateTime.Today.Month &&
              c.DateCreation.Day == DateTime.Today.Day)
              .Where(c => c.Resolu == false)
                 .Where(a => a.Livreur.UserId == user)
             .ToList();
                }
                else
                {
                    listex = this.context.Demande
                     .Include(c => c.User)
                     .Include(c => c.Livreur)
                     .Include(c => c.Livreur.User)
                      .Where(a => a.Livreur.UserId == user)

                     .ToList();
                }
                return listex;
            }
            listex = this.context.Demande
                     .Include(c => c.User)
                     .Include(c => c.Livreur)
                     .Include(c => c.Livreur.User)
                      .Where(a => a.UserId == user)

                     .ToList();
            return listex;
        }

        public int Count(string x)
        {
            if (x == "alltoday")
            {
                var Dmd = this.context.Demande
              .Where(c => c.DateCreation.Year == DateTime.Now.Year)
              .Where(c => c.DateCreation.Month == DateTime.Now.Month)
              .Where(c => c.DateCreation.Day == DateTime.Now.Day)

             .Count();
                return Dmd;
            }
            else if (x == "alltodaysuccess")
            {
                var Dmd = this.context.Demande
                              .Where(c => c.DateCreation.Year == DateTime.Now.Year)
                              .Where(c => c.DateCreation.Month == DateTime.Now.Month)
                              .Where(c => c.DateCreation.Day == DateTime.Now.Day)
                              .Where(c => c.Resolu == true)
                             .Count();
                return Dmd;
            }
            else if (x == "alltodayBad")
            {
                var Dmd = this.context.Demande
                              .Where(c => c.DateCreation.Year == DateTime.Now.Year)
                              .Where(c => c.DateCreation.Month == DateTime.Now.Month)
                              .Where(c => c.DateCreation.Day == DateTime.Now.Day)
                              .Where(c => c.Resolu == false)
                             .Count();
                return Dmd;
            }

            else if (x == "all")
            {
                var Dmd = this.context.Demande

                            .Count();
                return Dmd;
            }
            return 0;


        }

        public int Count(string x, string userid)
        {
            if (x == "alltoday")
            {
                var Dmd = this.context.Demande
              .Where(c => c.DateCreation.Year == DateTime.Now.Year)
              .Where(c => c.DateCreation.Month == DateTime.Now.Month)
              .Where(c => c.DateCreation.Day == DateTime.Now.Day)
              .Where(c => c.Livreur.UserId == userid)
              .Count();
                return Dmd;
            }
            else if (x == "alltodaysuccess")
            {
                var Dmd = this.context.Demande
                              .Where(c => c.DateCreation.Year == DateTime.Now.Year)
                              .Where(c => c.DateCreation.Month == DateTime.Now.Month)
                              .Where(c => c.DateCreation.Day == DateTime.Now.Day)
                              .Where(c => c.Resolu == true)
                                  .Where(c => c.Livreur.UserId == userid)
                             .ToList();
                return Dmd.Count();
            }
            else if (x == "alltodayBad")
            {
                var Dmd = this.context.Demande
                              .Where(c => c.DateCreation.Year == DateTime.Now.Year)
                              .Where(c => c.DateCreation.Month == DateTime.Now.Month)
                              .Where(c => c.DateCreation.Day == DateTime.Now.Day)
                              .Where(c => c.Resolu == false)
                                  .Where(c => c.Livreur.UserId == userid)
                             .Count();
                return Dmd;
            }

            else if (x == "all")
            {
                var Dmd = this.context.Demande
                        .Where(c => c.Livreur.UserId == userid)
                            .Count();
                return Dmd;
            }
            return 0;
        }

        public Demande Get(int id)
        {
            var demandex = this.context.Demande
                 .Include(c => c.User)
                 .Where(c => c.Id == id)
                 .FirstOrDefault();
            return demandex;
        }

        public List<Demande> GetAllWithString(string x)
        {

            if (x == "now")
            {
                var all = this.context.Demande
                    .Include(c => c.Livreur)
                    .Include(c => c.Livreur.User)
                    .Include(c => c.User)
                    //.Include(c => c.User.Boutique.Ville)
                    .Where(c => c.DateCreation.Date == DateTime.Today.Date)
                    .ToList();
                return all;
            }
            else if (x == "allsuccess")
            {
                var all = this.context.Demande
                       .Include(c => c.Livreur)
                       .Include(c => c.Livreur.User)
                       .Include(c => c.User)
                       //.Include(c => c.User.Boutique.Ville)
                       .Where(c => c.DateCreation.Date == DateTime.Today.Date)
                       //.Where(c=>c.Resolu == true)
                       .ToList();
                return all;
            }
            else if (x == "all")
            {
                var all = this.context.Demande
                       .Include(c => c.Livreur)
                       //.Include(c => c.User.Boutique.Ville)
                       .Include(c => c.Livreur.User)
                       .Include(c => c.User)
                       .ToList();
                return all;
            }
            return null;
        }

        public Demande update(Demande demande)
        {
            var colis = this.context.Demande.Attach(demande);
            colis.State = EntityState.Modified;
            this.context.SaveChanges();
            return demande;
        }

       
    }
}
