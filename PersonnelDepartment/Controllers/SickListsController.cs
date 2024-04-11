using Microsoft.AspNetCore.Mvc;
using PersonnelDepartment.Domain.SickLists;
using PersonnelDepartment.Infrastructure;
using PersonnelDepartment.Services.SickLists;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Controllers;

public class SickListsController : BaseController
{
    private readonly ISickListsService _sickListsService;

    public SickListsController(ISickListsService sickListsService)
    {
        _sickListsService = sickListsService;
    }

    [HttpGet("/sickLists")]
    public IActionResult Index() => ReactApp();

    [HttpPost("sickLists/save")]
    public Result SaveSickList([FromBody] SickListBlank sickListBlank)
    {
        return _sickListsService.SaveSickList(sickListBlank);
    }

    [HttpPost("sickLists/getPage")]
    public Page<SickList> GetSickListsPage(Int32 page, Int32 pageSize)
    {
        return _sickListsService.GetSickListsPage(page, pageSize);
    }

    [HttpPost("sickLists/remove")]
    public Result RemoveSickList([FromBody] Guid id)
    {
        return _sickListsService.RemoveSickList(id);
    }
}
