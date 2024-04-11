using PersonnelDepartment.Domain.DismissalOrders;
using PersonnelDepartment.Services.DismissalOrders.Repository;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Services.DismissalOrders;

public class DismissalOrdersService : IDismissalOrdersService
{
    private readonly IDismissalOrdersRepository _dismissalOrdersRepository;

    public DismissalOrdersService(IDismissalOrdersRepository dismissalOrdersRepository)
    {
        _dismissalOrdersRepository = dismissalOrdersRepository;
    }

    public Result SaveDissmisalOrder(DismissalOrderBlank dismissalOrderBlank)
    {
        _dismissalOrdersRepository.SaveDissmisalOrder(dismissalOrderBlank);
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
