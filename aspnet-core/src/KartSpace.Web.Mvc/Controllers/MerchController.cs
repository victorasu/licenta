using KartSpace.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using KartSpace.Merchandise;
using KartSpace.Web.Models.Merchandise;

namespace KartSpace.Web.Controllers;

public class MerchandiseController : KartSpaceControllerBase
{
    private readonly IMerchAppService _merchAppService;

    public MerchandiseController(
        IMerchAppService merchAppService)
    {
        _merchAppService = merchAppService;
    }

    public async Task<IActionResult> Index()
    {
        return View(new MerchModalViewModel());
    }

    public async Task<ActionResult> EditModal(int merchId)
    {
        var theProduct = await _merchAppService.GetAsync(new EntityDto<int>(merchId));
        var model = new MerchModalViewModel()
        {
            Merch = theProduct
        };

        return PartialView("_EditModal", model);
    }
}