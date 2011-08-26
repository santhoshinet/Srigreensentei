using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CCAvenueIntegrationDL;

namespace SriGreenShoppingCart.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["LatestNews"] = "";
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            List<LatestNews> latestNewses = (from c in scope.GetOqlQuery<LatestNews>().ExecuteEnumerable()
                                             select c).ToList();
            if (latestNewses.Count > 0)
            {
                if (latestNewses[0].News.Length > 50)
                    ViewData["LatestNews"] = latestNewses[0].News.Substring(0, 50) + " .....";
                else
                    ViewData["LatestNews"] = latestNewses[0].News;
            }
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult NewsandEvents()
        {
            ViewData["LatestNews"] = "";
            var scope = ObjectScopeProvider1.GetNewObjectScope();
            List<LatestNews> latestNewses = (from c in scope.GetOqlQuery<LatestNews>().ExecuteEnumerable()
                                             select c).ToList();
            if (latestNewses.Count > 0)
            {
                ViewData["LatestNews"] = latestNewses[0].NewsandEvents;
            }
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }
    }
}