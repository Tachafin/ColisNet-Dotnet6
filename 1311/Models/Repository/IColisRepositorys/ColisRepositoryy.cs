using _1311.Models.ViewModels;
using _1311.Models.ViewModels.ColisViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using ZXing;
using ZXing.Windows.Compatibility;

namespace _1311.Models.Repository.IColisRepositorys
{
    public class ColisRepositoryy : IColisRepository<Colis>
    {
        private readonly AppDbContext context;

        public ColisRepositoryy(AppDbContext context)
        {
            this.context = context;
        }

        public void Add(Colis entity, string userid)
        {
            // Generate the codebarreid
            int countx = this.context.CodeBarre.Count();
            int codebarreid = countx > 0 ? this.context.CodeBarre.Max(cb => cb.id) + 1 : 1;

            int count = this.context.Colis.Count();
            int id;
            if (count == 0) { id = 1; }
            else { id = this.context.Colis.Max(colisa => colisa.id) + 1; }
            string nom = "Colis " + id;

            entity.Numero_Colis = nom;
            entity.Date_creation = DateTime.Now;
            entity.Etat = "En cours";
            entity.Statut = "Non Paye";
            Client cl = new Client();

            cl.NomComplet = entity.Client.NomComplet;
            cl.Telephone = entity.Client.Telephone;
            cl.VilleId = entity.Client.VilleId;
            cl.adresse = entity.Client.adresse;

            entity.ClientId = cl.Id;
            entity.UserId = userid;

            var codeX = GenererCodeBarre();

            this.context.CodeBarre.Add(codeX);
            try
            {
                this.context.SaveChanges();

            }
            catch (Exception ex)
            {
                var error = ex.ToString();

            }
            entity.CodeBarreId = codebarreid;
            this.context.Colis.Add(entity);
            try
            {
                this.context.SaveChanges();

            }
            catch (Exception ex)
            {
                var error = ex.ToString();

            }
        }

        public void AddForAdmin(Colis entity, string userid)
        {
            // Generate the codebarreid
            int count = this.context.CodeBarre.Count();
            int codebarreid = count > 0 ? this.context.CodeBarre.Max(cb => cb.id) + 1 : 1;

            // Set the codebarreid in the Colis entity

            int countx = this.context.Colis.Count();
            int id;
            if (countx == 0) { id = 1; }
            else { id = this.context.Colis.Max(colisa => colisa.id) + 1; }
            string nom = "Colis " + id;

            entity.Numero_Colis = nom;
            entity.Date_creation = DateTime.Now;
            entity.Etat = "En cours";
            entity.Statut = "Non Paye";
            Client cl = new Client();

            cl.NomComplet = entity.Client.NomComplet;
            cl.Telephone = entity.Client.Telephone;
            cl.VilleId = entity.Client.VilleId;
            cl.adresse = entity.Client.adresse;

            entity.ClientId = cl.Id;
            entity.UserId = userid;
            var codeX = GenererCodeBarre();

            this.context.CodeBarre.Add(codeX);
            try
            {
                this.context.SaveChanges();

            }
            catch (Exception ex)
            {
                var error = ex.ToString();

            }
            entity.CodeBarreId = codebarreid;
            this.context.Colis.Add(entity);
            try
            {
                this.context.SaveChanges();

            }
            catch (Exception ex)
            {
                var error = ex.ToString();

            }




        }
        private bool CodeBarreExistsInDatabase(string codeBarreValue)
        {

            return context.CodeBarre.Any(cb => cb.Nom == codeBarreValue);

        }
        public CodeBarre GenererCodeBarre()
        {
            // Création d'un objet de génération de code-barres
            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128, // Type de code-barres à générer
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 300, // Largeur du code-barres en pixels
                    Height = 100, // Hauteur du code-barres en pixels
                    Margin = 10 // Marge autour du code-barres en pixels
                }
            };

            // Génération d'un code-barres à 8 chiffres
            Random random = new Random();
            string valeurCodeBarre;
            do
            {
                valeurCodeBarre = random.Next(10000000, 99999999).ToString();
            } while (CodeBarreExistsInDatabase(valeurCodeBarre)); // Check if it exists in the database

            var barcodeBitmap = writer.Write(valeurCodeBarre);

            // Conversion du code-barres en image sous forme de tableau de bytes
            byte[] imageBytes;
            using (var memoryStream = new MemoryStream())
            {
                barcodeBitmap.Save(memoryStream, ImageFormat.Png);
                imageBytes = memoryStream.ToArray();

            }

