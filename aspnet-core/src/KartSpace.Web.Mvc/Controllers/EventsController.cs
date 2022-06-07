using KartSpace.Controllers;
using KartSpace.Events;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using KartSpace.Web.Models.Events;

namespace KartSpace.Web.Controllers;

[AbpMvcAuthorize()]
public class EventsController : KartSpaceControllerBase
{
    private readonly IEventAppService _eventAppService;

    public EventsController(
        IEventAppService eventAppService)
    {
        _eventAppService = eventAppService;
    }

    public async Task<IActionResult> Index()
    {
        return View(new EventModalViewModel());
    }

    public async Task<ActionResult> EditModal(int eventId)
    {
        var theEvent = await _eventAppService.GetAsync(new EntityDto<int>(eventId));
        var model = new EventModalViewModel()
        {
            Event = theEvent
        };

        return PartialView("_EditModal", model);
    }
}