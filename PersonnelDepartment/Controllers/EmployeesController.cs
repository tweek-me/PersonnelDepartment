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


    public record SaveEmployeeRequest(EmployeeBlank EmployeeBlank);

    [HttpPost("/employees/save")]
    public Result SaveEmployee([FromBody] SaveEmployeeRequest request)
    {
        return _employeeService.SaveEmployee(request.EmployeeBlank);
    }

    [HttpGet("/employees/get")]
    public Employee? GetEmployee(Guid employeeId)
    {
        return _employeeService.GetEmployee(employeeId);
    }

    [HttpGet("/employees/getPage")]
    public Page<Employee> GetEmployeesPage(Int32 page, Int32 pageSize)
    {
        return _employeeService.GetEmployeesPage(page, pageSize);
    }

    public record RemoveEmployeeRequest(Guid EmployeeId);

    [HttpPost("/employees/remove")]
    public Result RemoveEmployee([FromBody] RemoveEmployeeRequest request)
    {
        return _employeeService.RemoveEmployee(request.EmployeeId);
    }
}
