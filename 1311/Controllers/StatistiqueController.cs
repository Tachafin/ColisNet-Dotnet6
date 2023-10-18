using _1311.Models;
using _1311.Models.Repository.IStatistiqueRepositorys;
using _1311.Models.Repository.IVilleRepositorys;
using Microsoft.AspNetCore.Mvc;

namespace _1311.Controllers
{
    public class Statistique : Controller
    {
        private readonly ILivreurRepository<Livreur> _Livreur;
        private readonly IstatistiqueRepository<Colis> _Statistique;
        private readonly IVilleRepository<Ville> _ville;

        public Statistique(ILivreurRepository<Livreur> livreur
, IstatistiqueRepository<Colis> statistique,
IVilleRepository<Ville> ville)
        {
            _Livreur = livreur;
            _Statistique = statistique;
            _ville = ville;
        }
        [HttpGet()]
        public IActionResult Livreur()
        {
            ViewBag.AllLivreur = _Statistique.GetLivreurss();

            return View();
        }
        [HttpGet()]
        public IActionResult Ville()    
        {
            ViewBag.AllLivreur = _Statistique.GetLivreurss();
            ViewBag.AllVille = _ville.All();
            ViewBag.Ville=_Statistique.GetListeVille();
            return View();
        }
    }
}
