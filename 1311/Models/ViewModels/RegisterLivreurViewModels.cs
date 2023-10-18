using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace _1311.Models.ViewModels
{
    public class RegisterLivreurViewModel 
    {
        [Required]
        
        public string Nom  { get; set; }
        [Required]
        public string Prenom  { get; set; }
        [Required]
        [Display(Name ="Numero de telephone")]
        public string Numero_Telephone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Ville")]
        public int VilleId { get; set; }

        public string Adresse { get; set; }
  
    }
}
