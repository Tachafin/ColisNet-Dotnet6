using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using _1311.Models;
using _1311.Models.Repository.IColisRepositorys;

namespace _1311.Models.Repository
{
    public class ListeRamassagesRepository : IListRamassageRepository<ListeRamassage>
    {
        private readonly AppDbContext context;
        private readonly IColisRepository<Colis> _Colis;


        public ListeRamassagesRepository(AppDbContext context, IColisRepository<Colis> colis)
        {
            this.context = context;
            _Colis = colis;
        }

        public void add(ListeRamassage entity)
        {
            ListeRamassage ram = new ListeRamassage();
            int count = this.context.ListeRamassage.Count();
            int id;
            if (count == 0) { id = 1; }
            else { id = this.context.ListeRamassage.Max(C => C.Id) + 1; }
            string nom = "Liste Ramassage  " + id;
            ram.Name = "Ramassage" + id;
            ram.DateCreation = DateTime.Now;
            ram.UserId = entity.UserId;
            ram.VilleId = entity.VilleId;
            ram.LivreurId = entity.LivreurId;
            ram.Etat = "En cours";
            this.context.ListeRamassage.Add(ram);
            this.context.SaveChanges();
        }



        public void AddInList(int coco, int Bonbon)
        {
            var col = this.context.Colis.SingleOrDefault(col => col.id == coco);
            var Listrec = this.context.ListeRamassage.SingleOrDefault(c => c.Id == Bonbon);

            if (col != null && Listrec != null)
            {
                col.ListeRamassageId = Listrec.Id;
                col.Etat = "Envoye";
                col.Date_Ramassage = DateTime.Now;

                this.context.SaveChanges();
            }
        }

   

        public List<ListeRamassage> All(string userid)
        {
             List<ListeRamassage> ListRamassagex= new List<ListeRamassage> (); 
            if (userid == null)
            {
                ListRamassagex = this.context.ListeRamassage
             .Include(c => c.Livreur)
             .Include(c => c.Ville)
             .Include(c => c.User)
             .Include(c => c.Livreur.User)
             .Include(c => c.Colis)
             .ToList();

                return ListRamassagex;
            }
            ListRamassagex = this.context.ListeRamassage
              .Include(c => c.Livreur)
              .Include(c => c.Ville)
              .Include(c => c.User)
              .Include(c => c.Livreur.User)
              .Include(c => c.Colis)
              .Where(c=>c.Livreur.UserId==userid)         
              .ToList();

            return ListRamassagex;
        }

        public List<Colis> AllColisHaveList(int id)
        {
            var all = this.context.Colis
                .Include(c => c.BonsLivraison)
               .Include(c => c.Client)
               .Include(c => c.User)
               .Include(c => c.Client.Ville)
               .Include(c=>c.Comments)
               .Where(c => c.ListeRamassageId == id)
               .ToList();
            return all;
        }

        public List<Colis> AllColisHaveNotList(int villeid)
        {
    
            if (villeid == 0)
            {
                var alls = this.context.Colis
                       .Include(c => c.BonsLivraison)
                        .Include(c => c.Client)
                       .Include(c => c.User)
                       .Include(c => c.Client.Ville)
                       .Where(c => c.ListeRamassageId == null)

                        .ToList();
                return alls;
            }
            else
            {
                var alls = this.context.Colis
                     .Include(c => c.BonsLivraison)
                      .Include(c => c.Client)
                     .Include(c => c.User)
                     .Include(c => c.Client.Ville)
                     .Where(c => c.ListeRamassageId == null)
                     .Where(c=>c.Client.Ville.Id==villeid)
                      .ToList();
                return alls;
            }
        }

        public void ChangeEtat(int idcolis, string etat)
        {
            var Colisx = _Colis.getwithid(idcolis);
            if (Colisx != null )
            {
                if(etat == null || etat=="Envoye" || etat == "Livre" || etat == "Retourne" || etat == "Annule")
                {
                    Colisx.Etat = etat;
                    this.context.SaveChanges();
                
                }
            }
        }

