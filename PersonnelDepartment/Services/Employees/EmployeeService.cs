using PersonnelDepartment.Domain.Departments;
using PersonnelDepartment.Domain.Employees;
using PersonnelDepartment.Domain.Posts;
using PersonnelDepartment.Services.Departments;
using PersonnelDepartment.Services.Employees.Repository;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;
using SupplierPO.Tools.Extensions;
using System.Text.RegularExpressions;

namespace PersonnelDepartment.Services.Employees;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentService _departmentService;

    public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentService departmentService)
    {
        _employeeRepository = employeeRepository;
        _departmentService = departmentService;
    }

    public Result SaveEmployee(EmployeeBlank employeeBlank)
    {
        Boolean isCreating = employeeBlank.Id is null;

        PreprocessEmployee(employeeBlank);

        Result validateResult = ValidateEmployee(employeeBlank);
        if (!validateResult.IsSuccess) return validateResult;

        if (!isCreating)
        {
            if (employeeBlank.Id is not { } employeeId) throw new Exception($"{nameof(employeeBlank.Id)} is null");

            Employee? existEmployee = GetEmployee(employeeId);
            if (existEmployee is null) throw new Exception("Редактирование несуществующего сотрудника");

            if (!existEmployee.IsDismissed && employeeBlank.IsDismissed)
            {
                //TASK ILYA сохранение приказа об увольнении
            }
        }

        _employeeRepository.SaveEmployee(employeeBlank);

        return Result.Success();
    }

    private void PreprocessEmployee(EmployeeBlank employeeBlank)
    {
        employeeBlank.Id ??= Guid.NewGuid();
        employeeBlank.PhoneNumber = employeeBlank.PhoneNumber.NormalizePhoneNumber();
        employeeBlank.BirthDay ??= DateTime.Now;
    }

    private Result ValidateEmployee(EmployeeBlank employeeBlank)
    {
        if (employeeBlank.DepartmentId is not { } departmentId) return Result.Fail("Не указан отдел, в котором работает сотрудник");

        Department? department = _departmentService.GetDepartment(departmentId);
        if (department is null) return Result.Fail("Указанный отдел не найден");

        if (employeeBlank.PostId is not { } postId) return Result.Fail("Не указана должность сотрудника");

        Post? post = _departmentService.GetPost(postId);
        if (post is null) return Result.Fail("Указанная должность не найдена");

        if (String.IsNullOrWhiteSpace(employeeBlank.Name)) return Result.Fail("Не указано имя");
        if (String.IsNullOrWhiteSpace(employeeBlank.Surname)) return Result.Fail("Не указана фамилия");
        if (String.IsNullOrWhiteSpace(employeeBlank.Partronymic)) return Result.Fail("Не указана отчество");
        if (String.IsNullOrWhiteSpace(employeeBlank.PhoneNumber)) return Result.Fail("Некорректный номер телефона");

        Result emailValidateResult = ValidateEmail(employeeBlank?.Email);
        if (!emailValidateResult.IsSuccess) return emailValidateResult;

        if (String.IsNullOrWhiteSpace(employeeBlank.Inn)) return Result.Fail("Не указан ИНН");
        if (String.IsNullOrWhiteSpace(employeeBlank.Snils)) return Result.Fail("Не указан снилс");

        if (employeeBlank.PassportSeries is null) return Result.Fail("Не указана серия паспорта");
        if (employeeBlank.PassportNumber is null) return Result.Fail("Не указан номер паспорта");

        if (employeeBlank.BirthDay is not { } birthDay || birthDay.Date > DateTime.Now.Date) return Result.Fail("Некорректная дата рождения");

        return Result.Success();
    }

    private Result ValidateEmail(String? email)
    {
        if (email is null) return Result.Fail("Некорретный email");

        String pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";

        return Regex.IsMatch(email, pattern) ? Result.Success() : Result.Fail("Некорретный email");
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
}
