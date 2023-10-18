using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _1311.Models.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public String Password { get; set; }
    }
}
