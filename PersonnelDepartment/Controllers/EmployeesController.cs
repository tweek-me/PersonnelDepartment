using Microsoft.AspNetCore.Mvc;
using PersonnelDepartment.Domain.Employees;
using PersonnelDepartment.Infrastructure;
using PersonnelDepartment.Services.Employees;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Controllers;
public class EmployeesController : BaseController
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("/employees")]
    public IActionResult Index() => ReactApp();


    [HttpPost("/employees/save")]
    public Result SaveEmployee([FromBody] EmployeeBlank employeeBlank)
    {
        return _employeeService.SaveEmployee(employeeBlank);
    }

    [HttpGet("/employees/get")]
    public Employee? GetEmployee(Guid id)
    {
        return _employeeService.GetEmployee(id);
    }

    [HttpGet("/employees/getPage")]
    public Page<Employee> GetEmployee(Int32 page, Int32 pageSize)
    {
        return _employeeService.GetEmployeesPage(page, pageSize);
    }

    [HttpPost("/employees/remove")]
    public Result SaveEmployee([FromBody] Guid employeeId)
    {
        return _employeeService.RemoveEmployee(employeeId);
    }
}
