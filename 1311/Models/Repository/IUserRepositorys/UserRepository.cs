using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _1311.Models.Repository.IUserRepositorys
{
    public class UserRepository 

    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        public UserRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
           this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<List<AppUser>> GetUserAndAdminAsync()
        {
            List<AppUser> model = new List<AppUser>();

            foreach (AppUser userx in userManager.Users)
            {

                if (await userManager.IsInRoleAsync(userx, "User") && !await userManager.IsInRoleAsync(userx, "Admin"))
                {
                    model.Add(userx);
                }
            }
            return model;
        }
        public async Task<string> GetUserIdAsync(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            return user?.Id;
        }

    }
}
