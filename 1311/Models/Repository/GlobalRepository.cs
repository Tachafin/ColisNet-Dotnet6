using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Models.Repository
{
   public interface GlobalRepository<Tentity>
    {
        void Add(string userid);
        Tentity getwithid(int id, string userid);
        void Add(Tentity entity, string userid);
        Tentity Update(Tentity entitychanges);
        IEnumerable<Tentity> GetEntitiesforuser(string userid, bool ab);
        IEnumerable<Tentity> GetEntitiesforuser(string userid, int ab);
        void addinbons(Colis coco,BonsLivraison Bonbon);
        void removeinbons(Colis coco, BonsLivraison Bonbon);
        Tentity Delete(int id, string userid);
    }
}
