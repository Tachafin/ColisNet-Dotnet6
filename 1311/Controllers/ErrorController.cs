using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace _1311.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this._logger = logger;
        }

        [Route("/Error/{StatusCode}")]
        public IActionResult Index(int StatutsCode)
        {
            string message = string.Empty;
            var ExceptionStatuts = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            //switch (StatutsCode)
            //{
            //    case 404:
            //        {
            message = "Desolé , la page demandé n'est pas trouvé 404 ";

                        this._logger.LogWarning($"404 error iap { ExceptionStatuts.OriginalPath}"  +$"     and query string =  " +
                             $"{ExceptionStatuts.OriginalQueryString}");
                    //}
                    //break;

            //}



            return View("NotFound", message);
        }
        [Route("Error")]
        public IActionResult Error()
        {
            var ExceptionStatuts = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.Message = ExceptionStatuts.Error.Message;
            ViewBag.StackTrace = ExceptionStatuts.Error.StackTrace;
            ViewBag.Path = ExceptionStatuts.Path;
            return View();
        }
    }
}
