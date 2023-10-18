using System.Collections.Generic;

namespace _1311.Models.ViewModels.ColisViewModels
{
    public class ColisAdminViewModel
    {
        public IEnumerable<GetColisAdmin> XColis { get; set; }
        public IEnumerable<Ville> AllVille { get; set; }
        public int AllColis { get; set; }
        public int AllColisEncours { get; set; }
        public int AllColisEnvoye { get; set; }
        public int AllColisLivre { get; set; }
        public int AllColisRetourne { get; set; }
        public IEnumerable<Livreur> AllLivreur { get; set; }
        public IEnumerable<AppUser> Listuser { get; set; }
    }
}
