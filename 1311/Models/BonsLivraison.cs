using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Models
{
    public class BonsLivraison
    {
        public int id { get; set; }
        public String Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date_creation { get; set; }
        public AppUser User { get; set; }

        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public IList<Colis> Colis { get; set; }
    }
}
