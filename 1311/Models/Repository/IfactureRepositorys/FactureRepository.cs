using _1311.Models.Repository.IColisRepositorys;
using _1311.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Models.Repository.IfactureRepositorys
{
    public class FactureRepository : IFactureRepository<Facture>
    {
        private readonly AppDbContext _context;
        private readonly IColisRepository<Colis> _Colis;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FactureRepository(AppDbContext context, IColisRepository<Colis> colis, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _Colis = colis;
            _webHostEnvironment = webHostEnvironment;
        }

        public void AddInFacture(int coco, int Bonbon)
        {
            var col = _Colis.getwithid(coco);
            var Fact = Get(Bonbon);

            if (col != null && Fact != null &&
                col.FactureId == null &&
                Fact.UserId == col.UserId &&
                col.Statut == "Non Paye")
            {
                col.FactureId = Fact.Id;
                col.Statut = "Paye";
                Regler(Fact, col);
                this._context.SaveChanges();

            }
        }

        public List<Facture> All()
        {
            var all = this._context.Facture
                .Include(c => c.User)
                .Include(c => c.Livreur)
                .Include(c=>c.Recu_Paiement)
                .ToList();
            return all;
        }

        public List<Facture> All(string id)
        {
            var all = this._context.Facture
                 .Include(c => c.Recu_Paiement)
               .Where(c => c.UserId == id)
               .ToList();
            return all;
        }

        public Facture Create(CreateFactureViewModel model)
        {
            Facture facturex = new Facture();
            int count = this._context.Facture.Count();
            int ids;
            if (count == 0) { ids = 1; }
            else { ids = this._context.Facture.Max(colisa => colisa.Id); }
            facturex.Nom = "Facture " + ids + 1;
            facturex.DateCreation = System.DateTime.Now;
            facturex.Total = 0;
            facturex.Net = 0;
            facturex.Frais = 0;
            facturex.Statut = "Non Paye";
            facturex.Type = "En cours";
            facturex.UserId = model.User;
            this._context.Facture.Add(facturex);
            this._context.SaveChanges();
            return facturex;
        }

        public Facture Get(int id)
        {
            var facture = this._context.Facture
              .Include(c => c.User)
              .Include(c => c.User.Boutique)
              .Include(c => c.User.Boutique.Bank)
              .Include(c => c.User.Boutique.Ville)
              .Include(c => c.User.Colis)
              .Include(c => c.Colis)
              .ThenInclude(c => c.User.Boutique.Ville)
              .Include(c => c.Colis)
              .ThenInclude(c => c.Client.Ville)
              .Include(c=>c.Recu_Paiement)
               .FirstOrDefault(c => c.Id == id);
            return (facture);
        }

        public void Regler(Facture fct, Colis col)
        {
            Facture fctx = Get(fct.Id);
            if (fctx != null || col != null)
            {
                if (fctx.Total is null)
                {
                    fctx.Total = (int?)col.Prix;
                }
                else
                {
                    fctx.Total += (int?)col.Prix;
                }


                if (col.Client.Ville.Name == col.User.Boutique.Ville.Name)
                {
                    if (fctx.Frais is null)
                    {
                        fctx.Frais = 20;
                    }
                    else { fctx.Frais += 20; }

                }
                else
                {
                    if (fctx.Frais is null)
                    {
                        fctx.Frais = 45;
                    }
                    else { fctx.Frais += 45; }
                }
                fctx.Net = fctx.Total - fctx.Frais;
                Update(fctx);
            }

        }

        public void ReglerMoins(Facture fct, Colis col)
        {
            if (fct != null || col != null)
            {
                fct.Total -= (int?)col.Prix;
                if (col.Client.Ville.Name == col.User.Boutique.Ville.Name)
                {
                    fct.Frais -= 20;
                }
                else
                {
                    fct.Frais -= 45;
                }
                fct.Net = fct.Total - fct.Frais;
                this._context.SaveChanges();
            }
        }

        public void RemoveInFacture(int coco, int Bonbon)
        {
            var col = _Colis.getwithid(coco);

            var Fact = Get(Bonbon);

            if (col != null && Fact != null &&
                col.FactureId != null &&
                Fact.UserId == col.UserId &&
                col.Statut == "Paye")
            {
                col.FactureId = null;
                col.Statut = "Non Paye";

                this._context.SaveChanges();
                ReglerMoins(Fact, col);
            }
        }

        public Facture Update(Facture entitychanges)
        {
            var Facturex = this._context.Facture.Attach(entitychanges);
            Facturex.State = EntityState.Modified;
            this._context.SaveChanges();
            return entitychanges;
        }

        public void UpdateImage(UploadRecuViewModel model)
        {
            var factureX = _context.Facture.Find(model.Id);

            if (factureX.Recu_PaiementId.HasValue)
            {
                var oldRecu = _context.Recu_Paiement.Find(factureX.Recu_PaiementId);
                if (oldRecu != null)
                {
                    string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, oldRecu.Chemin.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                    _context.Recu_Paiement.Remove(oldRecu);
                }
            }
            int countx = this._context.Recu_Paiement.Count();
            int RecuId = countx > 0 ? this._context.Recu_Paiement.Max(cb => cb.Id) + 1 : 1;

            if (model.Photo != null && model.Photo.Length > 0)
            {
                // Generate a unique filename using GUID
                string uniqueFileName = Guid.NewGuid().ToString();
                string fileExtension = Path.GetExtension(model.Photo.FileName);

                // Construct the full path
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "RecuPaiement", uniqueFileName);
                string filePathWithExtension = filePath + fileExtension;
                string uniqueFileNameWithExtension = uniqueFileName + fileExtension;
                // Save the uploaded file to the specified path
                using (var stream = new FileStream(filePathWithExtension, FileMode.Create))
                {
                    model.Photo.CopyTo(stream);
                }
                string relativePath = $"/RecuPaiement/{uniqueFileNameWithExtension}";
                // Create a new Recu_Paiement entity
                var recu = new Recu_Paiement
                {
                    Nom = uniqueFileName+ fileExtension,
                    Chemin = relativePath
                    // Set other properties as needed
                };

                // Save the entity to the database
                _context.Recu_Paiement.Add(recu);
                _context.SaveChanges();

                factureX.Recu_PaiementId = RecuId;
                _context.SaveChanges();
          
              
            }
        }
    }
}