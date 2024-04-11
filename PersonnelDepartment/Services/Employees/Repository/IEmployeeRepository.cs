using PersonnelDepartment.Domain.Employees;
using PersonnelDepartment.Tools;

namespace PersonnelDepartment.Services.Employees.Repository;

public interface IEmployeeRepository
{
    void SaveEmployee(EmployeeBlank employeeBlank);

    Employee? GetEmployee(Guid employeeId);
    Page<Employee> GetEmployeesPage(Int32 page, Int32 pageSize);

    void RemoveEmployee(Guid employeeId);
}
