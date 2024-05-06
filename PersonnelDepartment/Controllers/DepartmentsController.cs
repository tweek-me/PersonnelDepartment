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

    [HttpPost("/departments/saveDepartment")]
    public Result SaveDepartment([FromBody] DepartmentBlank departmentBlank)
    {
        return _departmentService.SaveDepartment(departmentBlank);
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

    [HttpPost("/departments/removeDepartment")]
    public Result RemoveDepartment([FromBody] Guid id)
    {
        return _departmentService.RemoveDepartment(id);
    }
}
