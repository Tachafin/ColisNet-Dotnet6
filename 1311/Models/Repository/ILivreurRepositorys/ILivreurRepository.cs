using _1311.Models.ViewModels;
using System.Collections.Generic;

namespace _1311.Models
{
    public interface ILivreurRepository<Tentity>
    {
        void Add(RegisterLivreurViewModel model,string userx );
        Tentity Get(int id);
        Tentity Update(Tentity entitychanges);
        IEnumerable<Tentity> All();
    }
}
