using Microsoft.AspNetCore.Http;
using System.Web;
namespace _1311.Models.ViewModels
{
    public class UploadRecuViewModel
    {
        public int  Id { get; set; }
        public IFormFile Photo { get; set; }
    }
}
