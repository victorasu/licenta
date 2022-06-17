using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using KartSpace.Controllers;

namespace KartSpace.Web.Controllers
{
    public class HomeController : KartSpaceControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
