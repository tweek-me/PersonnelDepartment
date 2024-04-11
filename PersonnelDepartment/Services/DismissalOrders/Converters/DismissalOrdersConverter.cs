using PersonnelDepartment.Domain.DismissalOrders;
using PersonnelDepartment.Services.DismissalOrders.Repository.Models;

namespace PersonnelDepartment.Services.DismissalOrders.Converters;

public static class DismissalOrdersConverter
{
    public static DismissalOrder ToDismissalOrder(this DismissalOrderDb db)
    {
        return new(db.Id, db.EmployeeId, db.DismissDate, db.Reason);
    }

    public static DismissalOrder[] ToDismissalOrders(this DismissalOrderDb[] dbs)
    {
        return dbs.Select(ToDismissalOrder).ToArray();
    }
}
