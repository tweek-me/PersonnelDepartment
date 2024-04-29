using PersonnelDepartment.Domain.Employees;
using PersonnelDepartment.Domain.Vacations;
using PersonnelDepartment.Services.Employees;
using PersonnelDepartment.Services.Vacations.Repository;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Services.Vacations;

public class VacationsService : IVacationsService
{
    private readonly IVacationsRepository _vacationsRepository;
    private readonly IEmployeeService _employeeService;

    public VacationsService(IVacationsRepository vacationsRepository, IEmployeeService employeeService)
    {
        _vacationsRepository = vacationsRepository;
        _employeeService = employeeService;
    }

    public Result SaveVacation(VacationBlank vacationBlank)
    {
        PreprocessVacation(vacationBlank);

        Result validateResult = ValidateVacationBlank(vacationBlank);
        if (!validateResult.IsSuccess) return validateResult;

        _vacationsRepository.SaveVacation(vacationBlank);
        return Result.Success();
    }

    private void PreprocessVacation(VacationBlank vacationBlank)
    {
        vacationBlank.Id ??= Guid.NewGuid();
    }

    private Result ValidateVacationBlank(VacationBlank vacationBlank)
    {
        if (vacationBlank.EmployeeId is not { } employeeId) return Result.Fail("Не указан сотрудник");

        Employee? employee = _employeeService.GetEmployee(employeeId);
        if (employee is null) return Result.Fail("Указанный сотрудник не найден");

        if (vacationBlank.BeginDate is not { } beginDate) return Result.Fail("Некорретная дата начала больничного");
        if (vacationBlank.EndDate is not { } endDate) return Result.Fail("Некорретная дата конца больничного");

        if (endDate > beginDate) return Result.Fail("Некорректный период больничного");

        return Result.Success();
    }

    private Vacation? GetVacation(Guid id)
    {
        return _vacationsRepository.GetVacation(id);
    }

    public Page<Vacation> GetVacationsPage(Int32 page, Int32 pageSize)
    {
        return _vacationsRepository.GetVacationsPage(page, pageSize);
    }

    public Result RemoveVacation(Guid id)
    {
        Vacation? vacation = GetVacation(id);
        if (vacation is null) return Result.Fail("Выбранного отпуска не существует");

        _vacationsRepository.RemoveVacation(id);
        return Result.Success();
    }
}
