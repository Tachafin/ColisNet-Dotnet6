using _1311.Models;
using _1311.Models.Repository.IColisRepositorys;
using _1311.Models.Repository.ICommentRepositorys;
using _1311.Models.Repository.IUserRepositorys;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _1311.Controllers
{
    public class CommentController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IColisRepository<Colis> _Colis;
        private readonly ICommentRepositorys<Comment> _Comment;
        private readonly UserRepository _User;

        public CommentController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ICommentRepositorys<Comment> comment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _Comment = comment;
        }
        public async Task<string> Returnuser()
        {
            string CurrentUser = User.Identity.Name;
            var userx = await userManager.FindByNameAsync(CurrentUser);

            return userx.Id;
        }
        public IActionResult Index()
        {
            return View();
        }
 
        [HttpGet]
        public IActionResult CreateComment(int id)
        {
            ViewBag.Colisid = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommentAsync(Comment comment,int colisid)
        {
            string Userid = await _User.GetUserIdAsync(User.Identity.Name);
            _Comment.Add(comment, Userid, comment.ColisId);

            return View();
        }
    [HttpGet]
        public IActionResult ShowComment(int id)
        {
            ViewBag.idcolis = id;
            List<Comment> ListComment = _Comment.Get(id);
            return View(ListComment);
        }

    }
}
