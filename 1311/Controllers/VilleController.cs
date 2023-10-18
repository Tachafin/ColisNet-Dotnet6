using _1311.Models;
using _1311.Models.Repository.IVilleRepositorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _1311.Controllers
{
    public class VilleController : Controller
    {
        private readonly AppDbContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IVilleRepository<Ville> _Ville;

        public VilleController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IVilleRepository<Ville> ville)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _roleManager = roleManager;
            _Ville = ville;
        }
        [Authorize(Roles = "User")]
        [Route("/Client/Ville")]
        public IActionResult Index()
        {
           var All= _Ville.All("f");
            return View(All);
        }
        [Route("Admin/Ville/")]
        [Authorize(Roles = "Admin,Super Admin")]
        public IActionResult All()
        {
            var All = _Ville.All("dd");
            return View(All);
        }
        [Authorize(Roles = "Super Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Super Admin")]
        public IActionResult Create(Ville model)
        {
            _Ville.Add(model);
            return RedirectToAction("All");
     
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Super Admin")]
        public IActionResult Edit(Ville model)
        {
            if (ModelState.IsValid)
            {
                Ville Villex = _Ville.Get(model.Id);


                if (Villex is null)
                {
                    return RedirectToAction("All");
                }
                else
                {
                    Villex.Name = model.Name;
                    Villex.Statut = model.Statut;


                    _Ville.Update(Villex);
                    return RedirectToAction("All");
                }



            }

            return RedirectToAction("All");


        }
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        [Route("Admin/Ville/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var Villex = _Ville.Get(id);
            if (Villex == null)
            {

                return RedirectToAction("All");
              
            }

            return View(Villex); 


        }



    }
}
