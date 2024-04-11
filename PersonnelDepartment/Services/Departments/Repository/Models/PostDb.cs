namespace PersonnelDepartment.Services.Departments.Repository.Models;

public class PostDb
{
    public Guid Id { get; set; }
    public Guid Departmentid { get; set; }
    public String Name { get; set; }
    public Int32 Salary { get; set; }
}