            // Enregistrement de l'image du code-barres dans un emplacement spécifié, si nécessaire
            // Exemple : barcodeBitmap.Save("chemin/vers/emplacement/codebarre.png", ImageFormat.Png);

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BarCode");
            string fileName = $"{valeurCodeBarre}.png";
            string filePath = Path.Combine(folderPath, fileName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            barcodeBitmap.Save(filePath, ImageFormat.Png);
            // Retour de la valeur du code-barres
            string relativePath = $"/BarCode/{fileName}";
            CodeBarre CodeX = new CodeBarre
            {
                Nom = valeurCodeBarre,
                Chemin = relativePath,

            };

            return CodeX;
        }
        public IEnumerable<Colis> ColisEnAttente(string userid)
        {

            var all = this.context.Colis
           .Include(c => c.BonsLivraison)
           .Include(c => c.Client)
           .Include(c => c.Client.Ville)
           .Where(c => c.UserId == userid)
           //.Where(c => c.BonsLivraisonId == null)
           .Where(c => c.Etat == "En cours")
           .ToList();
            return all;
        }

        public IEnumerable<Colis> ColisEnvoye(string userid)
        {
            var all = this.context.Colis
               .Include(c => c.BonsLivraison)
               .Include(c => c.Client)
               .Include(c => c.Client.Ville)
               .Include(c => c.ListeRamassage.Livreur)
               .Where(c => c.UserId == userid)
               //.Where(c => c.BonsLivraisonId != null)
               .Where(c => c.Etat != "En cours")
               .ToList();
            return all;
        }


        public IEnumerable<Colis> ColisForLivreur(string LivreurId, string Userid)
        {

            var All = this.context.Colis
                .Include(c => c.Client)
                .Include(c => c.User)
                .Include(c => c.Client.Ville)
                .Include(c => c.ListeRamassage)
                .Include(c => c.ListeRamassage.Livreur)
                .Include(c => c.ListeRamassage.Livreur.User)
                .AsEnumerable();
            if (!string.IsNullOrEmpty(LivreurId))// userid is who created colis and livreur id is colis is affected to livreur
            {
                All = All
                .Where(c => c.ListeRamassage != null && c.ListeRamassage.Livreur != null && c.ListeRamassage.Livreur.UserId == LivreurId)
                .ToList();
            }
            if (!string.IsNullOrEmpty(Userid))
            {
                All = All
               .Where(c => c.UserId == Userid)
               .ToList();
            }


            return All;

        }

        public IEnumerable<Colis> ColisWithBonsId(string userid, int idBons)
        {
            var all = this.context.Colis
               .Include(c => c.BonsLivraison)
               .Include(c => c.Client)
               .Include(c => c.Client.Ville)
               .Include(c => c.ListeRamassage.Livreur)
               .Include(c => c.User)
               .Include(c => c.User.Boutique)
               .Include(c => c.CodeBarre)
               .Where(c => c.UserId == userid)
               .Where(c => c.BonsLivraisonId == idBons)

               .ToList();
            return all;
        }

        public IEnumerable<Colis> ColisWithoutBonsiId(string userid)
        {
            var all = this.context.Colis
               .Include(c => c.BonsLivraison)
               .Include(c => c.Client)
               .Include(c => c.Client.Ville)
               .Include(c => c.ListeRamassage.Livreur)
               .Include(c => c.User)
               .Include(c => c.User.Boutique)
                  .Include(c => c.CodeBarre)
               .Where(c => c.UserId == userid)
               .Where(c => c.BonsLivraisonId == null)
               .ToList();
            return all;
        }

        public int CountEtat(string Etat)
        {
            int count;
            if (Etat != null)
                count = this.context.Colis.Where(a => a.Etat == Etat).Count();
            else

                count = this.context.Colis.Count();

            return count;
        }


        public int CountEtatForLivreur(string Etat, string LivreurId, string etatList, string Etatdeliste)
        {
            int count;
            if (Etatdeliste != null) // count list ramassage in dashboard
            {

                count = this.context.ListeRamassage
                   .Where(a => a.Etat == Etatdeliste)
                   .Where(c => c.Livreur.UserId == LivreurId)
                   .Count();
            }
            else if (etatList != null)
            {
                count = this.context.Colis
                   .Where(a => a.Etat == Etat)
                   .Where(a => a.ListeRamassage.Livreur.UserId == LivreurId)
                   .Count();
            }
            else if (Etat != null)
                count = this.context.Colis
                    .Where(a => a.Etat == Etat)
                    .Where(a => a.ListeRamassage.Livreur.UserId == LivreurId)
                    .Count();
            else

                count = this.context.Colis
                     .Where(a => a.ListeRamassage.Livreur.UserId == LivreurId)
                    .Count();

            return count;
        }

