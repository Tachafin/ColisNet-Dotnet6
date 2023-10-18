using _1311.Models;
using _1311.Models.Repository;
using _1311.Models.Repository.IDemandeRepositorys;
using _1311.Models.Repository.IUserRepositorys;
using _1311.Models.ViewModels.DemandeViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Controllers
{
    public class DemandeController : Controller
    {
        private readonly AppDbContext context;
        private readonly GlobalRepository<BonsLivraison> _MyrepositoryBonsLivraison;
        private readonly IDemandeRepository<Demande> _Demande;
        private readonly GlobalRepository<Demande> _MyrepositoryDemande;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ILivreurRepository<Livreur> _Livreur;
        private readonly UserRepository _User;
        public DemandeController(AppDbContext context,
            GlobalRepository<BonsLivraison> myrepositoryBonsLivraison,

            GlobalRepository<Demande> myrepositoryDemande,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IDemandeRepository<Demande> demande, ILivreurRepository<Livreur> livreur, UserRepository user)
        {
            this.context = context;
            _MyrepositoryBonsLivraison = myrepositoryBonsLivraison;

            _MyrepositoryDemande = myrepositoryDemande;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _Demande = demande;
            _Livreur = livreur;
            _User = user;
        }
        public async Task<string> Returnuser()
        {
            string CurrentUser = User.Identity.Name;
            var userx = await userManager.FindByNameAsync(CurrentUser);
            return userx.Id;
        }
        [Route("User/Demande/")]
        public async Task<IActionResult> Index()
        {

            string UserId = await _User.GetUserIdAsync(User.Identity.Name);
            var Demandes = _Demande.All(UserId, null);
            return View(Demandes);
        }

        [HttpGet]
        [Route("User/Demande/Create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Route("User/Demande/Create")]
        public async Task<IActionResult> Create(CreateDemandeViewModel model)
        {
            string UserId = await _User.GetUserIdAsync(User.Identity.Name);

            string check = _Demande.Add(UserId, model);

            TempData["Message"] = check;
            return RedirectToAction("index");


        }
        [Route("Admin/Demande/")]
        public IActionResult All(int? etat)
        {
            AllDemandeViewModel allDemandeViewModel = new()
            {
                today = _Demande.Count("alltoday"),
                todaygood = _Demande.Count("alltodaysuccess"),
                todaybad = _Demande.Count("alltodayBad"),
                all = _Demande.Count("all")
            };

            if (etat.HasValue)
            {
                allDemandeViewModel.Demandess = _Demande.AdminAll(etat);
                return View(allDemandeViewModel);
            }

            allDemandeViewModel.Demandess = _Demande.AdminAll(null);
            return View(allDemandeViewModel);

        }

        public IActionResult DetailDemande(string id)
        {
            var Demandes = _Demande.GetAllWithString(id);
            return View(Demandes);

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.ListLivreur = _Livreur.All();
            var Demandex = _Demande.Get(id);
            if (Demandex == null)
            {
                return RedirectToAction("All");
            }

            return View(Demandex);


        }
        [HttpPost]
        public IActionResult Edit(Demande model)
        {
            if (ModelState.IsValid)
            {
                Demande Demandex = _Demande.Get(model.Id);


                if (Demandex is null)
                {
                    return RedirectToAction("All");
                }
                else
                {
                    Demandex.Nom = model.Nom;
                    Demandex.Resolu = model.Resolu;
                    Demandex.LivreurId = model.LivreurId;
                    if (model.LivreurId == null)
                    {
                        Demandex.Resolu = false;
                    }
                    else
                    {
                        Demandex.Resolu = true;
                    }
                    _Demande.update(Demandex);
                    return RedirectToAction("All");
                }



            }

            return RedirectToAction("All");


        }


    }



}

