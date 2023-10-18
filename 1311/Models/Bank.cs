using System.ComponentModel.DataAnnotations.Schema;

namespace _1311.Models
{
    public class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Rib { get; set; }

        public Boutique Boutique { get; set; }

        //[ForeignKey("Boutique")]
        //public int BoutiqueId { get; set; }


    }
}
