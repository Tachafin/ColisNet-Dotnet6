using System.Collections.Generic;

namespace _1311.Models.Repository.IVilleRepositorys
{
    public interface IVilleRepository<Tentity>
    {
        List<Tentity> All(string onorof);
        List<Tentity> All();
        Ville Get(int id);
        void Add(Ville entity);
        Ville Update(Ville entity);
    }
}
