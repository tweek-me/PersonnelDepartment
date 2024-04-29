using PersonnelDepartment.Domain.Employees;
using PersonnelDepartment.Domain.SickLists;
using PersonnelDepartment.Services.Employees;
using PersonnelDepartment.Services.SickLists.Repository;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Services.SickLists;

public class SickListsService : ISickListsService
{
    private readonly ISickListsRepository _sickListsRepository;
    private readonly IEmployeeService _employeeService;

    public SickListsService(ISickListsRepository sickListsRepository, IEmployeeService employeeService)
    {
        _sickListsRepository = sickListsRepository;
        _employeeService = employeeService;
    }

    public Result SaveSickList(SickListBlank sickListBlank)
    {
        PreprocessSickList(sickListBlank);

        Result validateResult = ValidateSickList(sickListBlank);
        if (!validateResult.IsSuccess) return validateResult;

        _sickListsRepository.SaveSickList(sickListBlank);
        return Result.Success();
    }

    private void PreprocessSickList(SickListBlank sickListBlank)
    {
        sickListBlank.Id ??= Guid.NewGuid();
    }

    private Result ValidateSickList(SickListBlank sickListBlank)
    {
        if (sickListBlank.EmployeeId is not { } employeeId) return Result.Fail("Не указан сотрудник");

        Employee? employee = _employeeService.GetEmployee(employeeId);
        if (employee is null) return Result.Fail("Указанный сотрудник не найден");

        if (sickListBlank.BeginDate is not { } beginDate) return Result.Fail("Некорретная дата начала больничного");
        if (sickListBlank.EndDate is not { } endDate) return Result.Fail("Некорретная дата конца больничного");

        if (endDate > beginDate) return Result.Fail("Некорректный период больничного");

        return Result.Success();
    }

    private SickList? GetSickList(Guid id)
    {
        return _sickListsRepository.GetSickList(id);
    }

    public Page<SickList> GetSickListsPage(Int32 page, Int32 pageSize)
    {
        return _sickListsRepository.GetSickListsPage(page, pageSize);
    }

    public Result RemoveSickList(Guid id)
    {
        SickList? sickList = GetSickList(id);
        if (sickList is null) throw new Exception($"{nameof(sickList)} is null");

        _sickListsRepository.RemoveSickList(id);
        return Result.Success();
    }
}
