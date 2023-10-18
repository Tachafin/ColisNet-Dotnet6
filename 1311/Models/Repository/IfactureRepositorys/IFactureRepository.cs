using _1311.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _1311.Models.Repository.IfactureRepositorys
{
    public interface IFactureRepository<Tentity>
    {
        List<Facture> All();
        List<Facture> All(string id);
        Facture Create(CreateFactureViewModel model);
        Facture Get(int id);
        void AddInFacture(int coco, int Bonbon);
        void RemoveInFacture(int coco, int Bonbon);
        Facture Update(Facture entitychanges);

        void Regler(Facture fct,Colis col);
        void ReglerMoins(Facture fct, Colis col);
        void UpdateImage (UploadRecuViewModel model);

    }
}
