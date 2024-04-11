using PersonnelDepartment.Domain.Employees;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Services.Employees;

public interface IEmployeeService
{
    Result SaveEmployee(EmployeeBlank employeeBlank);

    Employee? GetEmployee(Guid id);
    Page<Employee> GetEmployeesPage(Int32 page, Int32 pageSize);

    Result RemoveEmployee(Guid id);
}
