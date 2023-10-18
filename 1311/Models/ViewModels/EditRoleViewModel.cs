using System.Collections.Generic;

namespace _1311.Models.ViewModels
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }
        public string  RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}
