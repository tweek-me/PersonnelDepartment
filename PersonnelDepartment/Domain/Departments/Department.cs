namespace PersonnelDepartment.Domain.Departments;

public class Department
{
    public Guid Id { get; }
    public String Name { get; }
    public String PhoneNumber { get; }

    public Department(Guid id, String name, String phoneNumber)
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
    }
}
