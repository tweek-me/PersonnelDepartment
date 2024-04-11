using Microsoft.AspNetCore.Mvc;
using PersonnelDepartment.Domain.DismissalOrders;
using PersonnelDepartment.Infrastructure;
using PersonnelDepartment.Services.DismissalOrders;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Controllers;
public class DissmisalOrdersController : BaseController
{
    private readonly IDismissalOrdersService _dismissalOrdersService;

    public DissmisalOrdersController(IDismissalOrdersService dismissalOrdersService)
    {
        _dismissalOrdersService = dismissalOrdersService;
    }

    [HttpGet("/dismissalOrders")]
    public IActionResult Index() => ReactApp();


    [HttpPost("/dismissalOrders/save")]
    public Result SaveDissmisalOrder([FromBody] DismissalOrderBlank dismissalOrderBlank)
    {
        return _dismissalOrdersService.SaveDissmisalOrder(dismissalOrderBlank);
    }

    [HttpGet("/dismissalOrders/getPage")]
    public Page<DismissalOrder> GetDissmisalOrdersPage(Int32 page, Int32 pageSize)
    {
        return _dismissalOrdersService.GetDissmisalOrdersPage(page, pageSize);
    }

    [HttpPost("/dismissalOrders/remove")]
    public Result RemoveDissmisalOrder([FromBody] Guid id)
    {
        return _dismissalOrdersService.RemoveDissmisalOrder(id);
    }
}
