using _1311.Models.ViewModels;
using _1311.Models.ViewModels.VilleViewModels;
using System.Collections.Generic;

namespace _1311.Models.Repository.IStatistiqueRepositorys
{
    public interface IstatistiqueRepository<Tentity>
    {
        int CountColisenattente(string userid);
       
        int CountColis(string userid,string etat);
        int CountBonsLivraison(string userid);
        int CountFacture (string userid);
        int CountDemande(string userid);
        int CountReclamation(string userid);
        List<int> GetList(string userid);
        List<int> GetListRetourne(string userid);

       int FacturePaye(string userid);
        int FactureImpaye(string userid);
        int Reclamationtraite(string userid);
        int ReclamationEncours(string userid);
        int totale(string userid);
        int commision(string userid);
        int net(string userid);
        List<LivreurViewModel> GetLivreurss();
        List<LivreurViewModel> GetLivreurs(string Period,string villeid);
        List<ListVilleViewModel> GetListeVille();
        int cost(string userid, int? villeid, string start, string end,string type);
        int costnonpayout(string userid, int? villeid, string start, string end, string type);

       
    }
}