        public int CountColisEtat(int id, string etat)
        {
            if(id != null && etat != null)
            {
                int check=this.context.Colis
                    .Where(c=>c.ListeRamassageId==id)
                    .Where(c => c.Etat == etat)
                    .Count();
                return check;
            }
            return 0;
        }

        public int CountEtat(string etat)
        {
            int Nb = this.context.ListeRamassage
                 .Where(c => c.Etat == etat)
                 .Count();
            return Nb;
        }

        public void EditEtat(int id, string etat)
        {
            var list = Get(id);
            list.Etat = etat;
            this.context.SaveChanges();
        }

        public ListeRamassage Get(int id)
        {
            var list = this.context.ListeRamassage
                .Include(c=>c.Ville)
                .Include(c => c.Livreur.User)
                .Include(c=>c.Colis)
                .ThenInclude(c=>c.Client)
                .ThenInclude(c=>c.Ville)
                .Include(c=>c.User)
                .FirstOrDefault(c => c.Id == id);
            return list;
        }

        public ListeRamassage Get(int id, string userx)
        {
            var list = this.context.ListeRamassage
                           .Include(c => c.Livreur.User)
                           .Include(c=>c.Colis)
                           .ThenInclude(c=>c.Client)
                           .Where(c=>c.UserId==userx)
                           .FirstOrDefault(c => c.Id == id);
            if (list != null)
                return list;
            else
                return null;
        }

        public IEnumerable<ListeRamassage> Get(string userx)
        {
            var listx = this.context.ListeRamassage
                            .Include(c => c.Livreur.User)
                           .Include(c => c.Colis)
                           .ThenInclude(c => c.Client)
                           .Where(c => c.Livreur.UserId == userx);
            
            return listx;
        }

        public IEnumerable<ListeRamassage> Get(string etat, string userx, string start, string end)
        {

            IEnumerable<ListeRamassage> liste =
               this.context.ListeRamassage
               .Include(c => c.Colis)
               .ThenInclude(c => c.Client)
               .Include(c=>c.Livreur)
               .Include(c => c.Livreur.User)
               .Include(c=>c.Ville)
               .AsEnumerable();

            if (!string.IsNullOrEmpty(userx))
                liste = liste.Where(c => c.Livreur.UserId == userx);
            if (!string.IsNullOrEmpty(etat))
                liste = liste.Where(c => c.Etat == etat);

            if (string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                DateTime dt = new DateTime();

                if (DateTime.TryParse(start, out dt))
                {
                    liste = liste.Where(p => p.DateCreation == dt);
                }

            }

            if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                DateTime dt = new DateTime(); DateTime dt2 = new DateTime();
                DateTime.TryParse(start, out dt);
                DateTime.TryParse(end, out dt2);
                liste = liste.Where(p => p.DateCreation >= dt && p.DateCreation <= dt2);
            }




            return liste;
        }

        public void RemoveInList(int coco, int Bonbon)
        {
            var col = this.context.Colis.SingleOrDefault(col => col.id == coco);
            var Listrec = this.context.ListeRamassage.SingleOrDefault(c => c.Id == Bonbon);

            if (col != null && Listrec != null)
            {
                col.ListeRamassageId = null;
                col.Etat = "En cours";

                this.context.SaveChanges();
            }
        }

        public IEnumerable<ListeRamassage> Top5Liste(string livreurid)
        {
            IEnumerable<ListeRamassage> Liste=this.context.ListeRamassage
             
                .Where(c => c.Livreur.UserId == livreurid)
                .Include(c=>c.Livreur)
                .Include(c=>c.Colis)
                .ThenInclude(c=>c.Client)
                .Include(c=>c.Ville)
                .OrderByDescending(c => c.Id)
                .Take(5);
            return Liste;
        }

        public float TotaleAmount(int id)
        {
            var ret = this.context.Colis
                 .Where(c => c.ListeRamassageId == id)
                 .Where(c => c.Etat == "Livre")
                 .Sum(c => c.Prix);
            return ret;
        }

        public ListeRamassage Update(ListeRamassage entitychanges)
        {
            var Liste = this.context.ListeRamassage.Attach(entitychanges);
            Liste.State = EntityState.Modified;
            this.context.SaveChanges();
            return entitychanges;

        }

    }
}
