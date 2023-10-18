using System.Collections.Generic;
using System.Linq;

namespace _1311.Models.Repository.IBoutiqueRepositorys
{
    public class BoutiqueRepository : IBoutiqueRepositoryy<Boutique>
    {
        private readonly AppDbContext context;

        public BoutiqueRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IList<Boutique> GetAll()
        {
            var btq = this.context.Boutique.ToList();
            return btq;
        }

        public Boutique GetById(int id)
        {
           Boutique btq=this.context.Boutique.FirstOrDefault(c=>c.Id==id);
            return btq;
        }
    }
}
