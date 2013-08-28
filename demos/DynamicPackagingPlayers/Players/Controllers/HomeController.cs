namespace Players.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View("FlashSSPlayer");
        }

        public ActionResult SilverlightSSPlayer()
        {
            return this.View();
        }

        public ActionResult Html5HLSPlayer()
        {
            return this.View();
        }
    }
}
