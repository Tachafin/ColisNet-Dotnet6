using _1311.Models;
using _1311.Models.Repository;
using _1311.Models.Repository.IAccountRepositorys;
using _1311.Models.Repository.IColisRepositorys;
using _1311.Models.Repository.IUserRepositorys;
using _1311.Models.Repository.IVilleRepositorys;
using _1311.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Controllers
{
    public class AccountController : Controller
    {
        private readonly IColisRepository<Colis> _Myrepository;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly AppDbContext context;
        private readonly IVilleRepository<Ville> _ville;
        private readonly IAccountRepository _accountRepository;
        private readonly UserRepository _User;


        public AccountController(IColisRepository<Colis> myrepository,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, AppDbContext context,
            IVilleRepository<Ville> ville, IAccountRepository accountRepository, UserRepository user)
        {
            _Myrepository = myrepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            _ville = ville;
            _accountRepository = accountRepository;
            _User = user;
        }
        public async Task<string> Returnuser()
        {
            string CurrentUser = User.Identity.Name;
            var userx = await userManager.FindByNameAsync(CurrentUser);

            return userx.Id;
        }
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.ListVille = _ville.All("on");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> Register(EditAccountViewModel model)
        {
            ViewBag.ListVille = _ville.All("on");
            var registrationResult = false;

            if (ModelState.IsValid) { 
                 registrationResult = await _accountRepository.RegisterUserAsync(model);
            }
            if (registrationResult)
            {
                return RedirectToAction("ColisEnAttente", "colis");
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            string Userid = await _User.GetUserIdAsync(User.Identity.Name);
            string CurrentUser = User.Identity.Name;
            var userx = await userManager.FindByNameAsync(CurrentUser);
            var userfinal = userManager.Users
            .Include(c => c.Boutique)
            .Include(c => c.Boutique.Ville)
            .Include(c => c.Boutique.Bank)
            .Where(c => c.UserName == CurrentUser)
            .FirstOrDefault();


            EditAccountViewModel model = new EditAccountViewModel();

            model.FirstName = userfinal.FirstName;
            model.LastName = userfinal.LastName;
            model.Email = userfinal.Email;
            model.Password = userfinal.PasswordHash;
            model.PhoneNumber = userfinal.PhoneNumber;
            model.Adresse = userfinal.Adresse;
            if (User.IsInRole("User"))
            {
                model.BoutiqueName = userfinal.Boutique.Name;
                model.BankName = userfinal.Boutique.Bank.Name;
                model.RIB = userfinal.Boutique.Bank.Rib;
                model.RegistreCommerce = userfinal.Boutique.RegistreCommerce;
                model.VilleName = userfinal.Boutique.Ville.Id;
            }
            //if (!User.IsInRole("Livreur") || !User.IsInRole("Admin") || !User.IsInRole("Super Admin"))
            //{
            //    model.BoutiqueName = userfinal.Boutique.Name;
            //    model.BankName = userfinal.Boutique.Bank.Name;
            //    model.RIB = userfinal.Boutique.Bank.Rib;
            //    model.RegistreCommerce = userfinal.Boutique.RegistreCommerce;
            //    model.VilleName = userfinal.Boutique.Ville.Id;
            //}

            return View(model);


        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAccountViewModel model)
        {

            if (ModelState.IsValid)
            {
                string CurrentUser = User.Identity.Name;
                var userx = await userManager.FindByNameAsync(CurrentUser);

                if (userx != null)
                {
                    var PasswordHasher = userManager.PasswordHasher.HashPassword(userx, model.Password);

                    userx.PasswordHash = PasswordHasher;
                    IdentityResult res = await userManager.UpdateAsync(userx);
                    if (res.Succeeded)
                    {
                        return RedirectToAction("edit");
                    }
                }
            }

            return View();



        }



    }
}
