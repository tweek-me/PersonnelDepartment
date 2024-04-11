using Microsoft.AspNetCore.Mvc;
using PersonnelDepartment.Domain.Contracts;
using PersonnelDepartment.Infrastructure;
using PersonnelDepartment.Services.Contracts;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Controllers;
public class ContractsController : BaseController
{
    private readonly IContractsService _contractsService;

    public ContractsController(IContractsService contractsService)
    {
        _contractsService = contractsService;
    }

    [HttpGet("/contracts")]
    public IActionResult Index() => ReactApp();

    [HttpPost("/contracts/save")]
    public Result SaveContract([FromBody] ContractBlank contractBlank)
    {
        return _contractsService.SaveContract(contractBlank);
    }

    [HttpGet("/contracts/getPage")]
    public Page<Contract> GetContractsPage(Int32 page, Int32 pageSize)
    {
        return _contractsService.GetContractsPage(page, pageSize);
    }

    [HttpPost("/contracts/remove")]
    public Result RemoveContract([FromBody] Guid id)
    {
        return _contractsService.RemoveContract(id);
    }
}
