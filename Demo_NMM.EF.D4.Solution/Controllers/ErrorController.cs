using System.Web.Mvc;

namespace Demo_NMM.EF.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return RedirectToAction("AccessDenied");
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}