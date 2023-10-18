using System.Collections.Generic;

namespace _1311.Models.Repository.IReclamationRepositorys
{
    public interface IReclamationRepository<Tentity>
    {
        void Add(string userid);
        void Add(string userid, Reclamation recx);
        IEnumerable<Reclamation> All(string userid);
        Reclamation Get(string userid, int id);
        Reclamation GetForAdmin(int id);
        IEnumerable<Reclamation> AdminAll();
        Reclamation Update(Reclamation rec);
       IEnumerable<Reclamation> GetAllData(int? nb,string userid);
    }
}
