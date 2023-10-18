using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Display(Name = "Nom du client")]
        [Required]
        public String NomComplet { get; set; }
        [Required]
        [Display(Name = "Telephone du client")]
        public string Telephone { get; set; }
        public Ville Ville { get; set; }

        [ForeignKey("Ville")]
        public int VilleId { get; set; }


        [Display(Name = "Adresse du client")]
        [Required]
        public string adresse { get; set; }
        public List<Colis> colis { get; set; }
    }
}