        public IEnumerable<Colis> DetailsColis(string nom, int? villeid)
        {
            if (string.IsNullOrEmpty(nom) && !villeid.HasValue)
            {
                return null;
            }
            var colis = this.context.Colis
                  .Include(a => a.BonsLivraison)
                  .Include(a => a.ListeRamassage)
                  .Include(a => a.Client)
                  .Include(a => a.User)
                  .Include(c => c.Client.Ville)
                  .Include(c => c.ListeRamassage.Livreur)
                  .Include(c => c.ListeRamassage.Livreur.User)
                   .AsEnumerable();
            if (nom == "All") { return colis; }
            if (!string.IsNullOrEmpty(nom))
            {
                colis = colis
                  .Where(a => a.Etat == nom);
            }
            if (villeid.HasValue)
            {
                colis = colis
                  .Where(a => a.Client.VilleId == villeid);
            }

            return colis;
        }

        public IEnumerable<Colis> DetailsColisforclientAsync(string client, int? villeid, string livreurid)
        {
            if (string.IsNullOrEmpty(client) && !villeid.HasValue && string.IsNullOrEmpty(livreurid))
            {
                return null;
            }
            var colis = this.context.Colis
                  .Include(a => a.BonsLivraison)
                  .Include(a => a.ListeRamassage)
                  .Include(a => a.Client)
                  .Include(a => a.User)
                  .Include(c => c.Client.Ville)
                  .Include(c => c.ListeRamassage.Livreur)
                  .Include(c => c.ListeRamassage.Livreur.User)
                  .AsEnumerable();
            if (!string.IsNullOrEmpty(client))
            {
                colis = colis
                  .Where(a => a.UserId == client);
            }
            if (villeid.HasValue)
            {
                colis = colis
                 .Where(a => a.Client.VilleId == villeid);
            }
            if (!string.IsNullOrEmpty(livreurid))
            {
                colis = colis
                .Where(a => a.ListeRamassage != null && a.ListeRamassage.Livreur != null && a.ListeRamassage.Livreur.UserId == livreurid);
            }
            return colis;
        }

        public IEnumerable<Colis> Filtre(string userid, int? villeid, string start, string end)
        {
            var all = this.context.Colis.AsQueryable();
            if (!string.IsNullOrEmpty(userid))
            {
                all = all.Where(p => p.UserId == userid);
            }
            if (villeid.HasValue)
            {
                all = all.Where(p => p.Client.VilleId == villeid);
            }
            if (string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                DateTime dt = new DateTime();

                if (DateTime.TryParse(start, out dt))
                {
                    all = all.Where(p => p.Date_creation == dt);
                }

            }

            if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
            {
                DateTime dt = new DateTime(); DateTime dt2 = new DateTime();
                DateTime.TryParse(start, out dt);
                DateTime.TryParse(end, out dt2);
                all = all.Where(p => p.Date_creation >= dt && p.Date_creation <= dt2);
            }
            return all;
        }

        public Colis Get(int id, string userid)
        {
            Colis col = context.Colis
                 .Include(c => c.Client)
                 .Include(c => c.BonsLivraison)
                 .Where(c => c.UserId == userid)
                 .SingleOrDefault(a => a.id == id);
            if (col is null)
            {
                return null;
            }

            return col;
        }

     
        public Colis getwithid(int id)
        {
            Colis col = this.context.Colis
                 .Include(c => c.Client)
                 .Include(c => c.BonsLivraison)
                 .Include(c => c.User)
                 .Include(c => c.User.Boutique)
                 .Include(c => c.User.Boutique.Ville)
                 .Include(c => c.Client.Ville)
                 .SingleOrDefault(c => c.id == id);
            if (col is null)
            {
                return null;
            }

            return col;
        }

        public Colis getwithidAdmin(int id)
        {
            Colis col = this.context.Colis
               .Include(c => c.Client)
               .Include(c => c.BonsLivraison)
               .Include(c => c.User)
               .Include(c => c.User.Boutique)
               .Include(c => c.User.Boutique.Ville)
               .Include(c => c.Client.Ville)
               .Include(c => c.Comments)
               .ThenInclude(c => c.User)
               .Include(c => c.CodeBarre)
               

               .SingleOrDefault(c => c.id == id);
            if (col is null)
            {
                return null;
            }
            col.Comments = col.Comments.OrderByDescending(comment => comment.DateCreation).ToList();
            return col;
        }

        public List<Colis> ListColisFacture(int id)
        {
            if (id == 0)
            {
                return null;
            }
            var all = this.context.Colis
                .Include(c => c.BonsLivraison)
                .Include(c => c.Facture)
                .Include(c => c.User)
               .Include(c => c.Client)
               .Include(c => c.Client.Ville)
               .Include(c => c.ListeRamassage)
                .Include(c => c.ListeRamassage.Livreur)
                .Include(c => c.ListeRamassage.Livreur.User)
                .Where(c => c.FactureId == id)
                .ToList();
            return all;
        }

