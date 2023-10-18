using System.ComponentModel.DataAnnotations.Schema;

namespace _1311.Models
{
    public class Boutique
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RegistreCommerce { get; set; }
        public AppUser User { get; set; }

        //[ForeignKey("AppUser")]
        //public string UserId { get; set; }

        public Ville Ville { get; set; }

        [ForeignKey("Ville")]
        public int? VilleId { get; set; }


        public Bank Bank { get; set; }
        [ForeignKey("Bank")]
        public int? BankeId { get; set; }
    }
}
