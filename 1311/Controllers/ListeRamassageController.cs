using _1311.Models;
using _1311.Models.Repository;
using _1311.Models.Repository.IUserRepositorys;
using _1311.Models.Repository.IVilleRepositorys;
using _1311.Models.ViewModels.ListeRamassageViewModels;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Controllers
{
    public class ListeRamassageController : Controller
    {
        private readonly AppDbContext context;
        private readonly IListRamassageRepository<ListeRamassage> _ListeRamassage;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        //private readonly GlobalRepository<Colis> _Myrepositorycolis;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILivreurRepository<Livreur> _Livreur;
        private readonly IVilleRepository<Ville> _Ville;
        private readonly UserRepository _User;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ListeRamassageController(AppDbContext context,
            IListRamassageRepository<ListeRamassage> myrepository,
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager, ILivreurRepository<Livreur> livreur,
            IVilleRepository<Ville> ville, IWebHostEnvironment webHostEnvironment, UserRepository user)
        {
            this.context = context;
            _ListeRamassage = myrepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            //_Myrepositorycolis = myrepositorycolis;
            _roleManager = roleManager;
            _Livreur = livreur;
            _Ville = ville;
            _webHostEnvironment = webHostEnvironment;
            _User = user;
        }



        [Authorize(Roles = "Admin,Super Admin")]
        [Route("Admin/listeramassage")]
        public async Task<IActionResult> IndexAsync(string userid, string etat, string start, string end)
        {
            ViewBag.Listuser = await _User.GetUserAndAdminAsync();
            var ListeX = _ListeRamassage.Get(etat, userid, start, end);
            return View(ListeX);
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Create()

        {
            ViewBag.ListVille = _Ville.All("on");
            ViewBag.ListLivreur = _Livreur.All();
            return View();
        }
        [Authorize(Roles = "Admin,Super Admin")]
        public IActionResult Details(int id)
        {
            DetailsListeRamassageViewModel detailsListeRamassageViewModel = new()
            {
                listRmassage = _ListeRamassage.Get(id),
                ListCity = _Ville.All(null),
                ListColisNullListe = _ListeRamassage.AllColisHaveNotList(0),
                ListColis = _ListeRamassage.AllColisHaveList(id)
            };

            return View(detailsListeRamassageViewModel);
        }
        [HttpGet("/Detailsliste/{id?}/{villeid?}")]
        [Authorize(Roles = "Admin,Super Admin")]

        public IActionResult Detailsliste(int id, int villeid)
        {
            var list = _ListeRamassage.Get(id);
            ViewBag.ListCity = _Ville.All(null);
            ViewBag.ListColisNullListe = _ListeRamassage.AllColisHaveNotList(villeid);
            ViewBag.ListColis = _ListeRamassage.AllColisHaveList(id);
            return View(list);
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Super Admin")]
        public IActionResult CreateListe(ListeRamassage List)
        {

            _ListeRamassage.add(List);

            return RedirectToAction("index");
        }
        [HttpGet("/listeramassage/{id?}/ajouter/{tr?}")]
        [Authorize(Roles = "Admin,Super Admin")]
        public IActionResult Ajouter(int id, int tr)
        {

            _ListeRamassage.AddInList(id, tr);
            return RedirectToAction("Details", new { id = tr });

        }
        [HttpGet("/listeramassage/{id?}/supprimer/{tr?}")]
        [Authorize(Roles = "Admin,Super Admin")]
        public IActionResult supprimer(int id, int tr)
        {

            _ListeRamassage.RemoveInList(id, tr);
            return RedirectToAction("Details", new { id = tr });

        }
        [HttpPost]
        [Authorize(Roles = "Admin,Super Admin")]
        public IActionResult EditEtat(ListeRamassage liste)
        {
            if (ModelState.IsValid)
            {
                ListeRamassage ListeX = _ListeRamassage.Get(liste.Id);

                if (ListeX is null)
                {
                    return RedirectToAction("AllColis");
                }
                else
                {
                    ListeX.Etat = liste.Etat;

                    _ListeRamassage.Update(ListeX);
                    return RedirectToAction("Index");
                }

            }
            return RedirectToAction("Index");
        }
        public DataTable GetData(List<Colis> AllColisX, ListeRamassage ListeX)
        {
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Price");
            dt.Columns.Add("ClientName");
            dt.Columns.Add("City");
            dt.Columns.Add("ClientPhone");
            dt.Columns.Add("Adresse");
            DataRow Row;
            foreach (var item in AllColisX)

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
                Row["Name"] = item.Numero_Colis;
                Row["Price"] = item.Prix;
                Row["ClientName"] = item.Client.NomComplet;
                Row["City"] = item.Client.Ville.Name;
                Row["ClientPhone"] = item.Client.Telephone;
                Row["Adresse"] = item.Client.adresse;
                dt.Rows.Add(Row);
            }

            return dt;
        }


        public IActionResult Print(int id)
        {
            ListeRamassage listeX = _ListeRamassage.Get(id);
            if (listeX is null) { return null; }
            List<Colis> AllColisX = listeX.Colis.ToList();

            var dt = GetData(AllColisX, listeX);
            string mimetype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Liste.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("prm1", listeX.Name);
            parameters.Add("prm2", listeX.Ville.Name);
            parameters.Add("prm3", listeX.Livreur.Name);

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsListeRamassage", dt);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);
            return File(result.MainStream, " application/pdf");


        }


    }
}

