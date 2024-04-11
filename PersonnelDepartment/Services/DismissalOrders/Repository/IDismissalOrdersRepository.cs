using PersonnelDepartment.Domain.DismissalOrders;
using PersonnelDepartment.Tools;

namespace PersonnelDepartment.Services.DismissalOrders.Repository;

public interface IDismissalOrdersRepository
{
    void SaveDissmisalOrder(DismissalOrderBlank dismissalOrderBlank);
    DismissalOrder? GetDismissalOrder(Guid id);
    Page<DismissalOrder> GetDissmisalOrdersPage(Int32 page, Int32 pageSize);
    void RemoveDissmisalOrder(Guid id);
}
