using System.Collections.Generic;
using System.Threading.Tasks;

namespace _1311.Models.Repository.IUserRepositorys
{
    public interface IUserRepository<Tentity>
    {
        Task<List<AppUser>> GetUserAndAdminAsync();
    }
}
