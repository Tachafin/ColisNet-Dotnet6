using System;

namespace _1311.Models.ViewModels.ColisViewModels
{
    public class GetColisAdmin
    {
        public int Id { get; set; }
        public string Nom_Colis { get; set; }
        public string Nom_de_client { get; set; }
        public string NumeroTelephone { get; set; }
        public string Ville { get; set; }
        public string Adresse { get; set; }
        public float Prix { get; set; }
        public string Etat { get; set; }
        public string Statut { get; set; }
        public DateTime DateCreation { get; set; }
        public string User { get; set; }
        public string Livreur { get; set; }
        public string LastComment { get; set; }
        
    }
}
