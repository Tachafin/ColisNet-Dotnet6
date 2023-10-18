using _1311.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace _1311.Models.Repository.IAccountRepositorys
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountRepository(AppDbContext context, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<bool> RegisterUserAsync(EditAccountViewModel model)
        {
         
                string fullname = model.FirstName + "_" + model.LastName;

                AppUser userx = new AppUser();

                userx.FirstName = model.FirstName;
                userx.LastName = model.LastName;
                userx.UserName = fullname;
                userx.Email = model.Email;
                userx.PhoneNumber = model.PhoneNumber;
                userx.Adresse = model.Adresse;
                var boutiquex = new Boutique();
                boutiquex.Name = model.BoutiqueName;
                boutiquex.RegistreCommerce = model.RegistreCommerce;
                boutiquex.VilleId = model.VilleName;
                userx.BoutiqueId = boutiquex.Id;
                userx.Boutique = boutiquex;
                var BankX = new Bank();
                BankX.Name = model.BankName;
                BankX.Rib = model.RIB;
                BankX.Boutique = boutiquex;
                boutiquex.BankeId = BankX.Id;


                this.context.Bank.Add(BankX);
                this.context.Boutique.Add(boutiquex);
                this.context.SaveChanges();

                var result = await userManager.CreateAsync(userx, model.Password);
                if (result.Succeeded)
                {
                    var res = await userManager.AddToRoleAsync(userx, "User");
                    return true;
                
                }
            return false;
            
        }

        public Task<EditAccountViewModel> UpdateUserAsync(AppUser model)
        {
            throw new System.NotImplementedException();
        }
    }
}
