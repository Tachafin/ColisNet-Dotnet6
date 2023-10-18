using _1311.Models;
using _1311.Models.Repository.IColisRepositorys;
using _1311.Models.Repository.IfactureRepositorys;
using _1311.Models.Repository.IUserRepositorys;
using _1311.Models.ViewModels;
using _1311.Models.ViewModels.FactureViewModels;
using AspNetCore.Reporting;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Controllers
{
    public class FactureController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IFactureRepository<Facture> _Facture;
        private readonly ILivreurRepository<Livreur> _Livreur;
        private readonly AppDbContext context;
        private readonly IColisRepository<Colis> _Colis;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserRepository _User;
        public FactureController(IFactureRepository<Facture> facture,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILivreurRepository<Livreur> livreur,
            AppDbContext context,
            IColisRepository<Colis> colis,
            IWebHostEnvironment webHostEnvironment,
            UserRepository user)
        {
            _Facture = facture;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _Livreur = livreur;
            this.context = context;
            _Colis = colis;
            _webHostEnvironment = webHostEnvironment;
            _User = user;
        }
        
        [Authorize(Roles = "Admin,Super Admin")]
        [Route("/Admin/Facture")]
        public IActionResult Index()
        {
            var model = _Facture.All();
            return View(model);

        }
        [Authorize(Roles = "User")]
        [Route("Client/Facture/")]
        public async Task<IActionResult> AllAsync()
        {
            string UserId = await _User.GetUserIdAsync(User.Identity.Name);
            var model = _Facture.All(UserId);
            return View(model);
        }
        [Authorize(Roles = "Admin,Super Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateFactureViewModel model)
        {
            Facture fct = _Facture.Create(model);
            return RedirectToAction("Detail", new { id = fct.Id });
        }

        [Authorize(Roles = "Admin,Super Admin")]
        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.Listuser = await _User.GetUserAndAdminAsync();
            return View();
        }
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.ListColisFacture = _Colis.ListColisFacture(id);
            Facture fct = _Facture.Get(id);
            if (fct is null)
            {
                return RedirectToAction("index", "Facture");
            }
            return View(fct);
        }
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Detail(int id)
        {
            Facture fct = _Facture.Get(id);

            DetailFactureViewModel detailFactureViewModel = new()
            {
                ListLivreur = _Livreur.All(),
                ListColisFacture = _Colis.ListColisFacture(id),
                fct = fct
            };
            
            if (fct is null)
            {
                return RedirectToAction("index", "Facture");
            }
            if (fct.UserId is null)
            {
                detailFactureViewModel.ListColisNonFacture = _Colis.ListColisNonFacture();
            }
            else
            { 
             detailFactureViewModel.ListColisNonFacture = _Colis.ListColisNonFacture(fct.UserId); 
            }
            return View(detailFactureViewModel);
        }

        [Authorize(Roles = "Admin,Super Admin")]
        [HttpGet("/FactureDetails/{id?}")]
        public IActionResult FactureDetails(int id)
        {
            if (id == null) { return View("Error"); }

            Facture fct = _Facture.Get(id);
            if (fct == null) { return View("Error"); }
            ViewBag.ListColisFacture = _Colis.ListColisFacture(id);
            ViewBag.ListColisNonFacture = _Colis.ListColisNonFacture(fct.UserId);
            return View(fct);

        }
        [Authorize(Roles = "Admin,Super Admin")]
        [HttpGet("/facture/{id?}/ajouter/{tr?}")]
        public IActionResult Ajouter(int id, int tr)
        {
            if (id == null || tr == null)
            {
                return View("Error");
            }
            _Facture.AddInFacture(id, tr);
            return RedirectToAction("FactureDetails", new { id = tr });

        }
        [Authorize(Roles = "Admin,Super Admin")]
        [HttpGet("/facture/{id?}/supprimer/{tr?}")]
        public IActionResult supprimer(int id, int tr)
        {

            _Facture.RemoveInFacture(id, tr);
            return RedirectToAction("FactureDetails", new { id = tr });

        }
        [HttpPost]
        [Authorize(Roles = "Admin,Super Admin")]
        public IActionResult Edit(Facture Fctx)
        {
            if (Fctx is null) { return View("Error"); }

            Facture fct = _Facture.Get(Fctx.Id);
            if (fct == null)
            {
                return View("error");
            }

            fct.Type = Fctx.Type;
            fct.LivreurId = Fctx.LivreurId;
            fct.Statut = Fctx.Statut;
            _Facture.Update(fct);
            return RedirectToAction("detail", new { id = Fctx.Id });
        }


        public DataTable GetData(int id)
        {

            Facture fctx = _Facture.Get(id);

            IEnumerable<Colis> colisrecherche = _Colis.ListColisFacture(id);

            var dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Name_Client");
            dt.Columns.Add("Prix");
            dt.Columns.Add("Phone_Number");
            dt.Columns.Add("ville");
            dt.Columns.Add("Adresse");
            dt.Columns.Add("Etat");
            dt.Columns.Add("Statut"); dt.Columns.Add("Date_Creation");
            dt.Columns.Add("Date_Ramassage");

            DataRow Row;
            foreach (var item in colisrecherche)

            {

                Client cl = new Client();

                cl.NomComplet = item.Client.NomComplet;
                cl.VilleId = item.Client.VilleId;
                cl.Telephone = item.Client.Telephone;
                cl.adresse = item.Client.adresse;

                Row = dt.NewRow();
                Row["id"] = item.id;
                Row["Phone_Number"] = item.Client.Telephone;
                Row["Prix"] = item.Prix;
                Row["Name_Client"] = item.Client.NomComplet;
                Row["ville"] = item.Client.Ville.Name;
                Row["Etat"] = item.Etat;
                Row["Statut"] = item.Statut;
                Row["Date_Creation"] = item.Date_creation;
                Row["Statut"] = item.Statut;
                Row["Date_Ramassage"] = item.Date_Ramassage;
                Row["Adresse"] = item.Client.adresse;
                dt.Rows.Add(Row);
            }

            return dt;
        }
        public async Task<IActionResult> Print(int id)
        {
            string Userid = await _User.GetUserIdAsync(User.Identity.Name);
            Facture fctx = _Facture.Get(id);

            IEnumerable<Colis> colisrecherche = _Colis.ListColisFacture(id);

            if (fctx is null)
            {
                return RedirectToAction("index");

            }

            var dt = new DataTable();
            dt = GetData(id);
            string mimetype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Facture.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm1", fctx.Nom);
            parameters.Add("User", fctx.User.UserName);
            parameters.Add("Boutique", fctx.User.Boutique.Name);
            parameters.Add("Nbcolis", Convert.ToString(colisrecherche.Count()));
            parameters.Add("Total", Convert.ToString(fctx.Total));
            parameters.Add("Frais", Convert.ToString(fctx.Frais));
            parameters.Add("Net", Convert.ToString(fctx.Net));
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsFacture", dt);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);
            return File(result.MainStream, " application/pdf");



        }

        [HttpGet]
        public ActionResult UploadRecu(int id)
        {
            var model = _Facture.Get(id);
            UploadRecuViewModel viewModel = new UploadRecuViewModel();
            viewModel.Id = model.Id;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UploadRecu(UploadRecuViewModel model)
        {
            if (model == null || model.Photo == null || model.Photo.Length == 0)
            {
                ModelState.AddModelError("Photo", "Please choose a photo to upload.");
                return View();
            }
            _Facture.UpdateImage(model);

            return RedirectToAction("Index", "Facture");
        }

        [HttpGet("/Facture/DownloadRecu")]
        public IActionResult DownloadRecu(string fileName)
        {
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileName.TrimStart('/'));

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            // Serve the file for download
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", Path.GetFileName(fileName));
        }
        [HttpGet("/Facture/VoirRecu")]
        public IActionResult VoirRecu(string Chemin)
        {
            ViewData["Chemin"] = Chemin;
            return View();
        }



    }


}
