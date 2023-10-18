using _1311.Models;
using _1311.Models.Repository;
using _1311.Models.Repository.Admin;
using _1311.Models.Repository.IColisRepositorys;
using _1311.Models.Repository.IUserRepositorys;
using _1311.Models.Repository.IVilleRepositorys;
using _1311.Models.ViewModels.ColisViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Controllers
{
    [Authorize]
    public class ColisController : Controller
    {
        private readonly AppDbContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IColisRepository<Colis> _Colis;
        private readonly IVilleRepository<Ville> _Ville;
        private readonly ILivreurRepository<Livreur> _Livreur;
        private readonly UserRepository _User;


        public ColisController(AppDbContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IColisRepository<Colis> colis,
            IVilleRepository<Ville> ville,
            ILivreurRepository<Livreur> livreur, CurrentUser userService, UserRepository user)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _Colis = colis;
            _Ville = ville;
            _Livreur = livreur;

            _User = user;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("User/Colis/colisenattente")]
        public async Task<IActionResult> ColisEnAttente()
        {
            string UserId = await _User.GetUserIdAsync(User.Identity.Name);
            var Allcolis = _Colis.ColisEnAttente(UserId);
            return View(Allcolis);
        }


        [Authorize(Roles = "User")]
        [Route("User/Colis/Colisenvoye")]
        public async Task<IActionResult> ColisEnvoye()
        {
            string UserId = await _User.GetUserIdAsync(User.Identity.Name);
            var Allcolis = _Colis.ColisEnvoye(UserId);
            return View(Allcolis);
        }

        [HttpPost]
        public async Task<IActionResult> create(Colis coliscreate)
        {
            if (ModelState.IsValid)
            {
                string UserId = await _User.GetUserIdAsync(User.Identity.Name);
                _Colis.Add(coliscreate, UserId);
                var redirectUrl = Url.Action("ColisEnAttente", "Colis");
                return Json(new { success = true, redirectUrl });

            }
            var AllViles = _Ville.All();
            ViewBag.ListVille = AllViles;
            ViewBag.error = "Merci de remplir tous les champs";
            return PartialView("create", coliscreate);
        }
        public async Task<IActionResult> Delete(int id)
        {

            string UserId = await _User.GetUserIdAsync(User.Identity.Name);

            if (UserId != null && id != null)
            {
                var colisx = _Colis.Get(id, UserId);
                if (colisx != null)
                {
                    _Colis.Remove(colisx);
                }
            }
            return RedirectToAction("ColisEnAttente");

        }
        public IActionResult createx()
        {
            ViewBag.ListVille = _Ville.All();
            Colis NewColis = new Colis();
            return PartialView("_addColisPartialView", NewColis);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("User/colis/create")]
        public IActionResult create()
        {
            ViewBag.ListVille = _Ville.All();
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> update(int id)
        {
            string UserId = await _User.GetUserIdAsync(User.Identity.Name);
            ViewBag.ListVille = _Ville.All();
            var colis = _Colis.Get(id, UserId);

            if (colis is null)
            {
                return RedirectToAction("index", "colis");
            }
            return View(colis);
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> update(Colis colischanged)
        {
            string UserId = await _User.GetUserIdAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                Colis Colises = _Colis.Get(colischanged.id, UserId);


                if (Colises is null)
                {
                    return RedirectToAction("ColisEnAttente");
                }
                else
                {
                    Colises.Client.NomComplet = colischanged.Client.NomComplet;
                    Colises.Client.Telephone = colischanged.Client.Telephone;
                    Colises.Client.VilleId = colischanged.Client.VilleId;
                    Colises.Client.adresse = colischanged.Client.adresse;


                    _Colis.Update(Colises);
                    return RedirectToAction("Colisenattente", "colis");
                }



            }

            return RedirectToAction("Colisenattente", "colis");


        }
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int id)
        {
            string UserId = await _User.GetUserIdAsync(User.Identity.Name);
            var colis = _Colis.Get(id, UserId);
            ViewBag.ListVille = _Ville.All();
            if (colis is null)
            {
                return RedirectToAction("ColisEnAttente");
            }
            else
            {
                return View(colis);
            }


        }

        //Admin Method


        [Authorize(Roles = "Admin,Super Admin")]
        [Route("/Admin/Colis")]
        public async Task<IActionResult> AllColisAsync()
        {
            Colis colis = new Colis();
            var model = new ColisAdminViewModel
            {
                XColis = _Colis.GetEntitiesforuser(),
                AllVille = _Ville.All("on"),
                AllColis = _Colis.CountEtat(null),
                AllColisEncours = _Colis.CountEtat("En cours"),
                AllColisEnvoye = _Colis.CountEtat("Envoye"),
                AllColisLivre = _Colis.CountEtat("Livre"),
                AllColisRetourne = _Colis.CountEtat("Retourne"),
                AllLivreur = _Livreur.All(),
                Listuser = await _User.GetUserAndAdminAsync()
            };

            return View(model);

        }

        [Route("/Admin/EditColis/{id}")]
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public IActionResult EditColis(int id)
        {
            ViewBag.ListVille = _Ville.All();
            var Colis = _Colis.getwithidAdmin(id);
            if (Colis is null)
            {
                RedirectToAction("AllColisAsync");
            }
            return View(Colis);
        }
        [Route("/Colis/GetColisWithScan/{BarCodeName?}")]
        public IActionResult GetColisWithScan(string BarCodeName = null)
        {

            if (string.IsNullOrEmpty(BarCodeName))
                return RedirectToAction("AllColis");

            var colisX = _Colis.GetColisWithScan(BarCodeName);

            if (colisX is null)
                return RedirectToAction("AllColis");

            ViewBag.ListVille = _Ville.All();
            return View("EditColis", colisX);
        }

        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> DetailsColisAsync(string clientid, int? villeid, string livreurid)
        {
            ViewBag.Listuser = await _User.GetUserAndAdminAsync();
            var colis = _Colis.DetailsColisforclientAsync(clientid, villeid, livreurid);
            return View(colis);
        }

        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> DetailsEtat(string etat)
        {
            ViewBag.Listuser = await _User.GetUserAndAdminAsync();
            var colis = _Colis.DetailsColis(etat, null);
            return View("DetailsColis", colis);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Super Admin")]
        public IActionResult Edits(Colis colischanged)
        {
            if (ModelState.IsValid)
            {
                Colis colise = _Colis.getwithidAdmin(colischanged.id);


                if (colise is null)
                {
                    return RedirectToAction("AllColis");
                }
                else
                {
                    colise.Client.NomComplet = colischanged.Client.NomComplet;
                    colise.Client.Telephone = colischanged.Client.Telephone;
                    colise.Client.VilleId = colischanged.Client.VilleId;
                    colise.Client.adresse = colischanged.Client.adresse;
                    colise.Prix = colischanged.Prix;

                    _Colis.Update(colise);
                    return RedirectToAction("AllColis");
                }

            }
            return RedirectToAction("AllColis");
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Super Admin")]
        public IActionResult EditEtat(Colis colischanged)
        {
            if (ModelState.IsValid)
            {
                Colis colise = _Colis.getwithidAdmin(colischanged.id);

                if (colise is null)
                {
                    return RedirectToAction("AllColis");
                }
                else
                {
                    colise.Etat = colischanged.Etat;
                    if (colischanged.Etat == "Livre")
                    {
                        colise.Date_Livraison = DateTime.Now;
                    }
                    else
                    {
                        colise.Date_Livraison = null;
                    }
                    _Colis.Update(colise);
                    return RedirectToAction("AllColis");
                }

            }
            return RedirectToAction("AllColis");
        }
        [HttpGet]
        //[Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> CreateColis()
        {
            ViewBag.ListVille = _Ville.All("on");
            ViewBag.Listuser = await _User.GetUserAndAdminAsync();
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> CreateColis(Colis coliscreate)
        {
            if (ModelState.IsValid)
            {
                _Colis.AddForAdmin(coliscreate, coliscreate.UserId);
                return new JsonResult("Data is nooo");
            }

            ViewBag.ListVille = _Ville.All("on");
            ViewBag.Listuser = await _User.GetUserAndAdminAsync();
            ViewBag.error = "Merci de remplir tous les champs";
            return PartialView("CreateColis", coliscreate);

        }

        [HttpGet]
        public IActionResult ShowComment(int id)
        {
            var Colis = _Colis.getwithidAdmin(id);
            return View(Colis);
        }




    }

}
