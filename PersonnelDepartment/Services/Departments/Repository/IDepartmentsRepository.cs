using PersonnelDepartment.Domain.Departments;
using PersonnelDepartment.Domain.Posts;

namespace PersonnelDepartment.Services.Departments.Repository;

public interface IDepartmentsRepository
{
    void SaveDepartment(DepartmentBlank departmentBlank);
    Department? GetDepartment(Guid id);
    //Page<Department> GetDepartments(Int32 page, Int32 pageSize);
    void RemoveDepartment(Guid id);

    void SavePost(PostBlank postBlank);
    Post? GetPost(Guid id);
    void RemovePost(Guid id);
}
