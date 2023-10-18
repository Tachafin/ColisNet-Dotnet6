using System.Collections.Generic;

namespace _1311.Models.Repository.IBoutiqueRepositorys
{
    public interface IBoutiqueRepositoryy<Tentity>
    {
        Tentity GetById(int id);
        IList<Boutique> GetAll();
    }
}
