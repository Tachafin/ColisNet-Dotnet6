using _1311.Models;
using _1311.Models.Repository.IBoutiqueRepositorys;
using _1311.Models.Repository.IColisRepositorys;
using _1311.Models.Repository.IStatistiqueRepositorys;
using _1311.Models.Repository.IUserRepositorys;
using _1311.Models.Repository.IVilleRepositorys;
using _1311.Models.ViewModels.HomeViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IstatistiqueRepository<Colis> _Statistique;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IVilleRepository<Ville> _Ville;
        private readonly ILivreurRepository<Livreur> _Livreur;
        private readonly IBoutiqueRepositoryy<Boutique> _Boutique;
        private readonly IColisRepository<Colis> _Colis;
        private readonly UserRepository _User;

        public HomeController(ILogger<HomeController> logger, IstatistiqueRepository<Colis> statistique,
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IVilleRepository<Ville> ville, ILivreurRepository<Livreur> livreur,
            IBoutiqueRepositoryy<Boutique> boutique, IColisRepository<Colis> colis, UserRepository user)
        {
            _logger = logger;
            _Statistique = statistique;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _Ville = ville;
            _Livreur = livreur;
            _Boutique = boutique;
            _Colis = colis;
            _User = user;

        }
        public async Task<string> Returnuser()
        {
            string CurrentUser = User.Identity.Name;
            var userx = await userManager.FindByNameAsync(CurrentUser);

            return userx.Id;
        }

        [HttpGet]
        [Route("/User/Dashboard")]
        public async Task<IActionResult> IndexAsync()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return View("~/Views/Authentification/Client.cshtml");
            }
            string username = User.Identity.Name;
            string UserId = await _User.GetUserIdAsync(username);

            HomeIndexViewModel homeIndexViewModel = new()
            {
                listcolisenattente = _Statistique.CountColisenattente(UserId),
                CountColis = _Statistique.CountColis(UserId, null),
                CountColisLivre = _Statistique.CountColis(UserId, "Livre"),
                CountBonsLivraison = _Statistique.CountBonsLivraison(UserId),
                CountFacture = _Statistique.CountFacture(UserId),
                CountDemande = _Statistique.CountDemande(UserId),
                CountReclamation = _Statistique.CountReclamation(UserId),

                FacturePaye = _Statistique.FacturePaye(UserId),

                FactureImpaye = _Statistique.FactureImpaye(UserId),
                ReclamationTraite = _Statistique.Reclamationtraite(UserId),

                totale = _Statistique.totale(UserId),
                commision = _Statistique.commision(UserId),
                net = _Statistique.net(UserId)


            };
            return View(homeIndexViewModel);

        }

        [HttpGet]
        [Route("/Admin/Dashboard")]
        public async Task<IActionResult> DashboardAsync(string userid, int? villeid, string start, string end)
        {
            AdminHomeIndexViewModel adminHomeIndexViewModel = new AdminHomeIndexViewModel()
            {
                Listuser = await _User.GetUserAndAdminAsync(),
                Listville = _Ville.All("on"),
                ListLivreur = _Livreur.All(),
                Boutique = _Boutique.GetAll(),
                    
            };
            
            if (!string.IsNullOrEmpty(userid) || villeid.HasValue || !string.IsNullOrEmpty(start) || !string.IsNullOrEmpty(end))
            {
                var FilterColis = _Colis.Filtre(userid, villeid, start, end);
                adminHomeIndexViewModel.Colis = FilterColis;
                if (!string.IsNullOrEmpty(userid))
                {
                    adminHomeIndexViewModel.allfacture = _Statistique.CountFacture(userid);
                    adminHomeIndexViewModel.facturepaye = _Statistique.FacturePaye(userid);
                    adminHomeIndexViewModel.factureimpaye = _Statistique.FactureImpaye(userid);
                    adminHomeIndexViewModel.totale = _Statistique.cost(userid, villeid, start, end, "Total");
                    adminHomeIndexViewModel.Commission = _Statistique.cost(userid, villeid, start, end, "Frais");
                    adminHomeIndexViewModel.net = _Statistique.cost(userid, villeid, start, end, "Net");
                    adminHomeIndexViewModel.totalenonpayout = _Statistique.costnonpayout(userid, villeid, start, end, "Total");
                    adminHomeIndexViewModel.Commissionnonpayout = _Statistique.costnonpayout(userid, villeid, start, end, "Frais");
                    adminHomeIndexViewModel.netnonpayout = _Statistique.costnonpayout(userid, villeid, start, end, "Net");

                }
                adminHomeIndexViewModel.all = FilterColis.Count();
                adminHomeIndexViewModel.Encours = FilterColis.Count(c => c.Etat == "Envoye");
                adminHomeIndexViewModel.Livre = FilterColis.Count(c => c.Etat == "Livre");
                adminHomeIndexViewModel.Retourne = FilterColis.Count(c => c.Etat == "Retourne");
             

                return View(adminHomeIndexViewModel);
            }

            return View(adminHomeIndexViewModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
