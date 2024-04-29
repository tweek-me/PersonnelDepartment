namespace PersonnelDepartment.Domain.Posts;

public class PostBlank
{
    public Guid? Id { get; set; }
    public Guid? DepartmentId { get; set; }
    public String? Name { get; set; }
    public Int32? Salary { get; set; }
}
