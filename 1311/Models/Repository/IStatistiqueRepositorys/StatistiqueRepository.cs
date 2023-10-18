using _1311.Models.ViewModels;
using _1311.Models.ViewModels.VilleViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1311.Models.Repository.IStatistiqueRepositorys
{
    public class StatistiqueRepository : IstatistiqueRepository<Colis>
    {
        private readonly AppDbContext context;

        public StatistiqueRepository(AppDbContext context)
        {
            this.context = context;
        }



        public int commision(string userid)
        {
            var all = this.context.Colis
               .Include(c => c.Client)
               .Include(c => c.Client.Ville)
               .Include(c => c.User)
               .Include(c => c.User.Boutique)
               .Include(c => c.User.Boutique.Ville)
               .Where(c => c.Etat == "Livre")
               .Where(c => c.UserId == userid)
               .ToList();
            var ret = 0;
            foreach (var col in all)
            {
                if (col.Client.Ville.Name == col.User.Boutique.Ville.Name)
                {
                    ret += 20;
                }
                else
                {
                    ret += 45;
                }
            }

            return ret;
        }



        public int cost(string userid, int? villeid, string start, string end, string type)
        {
            var check = this.context.Facture.AsQueryable();
            if (!string.IsNullOrEmpty(userid))
            {
                check = check.Where(c => c.UserId == userid);

            }

            if (string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                DateTime dt = new DateTime();

                if (DateTime.TryParse(start, out dt))
                {
                    check = check.Where(p => p.DateCreation == dt);
                }

            }

            if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                DateTime dt = new DateTime(); DateTime dt2 = new DateTime();
                DateTime.TryParse(start, out dt);
                DateTime.TryParse(end, out dt2);
                check = check.Where(p => p.DateCreation >= dt && p.DateCreation <= dt2);
            }
            int ret = 0;
            if (type == "Total")
            {
                ret = (int)check
               .Where(c => c.Type == "Livre")
               .Sum(c => c.Total);
            }
            if (type == "Frais")
            {
                ret = (int)check
               .Where(c => c.Type == "Livre")
               .Sum(c => c.Frais);
            }
            if (type == "Net")
            {
                ret = (int)check
               .Where(c => c.Type == "Livre")
               .Sum(c => c.Net);
            }

            return ret;
        }

        public int costnonpayout(string userid, int? villeid, string start, string end, string type)
        {
            var check = this.context.Colis.AsQueryable();
            if (!string.IsNullOrEmpty(userid))
            {
                //check = check.Where(c => c.UserId == userid)
                //    .Where(c=>c.FactureId == null) 
                //    .Where(c=>c.Etat=="Livre" );
                check = check
                    .Where(c => c.UserId == userid)
                    .Where(c => c.FactureId == null && (c.Etat == "Livre"));
                //b9it f khasni njib li 3ndhom factureid null and etatcolis = livre

            }
            if (villeid.HasValue)
            {
                check = check.Where(c => c.Client.VilleId == villeid);

            }
            if (string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                DateTime dt = new DateTime();

                if (DateTime.TryParse(start, out dt))
                {
                    check = check.Where(p => p.Date_creation == dt);
                }

            }

            if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                DateTime dt = new DateTime(); DateTime dt2 = new DateTime();
                DateTime.TryParse(start, out dt);
                DateTime.TryParse(end, out dt2);
                check = check.Where(p => p.Date_creation >= dt && p.Date_creation <= dt2);
            }
            int ret = 0;
            if (type == "Total")
            {
                ret = (int)check

               .Sum(c => c.Prix);
            }
            if (type == "Frais")
            {
                ret = (int)check

               .Sum(c => c.Facture.Frais);
            }
            if (type == "Net")
            {
                ret = (int)check

               .Sum(c => c.Facture.Net);
            }

            return ret;
        }

        public int CountBonsLivraison(string userid)
        {
            var count = this.context.BonsLivraison
                              .Where(c => c.UserId == userid)

                              .Count();
            return count;
        }

        public int CountColis(string userid, string etat)
        {
            if (string.IsNullOrEmpty(etat))
            {
                var count = this.context.Colis
              .Where(c => c.UserId == userid)
              //.Where(c => c.Etat != "En cours")
              .Count();
                return count;
            }
            else
            {
                var count = this.context.Colis
                             .Where(c => c.UserId == userid)
                             .Where(c => c.Etat == etat)
                             .Count();
                return count;
            }
        }

        public int CountColisenattente(string userid)
        {
            var count = this.context.Colis
                  .Where(c => c.UserId == userid)
                .Where(c => c.BonsLivraisonId == null)
                .Where(c => c.Etat == "En cours")
                 .Count();
            return count;
        }

        public int CountDemande(string userid)
        {
            var count = this.context.Demande
                              .Where(c => c.UserId == userid)

                              .Count();
            return count;
        }

        public int CountFacture(string userid)
        {
            var count = this.context.Facture
                             .Where(c => c.UserId == userid)

                             .Count();
            return count;
        }

        public int CountReclamation(string userid)
        {
            var count = this.context.Reclamation
                              .Where(c => c.UserId == userid)

                              .Count();
            return count;
        }

        public int FactureImpaye(string userid)
        {
            var all = this.context.Facture.Where(c => c.UserId == userid)
            .Where(c => c.Statut == "Non Paye")
            .Count();
            return all;
        }

        public int FacturePaye(string userid)
        {
            var all = this.context.Facture.Where(c => c.UserId == userid)
                .Where(c => c.Statut == "Paye")
                .Count();
            return all;
        }

        public List<int> GetList(string userid)
        {
            List<int> All = new List<int>();
            List<int> Mois = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            foreach (var item in Mois)
            {
                var count = this.context.Colis.
              Where(c => c.UserId == userid)
              .Where(c => c.Date_Livraison.Value.Month == item)
              .Where(c => c.Etat == "Livre")
              .Count();
                All.Add(count);
            }
            return All;

        }

        public List<ListVilleViewModel> GetListeVille()
        {
            var all = this.context.Ville.Select(c =>
            new ListVilleViewModel
            {
                Name = c.Name,
                Statut = c.Statut,
                ColisEncours = this.context.Colis
                     .Where(colis => colis.Client.VilleId == c.Id && colis.Etat == "En cours").Count(),
                ColisEnvoye= this.context.Colis
                     .Where(colis => colis.Client.VilleId == c.Id && colis.Etat == "Envoye").Count(),
                ColisLivre= this.context.Colis
                     .Where(colis => colis.Client.VilleId == c.Id && colis.Etat == "Livre").Count(),
                ColisRetourne= this.context.Colis
                     .Where(colis => colis.Client.VilleId == c.Id && colis.Etat == "Retourne").Count(),
                AllColis= this.context.Colis
                     .Where(colis => colis.Client.VilleId == c.Id ).Count(),


            }
            ).ToList();
            return all;
        }

        public List<int> GetListRetourne(string userid)
        {
            List<int> All = new List<int>();
            List<int> Mois = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            foreach (var item in Mois)
            {
                var count = this.context.Colis.
              Where(c => c.UserId == userid)
              .Where(c => c.Date_Livraison.Value.Month == item)
              .Where(c => c.Etat == "Retourne")
              .Count();
                All.Add(count);
            }
            return All;
        }



        public List<LivreurViewModel> GetLivreurs(string Period, string villeid)
        {
            //if (!string.IsNullOrEmpty(Period) || !string.IsNullOrEmpty(villeid))
            //{
            //    DateTime periodx;
            //    switch (Period)
            //    {
            //        case 1:
            //            periodx = DateTime.Today;
            //            break;
            //        case 2:

            //    }
            //    var All2 = this.context.Livreur.Select(n =>
            //              new LivreurViewModel
            //              {
            //                  Name = n.Name,
            //                  Id = n.Id,
            //                  Adresse = n.Adresse,
            //                  Ville = n.Ville,

            //                  CountColis = n.ListeRamassage.SelectMany(c => c.Colis.Where(c=>c.Date_Ramassage)).Count(),
            //                  CountColisLivre = n.ListeRamassage.SelectMany(c => c.Colis.Where(c => c.Etat == "Livre")).Count(),
            //                  CountColisRetourne = n.ListeRamassage.SelectMany(c => c.Colis.Where(c => c.Etat == "Retourne")).Count(),

            //              }
            //              ).ToList();


            //    return All2;
            //}
            var All = this.context.Livreur.Select(n =>
        new LivreurViewModel
        {
            Name = n.Name,
            Id = n.Id,
            Adresse = n.Adresse,
            Ville = n.Ville,

            CountColis = n.ListeRamassage.SelectMany(c => c.Colis).Count(),
            CountColisLivre = n.ListeRamassage.SelectMany(c => c.Colis.Where(c => c.Etat == "Livre")).Count(),
            CountColisRetourne = n.ListeRamassage.SelectMany(c => c.Colis.Where(c => c.Etat == "Retourne")).Count(),

        }
        ).ToList();


            return All;

        }

        public List<LivreurViewModel> GetLivreurss()
        {
            var All = this.context.Livreur.Select(n =>
             new LivreurViewModel
             {
                 Name = n.Name,
                 Id = n.Id,
                 Adresse = n.Adresse,
                 Ville = n.Ville,

                 CountColis = n.ListeRamassage.SelectMany(c => c.Colis).Count(),
                 CountColisLivre = n.ListeRamassage.SelectMany(c => c.Colis.Where(c => c.Etat == "Livre")).Count(),
                 CountColisRetourne = n.ListeRamassage.SelectMany(c => c.Colis.Where(c => c.Etat == "Retourne")).Count(),

             }
             ).ToList();


            return All;
        }

        public int net(string userid)
        {
            var ret = 0;
            ret = totale(userid) - commision(userid);
            return ret;
        }



        public int ReclamationEncours(string userid)
        {
            var all = this.context.Reclamation.Where(c => c.UserId == userid)
                .Where(c => c.Statut == false)
                .Count();
            return all;
        }

        public int Reclamationtraite(string userid)
        {
            var all = this.context.Reclamation.Where(c => c.UserId == userid)
                 .Where(c => c.Statut == true)
                 .Count();
            return all;
        }

        public int totale(string userid)
        {
            var check = this.context.Colis.Where(c => c.UserId == userid)
            .Where(c => c.Etat == "Livre")
            .Sum(c => c.Prix);
            return (int)check;
        }


    }
}