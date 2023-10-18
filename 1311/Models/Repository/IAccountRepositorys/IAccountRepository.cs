using _1311.Models.ViewModels;
using System.Threading.Tasks;

namespace _1311.Models.Repository.IAccountRepositorys
{
    public interface IAccountRepository
    {
        Task<bool> RegisterUserAsync(EditAccountViewModel model);
        Task<EditAccountViewModel> UpdateUserAsync(AppUser model);



    }
}
