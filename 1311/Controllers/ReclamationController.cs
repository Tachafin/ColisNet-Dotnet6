using _1311.Models;
using _1311.Models.Repository;
using _1311.Models.Repository.IReclamationRepositorys;
using _1311.Models.Repository.IUserRepositorys;
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
    public class ReclamationController : Controller
    {
        private readonly AppDbContext context;
        private readonly GlobalRepository<Reclamation> _Myrepository;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IReclamationRepository<Reclamation> _Reclamation;
        private readonly UserRepository _User;
        public ReclamationController(AppDbContext context, GlobalRepository<Reclamation> myrepository,

            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IReclamationRepository<Reclamation> reclamation, UserRepository user)
        {
            this.context = context;
            _Myrepository = myrepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _Reclamation = reclamation;
            _User = user;
        }
        public async Task<string> Returnuser()
        {
            var userx = await userManager.FindByNameAsync(User.Identity.Name);
            return userx.Id;
        }
        [Authorize(Roles = "User")]
        [Route("User/Reclamation")]
        public async Task<IActionResult> Index()
        {
            string Userid = await _User.GetUserIdAsync(User.Identity.Name);
            var Recx = _Reclamation.All(Userid);
            return View(Recx);
        }
        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("User/Reclamation/create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        [Route("User/Reclamation/create")]
        public async Task<IActionResult> Create(Reclamation reclamation)
        {
            string Userid = await _User.GetUserIdAsync(User.Identity.Name);
            _Reclamation.Add(Userid, reclamation);
            return RedirectToAction("index");

        }
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int id)
        {
            string Userid = await _User.GetUserIdAsync(User.Identity.Name);
            var recx = _Reclamation.Get(Userid, id);

            if (recx is null)
            {
                return RedirectToAction("Index");
            }
            return View(recx);

        }
        [Authorize(Roles = "Admin,Super Admin")]
        [Route("/Admin/Reclamation")]
        public async Task<ActionResult> allAsync(int? nb, string userid)
        {

            ViewBag.Listuser = await _User.GetUserAndAdminAsync();
            ViewBag.today = _Reclamation.GetAllData(1, userid).Count();
            ViewBag.todaygood = _Reclamation.GetAllData(2, userid).Count();
            ViewBag.todaybad = _Reclamation.GetAllData(3, userid).Count();
            ViewBag.all = _Reclamation.GetAllData(4, userid).Count();

            var allReclamation = _Reclamation.GetAllData(nb, userid);
            return View(allReclamation);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Super Admin")]
        public IActionResult Edit(int id)
        {
            var cherche = _Reclamation.GetForAdmin(id);
            if (cherche is null)
            {
                return RedirectToAction("all");
            }
            return View(cherche);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Super Admin")]
        public IActionResult Edit(Reclamation reclamation)
        {
            Reclamation recx = _Reclamation.GetForAdmin(reclamation.Id);
            if (recx is null)
            {
                return RedirectToAction("all");
            }
            recx.Reponse = reclamation.Reponse;
            if (reclamation.Reponse is null)
            {
                recx.Statut = false;
            }
            else
            {
                recx.Statut = true;
            }
            _Reclamation.Update(recx);
            return RedirectToAction("all");


        }

    }
}
