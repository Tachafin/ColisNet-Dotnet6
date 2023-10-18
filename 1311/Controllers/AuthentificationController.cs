using _1311.Models;
using _1311.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _1311.Controllers
{
    public class AuthentificationController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        
        public AuthentificationController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> LogoutAsync()
        {
            string CurrentUser = User.Identity.Name;
            var userx = await userManager.FindByNameAsync(CurrentUser);
           
            var userInRoleAdmin = await userManager.IsInRoleAsync(userx, "Admin");
            if (userInRoleAdmin)
            {
                await signInManager.SignOutAsync();
                return RedirectToAction("Admin", "Authentification");
            }
            var userInRoleUser = await userManager.IsInRoleAsync(userx, "User");
            if (userInRoleUser)
            {
                await signInManager.SignOutAsync(); 
                return RedirectToAction("Client", "Authentification");
            }
            await signInManager.SignOutAsync(); 
            return RedirectToAction("Client", "Authentification");
        }

    
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Admin()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("edit", "account");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Admin(AccountLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if(user != null)
                {

                
                var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, isPersistent: true, false);



                if (result.Succeeded)
                {

                        var userInRoleLivreur = await userManager.IsInRoleAsync(user, "Livreur");
                        if (userInRoleLivreur)
                        {
                            return RedirectToAction("Dashboard", "Livreur");
                        }
                        var userInRoleAdmin = await userManager.IsInRoleAsync(user, "Admin");
                    if (userInRoleAdmin)
                    {
                        return RedirectToAction("allcolis", "colis");
                    }
                    var userinroleBasic = await userManager.IsInRoleAsync(user, "User");
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
            }

            return View(model);
        }
   
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Client()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("edit","account");
            }
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Client(AccountLoginViewModel model)
        {


            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        var userInRoleAdmin = await userManager.IsInRoleAsync(user, "Admin");
                        if (userInRoleAdmin)
                        {
                            await signInManager.SignOutAsync();
                            return RedirectToAction("Admin", "Authentification");
                        }
                        var userinroleBasic = await userManager.IsInRoleAsync(user, "User"); //user means Client 
                        if (userinroleBasic)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            await signInManager.SignOutAsync();
                        }
                    }
                    else
                    {
                        ViewBag.error = "Incorrect Login";
                    }

                }


            }
            ViewBag.error = "Login Invalid";
            return View(model);
        }




    }
}
