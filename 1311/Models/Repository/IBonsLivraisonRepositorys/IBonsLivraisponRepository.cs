using System.Collections.Generic;

namespace _1311.Models.Repository.IBonsLivraisonRepositorys
{
    public interface IBonsLivraisponRepository<Tentity>
    {
            void Add(string userid);
            int Add2(string userid);
           IEnumerable<BonsLivraison> All(string userid);
        
            BonsLivraison Get(int id, string userid);
        void AddInBons(Colis coco, BonsLivraison Bonbon);
        void RemoveInbons(Colis coco, BonsLivraison Bonbon);

    }
}
