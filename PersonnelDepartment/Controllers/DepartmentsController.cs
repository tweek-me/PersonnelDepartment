using Microsoft.AspNetCore.Mvc;
using PersonnelDepartment.Domain.Departments;
using PersonnelDepartment.Infrastructure;
using PersonnelDepartment.Services.Departments;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Controllers;

public class DepartmentsController : BaseController
{
    private readonly IDepartmentService _departmentService;

    public DepartmentsController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet("/departments")]
    public IActionResult Index() => ReactApp();

    public record SaveDepartmentRequest(DepartmentBlank DepartmentBlank);

    [HttpPost("/departments/saveDepartment")]
    public Result SaveDepartment([FromBody] SaveDepartmentRequest request)
    {
        return _departmentService.SaveDepartment(request.DepartmentBlank);
    }

    [HttpGet("/departments/getDepartment")]
    public Department? GetDepartment(Guid departmentId)
    {
        return _departmentService.GetDepartment(departmentId);
    }

    [HttpGet("/departments/getDepartments")]
    public Department[] GetDepartments()
    {
        return _departmentService.GetDepartments();
    }

    [HttpGet("/departments/getDepartmentsPage")]
    public Page<DepartmentStructure> GetDepartmentsPage(Int32 page, Int32 pageSize)
    {
        return _departmentService.GetDepartmentStructure(page, pageSize);
    }

    public record RemoveDepartmentRequest(Guid DepartmentId);

    [HttpPost("/departments/removeDepartment")]
    public Result RemoveDepartment([FromBody] RemoveDepartmentRequest request)
    {
        return _departmentService.RemoveDepartment(request.DepartmentId);
    }
}
