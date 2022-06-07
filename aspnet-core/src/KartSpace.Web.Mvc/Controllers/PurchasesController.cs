using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using KartSpace.Controllers;
using KartSpace.Merchandise;
using KartSpace.Purchases;
using KartSpace.Web.Models.Purchases;
using Microsoft.AspNetCore.Mvc;

namespace KartSpace.Web.Controllers
{
    [AbpMvcAuthorize()]
    public class PurchasesController : KartSpaceControllerBase
    {
        private readonly IPurchaseAppService _purchaseAppService;

        public PurchasesController(IPurchaseAppService purchaseAppService)
        {
            _purchaseAppService = purchaseAppService;
        }

        public async Task<IActionResult> Index()
        {
            return View(new PurchaseModalViewModel());
        }

        [HttpGet]
        public async Task<ActionResult> EditStareModal(int purchaseId)
        {
            var purchase = await _purchaseAppService.GetAsync(new EntityDto<int>(purchaseId));
            var model = new PurchaseModalViewModel
            {
                Purchase = purchase
            };

            return PartialView("_EditModal", model);
        }
    }
}
