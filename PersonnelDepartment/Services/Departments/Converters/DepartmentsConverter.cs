using PersonnelDepartment.Domain.Departments;
using PersonnelDepartment.Services.Departments.Repository.Models;

namespace PersonnelDepartment.Services.Departments.Converters;

public static class DepartmentsConverter
{
    public static Department ToDepartment(this DepartmentDb db)
    {
        return new Department(db.Id, db.Name, db.PhoneNumber);
    }

    public static Department[] ToDepartments(this DepartmentDb[] dbs)
    {
        return dbs.Select(ToDepartment).ToArray();
    }
}
