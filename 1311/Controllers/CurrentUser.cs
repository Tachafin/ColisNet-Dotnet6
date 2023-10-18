using _1311.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _1311.Controllers
{
    public class CurrentUser 
    {
        private readonly UserManager<AppUser> userManager;

        public CurrentUser(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<string> GetUserIdAsync(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            return user?.Id;
        }
    }
}
