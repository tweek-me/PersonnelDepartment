using Microsoft.AspNetCore.Mvc;
using PersonnelDepartment.Domain.Vacations;
using PersonnelDepartment.Infrastructure;
using PersonnelDepartment.Services.Vacations;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Controllers;
public class VacationsController : BaseController
{
    private readonly IVacationsService _vacationsService;

    public VacationsController(IVacationsService vacationsService)
    {
        _vacationsService = vacationsService;
    }

    [HttpGet("/vacations")]
    public IActionResult Index() => ReactApp();

    [HttpPost("/vacations/save")]
    public Result SaveVacation([FromBody] VacationBlank vacationBlank)
    {
        return _vacationsService.SaveVacation(vacationBlank);
    }

    [HttpPost("/vacations/getPage")]
    public Page<Vacation> GetVacationsPage(Int32 page, Int32 pageSize)
    {
        return _vacationsService.GetVacationsPage(page, pageSize);
    }

    [HttpPost("/vacations/remove")]
    public Result RemoveVacation([FromBody] Guid id)
    {
        return _vacationsService.RemoveVacation(id);
    }
}