        public List<Colis> ListColisNonFacture()
        {
            var all = this.context.Colis
                .Include(c => c.User)
               .Include(c => c.Facture)
               .Include(c => c.BonsLivraison)
               .Include(c => c.Client)
               .Include(c => c.Client.Ville)
               .Include(c => c.ListeRamassage)
               .Include(c => c.ListeRamassage.Livreur)
                .Include(c => c.ListeRamassage.Livreur.User)
                .Where(c => c.FactureId == null)
                .Where(c => c.Etat == "Livre")
                .ToList();
            return all;
        }

        public List<Colis> ListColisNonFacture(string user)
        {
            var all = this.context.Colis
                 .Include(c => c.User)
                .Include(c => c.BonsLivraison)
                .Include(c => c.Client)
                .Include(c => c.Client.Ville)
                .Include(c => c.ListeRamassage.Livreur)
                 .Include(c => c.ListeRamassage.Livreur.User)
                 .Where(c => c.FactureId == null)
                 .Where(c => c.Etat == "Livre")
                 .Where(c => c.UserId == user)
                 .ToList();
            return all;
        }

        public IEnumerable<Colis> Top5(string livreurid)
        {
            IEnumerable<Colis> colisx
                = this.context.Colis
               .Include(c => c.User)
                .Include(c => c.BonsLivraison)
                .Include(c => c.Client)
                .Include(c => c.Client.Ville)
                .Include(c => c.ListeRamassage.Livreur)
                 .Include(c => c.ListeRamassage.Livreur.User)

                .Where(c => c.ListeRamassage.Livreur.User.Id == livreurid)
                .OrderByDescending(c => c.id)
                .Take(5);
            return colisx;
        }

        public Colis Update(Colis entitychanges)
        {
            var colis = this.context.Colis.Attach(entitychanges);
            colis.State = EntityState.Modified;
            this.context.SaveChanges();
            return entitychanges;
        }

        public Colis GetColisWithScan(string BarCode)
        {
            var colis = this.context.Colis
                 .Include(c => c.Client)
               .Include(c => c.BonsLivraison)
               .Include(c => c.User)
               .Include(c => c.User.Boutique)
               .Include(c => c.User.Boutique.Ville)
               .Include(c => c.Client.Ville)
               .Include(c => c.Comments)
               .Include(c => c.CodeBarre)

               .SingleOrDefault(c => c.CodeBarre.Nom == BarCode);
            return colis;
        }

        public bool AddCodeBareForColis()
        {
            var all = this.context.Colis
                .Include(c => c.CodeBarre)
                .Where(c => c.CodeBarreId == null)
                .ToList();
            if (all is not null)
            {
                foreach (var item in all)
                {
                    int count = this.context.CodeBarre.Count();
                    int codebarreid = count > 0 ? this.context.CodeBarre.Max(cb => cb.id) + 1 : 1;

                    if (item.CodeBarreId == null)
                    {
                        var codeX = GenererCodeBarre();

                        this.context.CodeBarre.Add(codeX);
                        try
                        {
                            this.context.SaveChanges();

                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();

                        }
                        item.CodeBarreId = codebarreid;

                        try
                        {
                            this.context.SaveChanges();

                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();

                        }
                    }

                }
                return true;
            }
            return true;
        }

        public IEnumerable<GetColisAdmin> GetEntitiesforuser()
        {
            var colis = context.Colis
        .Include(c => c.Client)
        .Include(c => c.User)
        .Include(c => c.Client.Ville)
        .Include(c => c.ListeRamassage)
        .Include(c => c.ListeRamassage.Livreur)
        .Include(c => c.ListeRamassage.Livreur.User)
        .Select(c => new GetColisAdmin
        {
            Id = c.id,
            Nom_Colis = c.Numero_Colis,
            Nom_de_client = c.Client.NomComplet,
            NumeroTelephone = c.Client.Telephone,
            Ville = c.Client.Ville.Name,
            Adresse = c.Client.adresse,
            Prix = c.Prix,
            Etat = c.Etat,
            Statut = c.Statut,
            DateCreation = c.Date_creation,
            User = c.User.UserName,
            Livreur = c.ListeRamassage.Livreur.Name,

            LastComment = c.Comments.OrderByDescending(comment => comment.DateCreation)
                                  .FirstOrDefault() != null ? c.Comments.OrderByDescending(comment => comment.DateCreation).FirstOrDefault().Message : null

        })
        .ToList();
            return colis;
        }

        public void Remove(Colis entity)
        {
            context.Colis.Remove(entity);
            context.SaveChanges();
        }
    }
}
