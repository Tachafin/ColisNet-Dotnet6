using _1311.Models.ViewModels.ColisViewModels;
using System;
using System.Collections.Generic;

namespace _1311.Models.Repository.IColisRepositorys
{
    public interface IColisRepository<Tentity>
    {
        void Remove(Colis entity);
        void Add(Colis entity, string userid);
        void AddForAdmin(Colis entity, string userid);
        IEnumerable<Colis> ColisForLivreur(string LivreurId,string Userid);
        IEnumerable<Colis> ColisEnAttente(string userid);
        IEnumerable<Colis> ColisEnvoye(string userid);
        IEnumerable<Colis> ColisWithBonsId(string userid,int idBons);
        IEnumerable<Colis> ColisWithoutBonsiId(string userid);
        Colis Get(int id, string userid);
        Colis Update(Colis entitychanges);
        int CountEtat(string Etat);

        int CountEtatForLivreur(string Etat, string LivreurId, string etatList,string Etatdeliste);
        List<Colis> ListColisFacture(int id);
        List<Colis> ListColisNonFacture();
        List<Colis> ListColisNonFacture(string user);
        Colis getwithid(int id);
       IEnumerable<Colis> Filtre(string userid,int? villeid,string start,string end);
        IEnumerable<Colis> Top5(string livreurid);
        bool AddCodeBareForColis();
       
        IEnumerable<GetColisAdmin> GetEntitiesforuser();

        Tentity getwithidAdmin(int id);
        Tentity GetColisWithScan(string BarCode);
        IEnumerable<Tentity> DetailsColis(String nom,int? villeid);
        IEnumerable<Tentity> DetailsColisforclientAsync(String client, int? villeid, string livreurid);
    }
}
