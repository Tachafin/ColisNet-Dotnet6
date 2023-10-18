using _1311.Models;
using _1311.Models.Repository.IStatistiqueRepositorys;
using _1311.Models.ViewModels.HomeViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _1311.Controllers
{
    public class UserController : Controller
    {
        private readonly CurrentUser _currentUser;
        private readonly IstatistiqueRepository<Colis> _Statistique;

        public UserController(CurrentUser currentUser, IstatistiqueRepository<Colis> statistique)
        {
            _currentUser = currentUser;
            _Statistique = statistique;
        }

        [HttpGet]

        public async Task<IActionResult> IndexAsync()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return View("~/Views/Authentification/Client.cshtml");
            }
            string UserId = await _currentUser.GetUserIdAsync(User.Identity.Name);
        
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
            return View("~/Views/Home/Index.cshtml", homeIndexViewModel);

        }
    }
}
