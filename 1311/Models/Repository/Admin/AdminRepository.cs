using System;
using System.Collections.Generic;

namespace _1311.Models.Repository.Admin
{
    public interface AdminRepository<Tentity>
    {
        IEnumerable<Tentity> GetEntitiesforuser();
        Tentity getwithid(int id);
        IEnumerable<Tentity> DetailsColis (String nom);
        IEnumerable<Tentity> DetailsColisforclientAsync(String client);
    }
}
