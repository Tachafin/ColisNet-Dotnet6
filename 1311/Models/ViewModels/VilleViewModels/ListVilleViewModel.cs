namespace _1311.Models.ViewModels.VilleViewModels
{
    public class ListVilleViewModel
    {
        public string Name { get; set; }
        public bool Statut { get; set; }

        public int ColisEncours { get; set; }
        public int AllColis { get; set; }
        public int ColisLivre { get; set; }
        public int ColisEnvoye { get; set; }
        public int ColisRetourne { get; set; }
    }
}
