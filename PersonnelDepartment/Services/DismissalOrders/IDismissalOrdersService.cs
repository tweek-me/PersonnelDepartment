using PersonnelDepartment.Domain.DismissalOrders;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Services.DismissalOrders;

public interface IDismissalOrdersService
{
    Result SaveDissmisalOrder(DismissalOrderBlank dismissalOrderBlank);
    Page<DismissalOrder> GetDissmisalOrdersPage(Int32 page, Int32 pageSize);
    Result RemoveDissmisalOrder(Guid id);
}
