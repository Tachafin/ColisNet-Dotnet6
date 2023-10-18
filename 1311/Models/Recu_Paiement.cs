namespace _1311.Models
{
    public class Recu_Paiement
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Chemin { get; set; }
        public Facture? Facture { get; set; }
        
    }
}
