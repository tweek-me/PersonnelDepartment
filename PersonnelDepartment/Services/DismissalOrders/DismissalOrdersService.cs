using PersonnelDepartment.Domain.DismissalOrders;
using PersonnelDepartment.Domain.Employees;
using PersonnelDepartment.Services.DismissalOrders.Repository;
using PersonnelDepartment.Services.Employees;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Services.DismissalOrders;

public class DismissalOrdersService : IDismissalOrdersService
{
    private readonly IDismissalOrdersRepository _dismissalOrdersRepository;
    private readonly IEmployeeService _employeeService;

    public DismissalOrdersService(IDismissalOrdersRepository dismissalOrdersRepository, IEmployeeService employeeService)
    {
        _dismissalOrdersRepository = dismissalOrdersRepository;
        _employeeService = employeeService;
    }

    public Result SaveDissmisalOrder(DismissalOrderBlank dismissalOrderBlank)
    {
        PreprocessDissmisalOrderBlank(dismissalOrderBlank);

        Result validateResult = ValidateDissmisalOrderBlank(dismissalOrderBlank);
        if (!validateResult.IsSuccess) return validateResult;

        _dismissalOrdersRepository.SaveDissmisalOrder(dismissalOrderBlank);
        return Result.Success();
    }

    private void PreprocessDissmisalOrderBlank(DismissalOrderBlank dismissalOrderBlank)
    {
        dismissalOrderBlank.Id ??= Guid.NewGuid();
    }

    private Result ValidateDissmisalOrderBlank(DismissalOrderBlank dismissalOrderBlank)
    {
        if (dismissalOrderBlank.EmployeeId is not { } employeeId) return Result.Fail("Не указан сотрудник");

        Employee? employee = _employeeService.GetEmployee(employeeId);
        if (employee is null) return Result.Fail("Указанный сотрудник не найден");

        if (dismissalOrderBlank.DismissDate is not { } dismissDate || dismissDate < DateTime.Now) return Result.Fail("Указана некорректная дата");
        if (String.IsNullOrWhiteSpace(dismissalOrderBlank.Reason)) return Result.Fail("Не указан причина увольнения");

        return Result.Success();
    }

    private DismissalOrder? GetDismissalOrder(Guid id)
    {
        return _dismissalOrdersRepository.GetDismissalOrder(id);
    }

    public Page<DismissalOrder> GetDissmisalOrdersPage(Int32 page, Int32 pageSize)
    {
        return _dismissalOrdersRepository.GetDissmisalOrdersPage(page, pageSize);
    }

    public Result RemoveDissmisalOrder(Guid id)
    {
        DismissalOrder? dismissalOrder = GetDismissalOrder(id);
        if (dismissalOrder is null) return Result.Fail($"{nameof(dismissalOrder)} is null");

        _dismissalOrdersRepository.RemoveDissmisalOrder(id);
        return Result.Success();
    }
}
