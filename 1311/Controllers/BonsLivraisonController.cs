using _1311.Models;
using _1311.Models.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using _1311.Models.Repository.IBonsLivraisonRepositorys;
using _1311.Models.Repository.IColisRepositorys;
using System.Data;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using AspNetCore.Reporting;
using Microsoft.Reporting.NETCore;
using System.Web;
using LocalReport = AspNetCore.Reporting.LocalReport;
using System.Drawing.Imaging;
using System.Diagnostics;
using _1311.Models.Repository.IUserRepositorys;

namespace _1311.Controllers
{
    public class BonsLivraisonController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext context;
        private readonly GlobalRepository<BonsLivraison> _Myrepository;
        private readonly IBonsLivraisponRepository<BonsLivraison> _BonsLivraison;
        private readonly IColisRepository<Colis> _Colis;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserRepository _User;


        public BonsLivraisonController(AppDbContext context,
            GlobalRepository<BonsLivraison> myrepository,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IBonsLivraisponRepository<BonsLivraison> bonsLivraison,
            IColisRepository<Colis> colis,
            IWebHostEnvironment webHostEnvironment,
            UserRepository user)
        {

            this.context = context;
            _Myrepository = myrepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _BonsLivraison = bonsLivraison;
            _Colis = colis;
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            _User = user;
        }

     
        [Authorize(Roles = "User")]
        [Route("User/BonsLivraison/")]
        public async Task<IActionResult> Index()
        {
            string Userid = await _User.GetUserIdAsync(User.Identity.Name);
            var bonss = _BonsLivraison.All(Userid);

            return View(bonss);

        }
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create()
        {
            string Userid = await _User.GetUserIdAsync(User.Identity.Name);
            int Id = _BonsLivraison.Add2(Userid);

            return RedirectToAction("Details", new { id = Id });

        }
        [HttpGet("/DetailsTable/{id?}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DetailsTable(int id)
        {
            string Userid = await _User.GetUserIdAsync(User.Identity.Name);

            var bonss = _BonsLivraison.Get(id, Userid);
            if (bonss is null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                dynamic model = new ExpandoObject();
                model.coulisnull = _Colis.ColisWithoutBonsiId(Userid);
                model.coulistrue = _Colis.ColisWithBonsId(Userid, bonss.id);

                return View(model);
            }
        }
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int id)
        {
            string Userid = await _User.GetUserIdAsync(User.Identity.Name);
            var bonss = _BonsLivraison.Get(id, Userid);
            ViewBag.BonsId = bonss.id;
            if (bonss is null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                dynamic model = new ExpandoObject();
                model.coulisnull = _Colis.ColisWithoutBonsiId(Userid);
                model.coulistrue = _Colis.ColisWithBonsId(Userid, bonss.id);
                return View(model);
            }
        }
        [HttpGet("/bonslivraison/{id?}/ajouter/{tr?}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Ajouter(int id, int tr)
        {
            string Userid = await _User.GetUserIdAsync(User.Identity.Name);
            var Colischerche = _Colis.Get(id, Userid);
            var Bonrecherche = _BonsLivraison.Get(tr, Userid);

            if (Colischerche != null && Bonrecherche != null)
                _BonsLivraison.AddInBons(Colischerche, Bonrecherche);

            return RedirectToAction("Details", new { id = tr });

        }
        [HttpGet("/bonslivraison/{id?}/Supprimer/{tr?}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Supprimer(int id, int tr)
        {
            string Userid = await _User.GetUserIdAsync(User.Identity.Name);
            var Colischerche = _Colis.Get(id, Userid);
            var Bonrecherche = _BonsLivraison.Get(tr, Userid);

            if (Colischerche != null && Bonrecherche != null)
                _BonsLivraison.RemoveInbons(Colischerche, Bonrecherche);

            return RedirectToAction("Details", new { id = tr });

        }
        public async Task<IActionResult> Print(int id)
        {
            string Userid = await _User.GetUserIdAsync(User.Identity.Name);

            var Bonsrecherche = _BonsLivraison.Get(id, Userid);
            IEnumerable<Colis> colisrecherche = _Colis.ColisWithBonsId(Userid, id);

            if (Bonsrecherche is null)
            {
                return RedirectToAction("index");

            }

            var dt = new DataTable();
            dt = GetData(Userid, id);
            string mimetype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\RptBonsLivraison.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm1", Bonsrecherche.Name);
            parameters.Add("User", Bonsrecherche.User.UserName);
            parameters.Add("Boutique", Bonsrecherche.User.Boutique.Name);
            parameters.Add("Nbcolis", Convert.ToString(colisrecherche.Count()));

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsBonsLivraison", dt);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);
            return File(result.MainStream, " application/pdf");



        }

        public DataTable GetData(string user, int id)
        {

            var Bonsrecherche = _BonsLivraison.Get(id, user);
            IEnumerable<Colis> colisrecherche = _Colis.ColisWithBonsId(user, id);

            var dt = new DataTable();
            dt.Columns.Add("Numero_Colis");
            dt.Columns.Add("Prix");
            dt.Columns.Add("NomComplet");
            dt.Columns.Add("ville");
            dt.Columns.Add("Telephone");
            dt.Columns.Add("adresse");
            dt.Columns.Add("CodeBare");
            DataRow Row;
            foreach (var item in colisrecherche)

            {

                Client cl = new Client();

                cl.NomComplet = item.Client.NomComplet;
                if (item.Client.Ville != null)
                {
                    cl.Ville = new Ville();
                    cl.Ville.Name = item.Client.Ville.Name;
                }
                cl.Telephone = item.Client.Telephone;
                cl.adresse = item.Client.adresse;

                Row = dt.NewRow();
                Row["Numero_Colis"] = item.Numero_Colis;
                Row["Prix"] = item.Prix;
                Row["NomComplet"] = item.Client.NomComplet;
                Row["ville"] = item.Client.Ville.Name;
                Row["Telephone"] = item.Client.Telephone;
                Row["adresse"] = item.Client.adresse;
                Row["CodeBare"] = item.CodeBarre.Chemin;
                dt.Rows.Add(Row);
            }

            return dt;
        }
        private byte[] GetImageBytes(string imagePath)
        {
            // Convert the image path to binary data
            // You can adjust this method based on how the image data is stored in your database
            string relativeImagePath = imagePath.ToString().TrimStart('/');
            string webRootPath = _webHostEnvironment.WebRootPath;
            string absoluteImagePath = Path.Combine(webRootPath, relativeImagePath);

            byte[] imageData = System.IO.File.ReadAllBytes(absoluteImagePath);

            //byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
            return imageData;
        }
        public static string ConvertImageToBase64String(string imagePath)
        {
            string result = null;
            MemoryStream ms = new MemoryStream();
            Bitmap b = new Bitmap(imagePath);

            if (!string.IsNullOrEmpty(imagePath))
            {
                using (b)
                {
                    using (ms)
                    {
                        b.Save(ms, ImageFormat.Bmp);
                        result = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            return result;
        }
        public DataTable GeneratePdfWithTickets(string user, int id)
        {
            var Bonsrecherche = _BonsLivraison.Get(id, user);
            IEnumerable<Colis> colisrecherche = _Colis.ColisWithBonsId(user, id).ToList();

            // Create a new DataTable to hold ticket data
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Prix");

            dt.Columns.Add("Phone");
            dt.Columns.Add("Adresse");
            dt.Columns.Add("Nom_Complet");
            dt.Columns.Add("Ville");
            dt.Columns.Add("Boutique");
            dt.Columns.Add("USERNAME");
            dt.Columns.Add("PhoneUser");
            dt.Columns.Add("CodeBarre");

            // Generate tickets for each colis
            foreach (var item in colisrecherche)
            {
                // Create a new row for each ticket
                DataRow Row = dt.NewRow();
                Client cl = new Client();
                cl.Telephone = item.Client.Telephone;
                Row["Name"] = item.Numero_Colis;
                Row["Nom_Complet"] = item.Client.NomComplet;

                Row["Prix"] = item.Prix;
                Row["Phone"] = item.Client.Telephone;
                Row["Adresse"] = item.Client.adresse;
                Row["Ville"] = item.Client.Ville.Name;
                Row["Boutique"] = item.User.Boutique.Name;
                Row["USERNAME"] = item.User.UserName;
                Row["PhoneUser"] = item.User.PhoneNumber;

                string relativeImagePath = item.CodeBarre.Chemin.ToString().TrimStart('/');
                string webRootPath = _webHostEnvironment.WebRootPath;
                string absoluteImagePath = Path.Combine(webRootPath, relativeImagePath);

                string result = null;
                byte[] imageData = null;
                if (System.IO.File.Exists(absoluteImagePath))
                {
                    imageData = System.IO.File.ReadAllBytes(absoluteImagePath);
                    result = Convert.ToBase64String(imageData);


                }
                Row["CodeBarre"] = result;

                dt.Rows.Add(Row);
            }

            return dt;
        }


        public async Task<IActionResult> stickers(int id)
        {
            string Userid = await _User.GetUserIdAsync(User.Identity.Name);
            var Bonsrecherche = _BonsLivraison.Get(id, Userid);

            IEnumerable<Colis> colisrecherche = _Colis.ColisWithBonsId(Userid, id).ToList();

            if (Bonsrecherche is null)
            {
                return RedirectToAction("index");

            }

            var dt = new DataTable();
            dt = GeneratePdfWithTickets(Userid, id);
            string mimetype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Sticker.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            LocalReport localReport = new LocalReport(path);

            localReport.AddDataSource("dsSticker", dt);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);
            return File(result.MainStream, " application/pdf");



        }


    }
}
