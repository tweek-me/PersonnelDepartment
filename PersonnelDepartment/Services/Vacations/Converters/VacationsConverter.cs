using PersonnelDepartment.Domain.Vacations;
using PersonnelDepartment.Services.Vacations.Repository.Models;

namespace PersonnelDepartment.Services.Vacations.Converters;

public static class VacationsConverter
{
    public static Vacation ToVacation(this VacationDb db)
    {
        return new(db.Id, db.EmployeeId, db.BeginDate, db.EndDate);
    }

    public static Vacation[] ToVacations(this VacationDb[] dbs)
    {
        return dbs.Select(ToVacation).ToArray();
    }
}
