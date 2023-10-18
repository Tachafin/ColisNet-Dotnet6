using _1311.Models.ViewModels.DemandeViewModels;
using System;
using System.Collections.Generic;

namespace _1311.Models.Repository.IDemandeRepositorys
{
    public interface IDemandeRepository<Tentity>
    {
        List<Demande>  All(string user,int? etat);
        string Add(string user,CreateDemandeViewModel model);
        List<Demande> AdminAll(int? etat);
       
        int Count(string x);
        int Count(string x,string userid);
        Demande Get(int id);
        List<Demande> GetAllWithString(string x);
        Demande update(Demande demande);
    }
}
