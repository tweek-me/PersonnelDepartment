using PersonnelDepartment.Domain.Employees;
using PersonnelDepartment.Services.Employees.Repository.Models;

namespace PersonnelDepartment.Services.Employees.Converters;

public static class EmployeeConverter
{
    public static Employee ToEmployee(this EmployeeDb db)
    {
        return new Employee(
            db.Id, db.PostId, db.Name, db.Surname, db.Partronymic, db.PhoneNumber,
            db.Email, db.Inn, db.Snils, db.PassportSeries, db.PassportNumber,
            db.BirthDay, db.IsDismissed
        );
    }

    public static Employee[] ToEmployees(this EmployeeDb[] dbs)
    {
        return dbs.Select(ToEmployee).ToArray();
    }
}
