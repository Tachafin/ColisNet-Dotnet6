using System.ComponentModel.DataAnnotations;

namespace _1311.Models.ViewModels.DemandeViewModels
{
    public class CreateDemandeViewModel
    {
        [Required]
        [Display(Name = "Numero de Telephone")]
        public string Numero_Telephone { get; set; }
        [Required]
        public string Adresse { get; set; }
        public string Note { get; set; }
    }
}
