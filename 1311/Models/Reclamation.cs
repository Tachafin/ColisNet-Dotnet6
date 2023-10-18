using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Models
{
    public class Reclamation
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public DateTime DateCreation { get; set; }
        public bool Statut { get; set; }
        public String Sujet { get; set; }
        public String Message { get; set; }
        public String Reponse { get; set; }

        public AppUser User { get; set; }

        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }

    }
}
