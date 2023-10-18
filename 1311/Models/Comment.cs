using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1311.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreation { get; set; }
        public String Sujet { get; set; }
        public String Message { get; set; }

        public AppUser User { get; set; }

        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public Colis Colis { get; set; }
        [ForeignKey("Colis")]
        public int ColisId { get; set; }


    }
}
