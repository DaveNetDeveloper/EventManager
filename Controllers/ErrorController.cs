using System.Web.Mvc;

namespace EventManager.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult DefaultError()
        {
            return View();
        }
    }
}