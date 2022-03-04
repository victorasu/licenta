using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using KartSpace.Controllers;

namespace KartSpace.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : KartSpaceControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
