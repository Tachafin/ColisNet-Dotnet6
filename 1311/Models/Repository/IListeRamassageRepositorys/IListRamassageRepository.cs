using System.Collections.Generic;

namespace _1311.Models.Repository
{
    public interface IListRamassageRepository<Tentity>
    {
        void add(Tentity tentity);
        List<Tentity> All(string userid);
        void RemoveInList(int coco, int Bonbon);
        void AddInList(int coco, int Bonbon);
        List<Colis> AllColisHaveNotList(int villeid);
        List<Colis> AllColisHaveList(int id);
        int CountColisEtat(int id,string etat);
        int CountEtat(string etat);
        Tentity Get(int id);

        IEnumerable<Tentity> Get(string etat,string userx,string start,string end);

      
        Tentity Update (Tentity entitychanges);
        float TotaleAmount(int id);
        void ChangeEtat(int idcolis,string etat);
        IEnumerable<Tentity> Top5Liste(string livreurid);

    }
}
