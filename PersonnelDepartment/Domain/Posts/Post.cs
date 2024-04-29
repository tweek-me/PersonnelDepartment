namespace PersonnelDepartment.Domain.Posts;

public class Post
{
    public Guid Id { get; }
    public Guid DepartmentId { get; }
    public String Name { get; }
    public Int32 Salary { get; }

    public Post(Guid id, Guid departmentId, String name, Int32 salary)
    {
        Id = id;
        DepartmentId = departmentId;
        Name = name;
        Salary = salary;
    }
}
