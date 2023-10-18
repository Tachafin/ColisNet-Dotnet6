using _1311.Models;
using _1311.Models.Repository;
using _1311.Models.Repository.IColisRepositorys;
using _1311.Models.Repository.IDemandeRepositorys;
using _1311.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Controllers
{

    public class LivreurController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ILivreurRepository<Livreur> _Myrepository;
        private readonly IColisRepository<Colis> _Colis;
        private readonly IListRamassageRepository<ListeRamassage> _Liste;
        private readonly IDemandeRepository<Demande> _Demande;
        //private readonly LivreurRepository _Livreur;

        public LivreurController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, ILivreurRepository<Livreur> myrepository/*, LivreurRepository livreur*/, IColisRepository<Colis> colis, IListRamassageRepository<ListeRamassage> liste, IDemandeRepository<Demande> demande)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _roleManager = roleManager;
            _Myrepository = myrepository;
            _Colis = colis;
            _Liste = liste;
            _Demande = demande;
            //_Livreur = livreur;
        }
        public async Task<string> Returnuser()
        {
            string CurrentUser = User.Identity.Name;
            var userx = await userManager.FindByNameAsync(CurrentUser);

            return userx.Id;
        }
        public async Task<IActionResult> Dashboard()
        {
            string userid = await Returnuser();
            ViewBag.ColisEnCours =_Colis.CountEtatForLivreur("Envoye",userid,null,null);
            ViewBag.ColisLivre = _Colis.CountEtatForLivreur("Livre", userid, null, null);
            ViewBag.ColisRetourne = _Colis.CountEtatForLivreur("Retourne", userid, null, null);
            ViewBag.ListeRamassageEncours=_Colis.CountEtatForLivreur(null, userid, null,"Envoye");
            ViewBag.ListeRamassageLivre = _Colis.CountEtatForLivreur(null, userid, null, "Completed");
            ViewBag.Top5Colis = _Colis.Top5(userid);
            ViewBag.Top5Liste = _Liste.Top5Liste(userid);
            return View();

        }
        public async Task<IActionResult> ListeRamassage()
        {
            string userid = await Returnuser();

            var all = _Liste.All(userid);


            return View(all);

        }
        public async Task<IActionResult> Show(int id)
        {
            string userid = await Returnuser();

            var getListe = _Liste.Get(id);
            if(getListe == null)
            {
                return RedirectToAction("ListeRamassage");
            }
            ViewBag.ListColisForListId = _Liste.AllColisHaveList(id);
            ViewBag.ListColisEnvoyeForList = _Liste.CountColisEtat(id, "Envoye");
            ViewBag.ListColisLivreForList = _Liste.CountColisEtat(id, "Livre");
            ViewBag.ListColisRetourneForList = _Liste.CountColisEtat(id, "Retourne");
            ViewBag.totalAmount = _Liste.TotaleAmount(id);
            return View(getListe);

        }
        public async Task<IActionResult>  ColisAsync(string livreur)
        {
            List<AppUser> model = new List<AppUser>();

            foreach (AppUser userx in userManager.Users)
            {

                if (await userManager.IsInRoleAsync(userx, "User") && !await userManager.IsInRoleAsync(userx, "Admin"))
                {
                    model.Add(userx);
                }
            }
            ViewBag.Listuser = model;
            string UserId = await Returnuser();
            ViewBag.AllColis = _Colis.CountEtatForLivreur(null, UserId, null, null);
            ViewBag.AllColisEncours = _Colis.CountEtatForLivreur("En cours", UserId, null, null);
            ViewBag.AllColisEnvoye = _Colis.CountEtatForLivreur("Envoye", UserId, null, null);
            ViewBag.AllColisLivre = _Colis.CountEtatForLivreur("Livre", UserId, null, null);
            ViewBag.AllColisRetourne = _Colis.CountEtatForLivreur("Retourne", UserId, null, null);
            var colis = _Colis.ColisForLivreur(UserId, livreur);
            return View(colis);

        }
        public async Task<IActionResult> AllColis(string livreur)
        {
            List<AppUser> model = new List<AppUser>();

            foreach (AppUser userx in userManager.Users)
            {

                if (await userManager.IsInRoleAsync(userx, "User") && !await userManager.IsInRoleAsync(userx, "Admin"))
                {
                    model.Add(userx);
                }
            }
            ViewBag.Listuser = model;
            string UserId = await Returnuser();
            ViewBag.AllColis = _Colis.CountEtatForLivreur(null, UserId, null, null);
            ViewBag.AllColisEncours = _Colis.CountEtatForLivreur("En cours", UserId, null, null);
            ViewBag.AllColisEnvoye = _Colis.CountEtatForLivreur("Envoye", UserId, null, null);
            ViewBag.AllColisLivre = _Colis.CountEtatForLivreur("Livre", UserId, null, null);
            ViewBag.AllColisRetourne = _Colis.CountEtatForLivreur("Retourne", UserId, null, null);
            var colis = _Colis.ColisForLivreur(UserId, livreur);
            return View(colis);

        }
        [Route("/Admin/Livreur")]
        public  IActionResult Index()
        {
            var AllLivreurs = _Myrepository.All();
            return View(AllLivreurs);

        }
        public async Task<IActionResult> DemandeRamassageAsync(int? etat)
        {
           
            string UserId = await Returnuser();
            ViewBag.alltoday = _Demande.Count("alltoday", UserId);
            ViewBag.alltodaysuccess = _Demande.Count("alltodaysuccess", UserId);
            ViewBag.alltodaybad = _Demande.Count("alltodayBad", UserId);
            ViewBag.all = _Demande.Count("all", UserId);
            var alldemande = new List<Demande>();
            if (etat.HasValue)
            {
                alldemande = _Demande.All(UserId,etat);
                return View(alldemande);
            }
            alldemande = _Demande.All(UserId,null);
            return View(alldemande);

        }
        [HttpGet]
        [Route("/Admin/Livreur/create")]
        public ActionResult Create()
        {
            ViewBag.ListVille = this.context.Ville.ToList();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(RegisterLivreurViewModel modelx)
        {
            if (ModelState.IsValid)
            {
                string fullname = modelx.Nom + "  " + modelx.Prenom;
                var userx = new AppUser
                {
                    FirstName = modelx.Nom,
                    LastName = modelx.Prenom,
                    UserName = fullname,
                    Email = modelx.Email,
                    
                    PhoneNumber = modelx.Numero_Telephone

                };


                var result = await userManager.CreateAsync(userx, modelx.Password);

                if(result.Succeeded)
                {
                   var res= await userManager.AddToRoleAsync(userx, "Livreur");
                    _Myrepository.Add(modelx,userx.Id);
                    return RedirectToAction("index");
                }
            }

            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.ListVille = this.context.Ville.ToList();
            var model = _Myrepository.Get(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Livreur model)
        {
            if (ModelState.IsValid)
            {
                Livreur liv  = _Myrepository.Get(model.Id);

                if (liv is null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string username = model.User.FirstName + "_" + model.User.LastName;
                    liv.User.UserName = username;
                    liv.VilleId = model.VilleId;
                    liv.Adresse = model.Adresse;
                    liv.User.Email = model.User.Email;
                    liv.User.FirstName = model.User.FirstName;
                    liv.User.LastName = model.User.LastName;
                    liv.User.PhoneNumber = model.User.PhoneNumber;
                    _Myrepository.Update(liv);
                }
                   
                

                  
                    return RedirectToAction("index", "livreur");
                }

            return View();

            }

        [HttpGet("/Colis/{id?}/changeEtat/{id2?}")]
        public async Task<IActionResult> changeEtat(int id,string id2)
        {
            _Liste.ChangeEtat(id, id2);
            return RedirectToAction("Show", new { id = 0 });
        }


        }
    }

