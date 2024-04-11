using PersonnelDepartment.Domain.Employees;
using PersonnelDepartment.Services.Employees.Repository;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Services.Employees;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    #region Empoyees

    public Result SaveEmployee(EmployeeBlank employeeBlank)
    {
        //Result validateResult = ValidateEmployee(employeeBlank);
        //if (!validateResult.IsSuccess) return validateResult;

        _employeeRepository.SaveEmployee(employeeBlank);

        return Result.Success();
    }

    public Employee? GetEmployee(Guid employeeId)
    {
        return _employeeRepository.GetEmployee(employeeId);
    }

    public Page<Employee> GetEmployeesPage(Int32 page, Int32 pageSize)
    {
        return _employeeRepository.GetEmployeesPage(page, pageSize);
    }

    public Result RemoveEmployee(Guid employeeId)
    {
        Employee? employee = GetEmployee(employeeId);
        if (employee is null) return Result.Fail($"{nameof(employee)} is null");

        _employeeRepository.RemoveEmployee(employeeId);
        return Result.Success();
    }

    //private Result ValidateEmployee(EmployeeBlank employeeBlank)
    //{
    //    throw new NotImplementedException();
    //}

    #endregion Empoyees
}
