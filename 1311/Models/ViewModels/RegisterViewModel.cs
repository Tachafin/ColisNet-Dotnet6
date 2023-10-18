using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
        public String Password { get; set; }
        public int idboutique { get; set; }
        public string Nomboutique { get; set; }
        public string Registercommerce { get; set; }
        public String Numerotelephone { get; set; }

        public int ville { get; set; }
    }
}
