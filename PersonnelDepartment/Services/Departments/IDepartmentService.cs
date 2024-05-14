using PersonnelDepartment.Domain.Departments;
using PersonnelDepartment.Domain.Posts;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Services.Departments;

public interface IDepartmentService
{
    Result SaveDepartment(DepartmentBlank departmentBlank);
    Department? GetDepartment(Guid id);
    Department[] GetDepartments();
    Page<DepartmentStructure> GetDepartmentStructure(Int32 page, Int32 pageSize);
    Result RemoveDepartment(Guid id);

    Result SavePost(PostBlank postBlank);
    Post? GetPost(Guid id);
    Post[] GetPosts(Guid departmentId);
    Result RemovePost(Guid id);
}
