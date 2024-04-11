using PersonnelDepartment.Domain.Departments;
using PersonnelDepartment.Domain.Posts;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Services.Departments;

public interface IDepartmentService
{
    Result SaveDepartment(DepartmentBlank departmentBlank);
    //Page<DepartmentStructure> GetDepartments(Int32 page, Int32 pageSize);
    Result RemoveDepartment(Guid id);

    Result SavePost(PostBlank postBlank);
    Result RemovePost(Guid id);
}
