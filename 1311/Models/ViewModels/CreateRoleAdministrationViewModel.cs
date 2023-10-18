using System.ComponentModel.DataAnnotations;

namespace _1311.Models.ViewModels
{
    public class CreateRoleAdministrationViewModel
    {
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
    }
}
