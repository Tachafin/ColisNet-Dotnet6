using System.Collections.Generic;

namespace _1311.Models.Repository.ICommentRepositorys
{
    public interface ICommentRepositorys<Tentity>
    {
        void Add(Tentity entity,string userid,int colisid);
        List<Tentity> Get(int id);
    }
}
