using PersonnelDepartment.Domain.Departments;
using PersonnelDepartment.Domain.Posts;
using PersonnelDepartment.Tools;

namespace PersonnelDepartment.Services.Departments.Repository;

public interface IDepartmentsRepository
{
    void SaveDepartment(DepartmentBlank departmentBlank);
    Department? GetDepartment(Guid id);
    Department[] GetDepartments();
    Page<Department> GetDepartments(Int32 page, Int32 pageSize);
    void RemoveDepartment(Guid id);

    void SavePost(PostBlank postBlank);
    Post? GetPost(Guid id);
    Post[] GetPosts(Guid departmentId);
    Post[] GetPosts(Guid[] departmentIds);
    void RemovePost(Guid id);
}
