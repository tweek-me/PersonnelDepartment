using PersonnelDepartment.Domain.Departments;
using PersonnelDepartment.Domain.Posts;
using PersonnelDepartment.Services.Departments.Repository;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Services.Departments;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentsRepository _departmentsRepository;

    public DepartmentService(IDepartmentsRepository departmentsRepository)
    {
        _departmentsRepository = departmentsRepository;
    }

    public Result SaveDepartment(DepartmentBlank departmentBlank)
    {
        _departmentsRepository.SaveDepartment(departmentBlank);
        return Result.Success();
    }

    private Department? GetDepartment(Guid id)
    {
        return _departmentsRepository.GetDepartment(id);
    }

    //public Page<DepartmentStructure> GetDepartments(Int32 page, Int32 pageSize)
    //{
    //}

    public Result RemoveDepartment(Guid id)
    {
        Department? department = GetDepartment(id);
        if (department is null) return Result.Fail($"{nameof(department)} is null");

        _departmentsRepository.RemoveDepartment(id);
        return Result.Success();
    }

    public Result SavePost(PostBlank postBlank)
    {
        _departmentsRepository.SavePost(postBlank);
        return Result.Success();
    }

    private Post? GetPost(Guid id)
    {
        return _departmentsRepository.GetPost(id);
    }

    public Result RemovePost(Guid id)
    {
        Post? post = GetPost(id);
        if (post is null) return Result.Fail($"{nameof(post)} is null");

        _departmentsRepository.RemovePost(id);
        return Result.Success();
    }
}
