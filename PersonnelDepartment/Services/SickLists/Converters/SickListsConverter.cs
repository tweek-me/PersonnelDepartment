using PersonnelDepartment.Domain.SickLists;
using PersonnelDepartment.Services.SickLists.Repository.Models;

namespace PersonnelDepartment.Services.SickLists.Converters;

public static class SickListsConverter
{
    public static SickList ToSickList(this SickListDb db)
    {
        return new(db.Id, db.EmployeeId, db.BeginDate, db.EndDate);
    }

    public static SickList[] ToSickLists(this SickListDb[] dbs)
    {
        return dbs.Select(ToSickList).ToArray();
    }
}
