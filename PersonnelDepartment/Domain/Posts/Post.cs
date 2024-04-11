namespace PersonnelDepartment.Domain.Posts;

public class Post
{
    public Guid Id { get; }
    public Guid Departmentid { get; }
    public String Name { get; }
    public Int32 Salary { get; }

    public Post(Guid id, Guid departmentid, String name, Int32 salary)
    {
        Id = id;
        Departmentid = departmentid;
        Name = name;
        Salary = salary;
    }
}
