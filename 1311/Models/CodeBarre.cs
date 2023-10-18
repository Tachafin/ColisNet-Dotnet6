namespace _1311.Models
{
    public class CodeBarre
    {
        public int id { get; set; }
        public string Nom { get; set; }
        public string Chemin { get; set; }
        public string base64String { get; set; }
        public Colis? Colis { get; set; }
    }
}
