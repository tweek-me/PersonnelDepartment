using PersonnelDepartment.Domain.Posts;

namespace PersonnelDepartment.Domain.Departments;

public class DepartmentStructure
{
    public Department Department { get; }
    public Post[] Posts { get; }

    public DepartmentStructure(Department department, Post[] posts)
    {
        Department = department;
        Posts = posts;
    }
}
