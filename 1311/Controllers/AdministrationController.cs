using _1311.Models;
using _1311.Models.Repository.IVilleRepositorys;
using _1311.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> userManager;
        private readonly IVilleRepository<Ville> _ville;
        private readonly SignInManager<AppUser> signInManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IVilleRepository<Ville> ville)
        {
            _roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _ville = ville;
        }

        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }
        public async Task<IActionResult> AllUsers()
        {
            var allusers = userManager.Users
                .Include(c => c.Boutique)
                .Include(c => c.Boutique.Bank)
                .Include(c => c.Boutique.Ville)

                .ToList();
            List<AppUser> Model = new List<AppUser>();
            foreach (var userx in allusers)
            {
                if (await userManager.IsInRoleAsync(userx, "User") &&  ! await userManager.IsInRoleAsync(userx,"Admin") )
                {
                    Model.Add(userx);
                }
            }



            return View(Model);
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var userx = userManager.Users
                .Include(c => c.Boutique)
                .Include(c => c.Boutique.Bank)
                .Include(c => c.Boutique.Ville)
                .FirstOrDefault(c => c.Id == id);
            if (userx == null)
            {
                return RedirectToAction("AllUsers");
            }
            ViewBag.ListVille = _ville.All("on");
            return View(userx);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(AppUser model)
        {
            if (ModelState.IsValid)
            {
                var user = this.userManager.Users
                     .Include(c => c.Boutique)
                     .Include(c => c.Boutique.Bank)
                     .Include(c => c.Boutique.Ville)
                    .Where(c => c.Id == model.Id).FirstOrDefault(); 
                if (user is null)
                    return View("error", "test");
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;
                user.Boutique.RegistreCommerce = model.Boutique.RegistreCommerce;
                user.Boutique.VilleId = model.Boutique.VilleId;
                user.Adresse = model.Adresse;
                user.Boutique.Name = model.Boutique.Name;
                user.Boutique.Bank.Name = model.Boutique.Bank.Name;
                user.Boutique.Bank.Rib = model.Boutique.Bank.Rib;

                IdentityResult res = await this.userManager.UpdateAsync(user);
                if (res.Succeeded)
                {
                    return RedirectToAction("AllUsers");

                }
            }

            return View(model);
        }
        public async Task<IActionResult> AdminLogin(AccountLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, isPersistent: true, false);
                    if (result.Succeeded)
                    {


                        var userInRoleAdmin = await userManager.IsInRoleAsync(user, "Admin");
                        if (userInRoleAdmin)
                        {
                            return RedirectToAction("ColisEnAttente", "colis");
                        }
                        var userinroleBasic = await userManager.IsInRoleAsync(user, "Basic");
                        if (userinroleBasic)
                        {
                            ViewBag.Error = "Incorrect Login Admin";
                            return View(model);
                        }
                        else
                        {
                            ViewBag.Error = "Incorrect Login";
                            return View(model);
                        }

                    }
                }

                ViewBag.Error = "Incorrect Login";
                return View(model);


            }

            return View(model);
        }
        public IActionResult ListRoles()
        {
            var roles = this._roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleAdministrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = model.RoleName
                };
                IdentityResult result = await this._roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {

            if (id is null)
                return View("error", "test");
            IdentityRole role = await this._roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return View("error", "test");
            }
            EditRoleViewModel model = new EditRoleViewModel()
            {
                Id = id,
                RoleName = role.Name,
                Users = new List<string>()
            };

            foreach (AppUser userx in userManager.Users)
            {

                if (await userManager.IsInRoleAsync(userx, role.Name))
                {
                    model.Users.Add(userx.UserName);
                }
            }



            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel models)
        {
            if (ModelState.IsValid)
            {
                var role = await this._roleManager.FindByIdAsync(models.Id);
                if (role is null)
                    return View("error", "test");
                role.Name = models.RoleName;
                IdentityResult res = await this._roleManager.UpdateAsync(role);
                if (res.Succeeded)
                {
                    return RedirectToAction("listroles");

                }
            }

            return View(models);
        }
        [HttpGet]
        public async Task<IActionResult> EditUserRole(string id)
        {

            var role = await this._roleManager.FindByIdAsync(id);
            if (role is null)
                return View("error", "test");
            List<EditUserRoleViewModel> Models = new List<EditUserRoleViewModel>();
            foreach (AppUser item in userManager.Users)
            {
                EditUserRoleViewModel model = new EditUserRoleViewModel()
                {
                    UserId = item.Id,
                    UserName = item.UserName,
                    IsSelected = false
                };
                if (await userManager.IsInRoleAsync(item, role.Name))
                {
                    model.IsSelected = true;
                }

                Models.Add(model);
            }

            ViewBag.RoleId = id;

            return View(Models);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRole(List<EditUserRoleViewModel> model, string id)
        {

            var role = await this._roleManager.FindByIdAsync(id);
            if (role is null)
                return View("error", "test");

            for (int i = 0; i < model.Count; i++)
            {

                AppUser userx = await userManager.FindByIdAsync(model[i].UserId);
                if (await userManager.IsInRoleAsync(userx, role.Name) && !model[i].IsSelected)
                {
                    var result = await userManager.RemoveFromRoleAsync(userx, role.Name);
                }
                else if (!(await userManager.IsInRoleAsync(userx, role.Name)) && model[i].IsSelected)

                {
                    var result = await userManager.AddToRoleAsync(userx, role.Name);
                }


            }




            return RedirectToAction("EditRole", new { id = id });
        }


    }
}
