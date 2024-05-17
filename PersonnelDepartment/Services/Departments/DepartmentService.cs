using PersonnelDepartment.Domain.Departments;
using PersonnelDepartment.Domain.Posts;
using PersonnelDepartment.Services.Departments.Repository;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;
using SupplierPO.Tools.Extensions;

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
        PreprocessDepartmentBlank(departmentBlank);
        Result validateResult = ValidateDepartmentBlank(departmentBlank);
        if (!validateResult.IsSuccess) return validateResult;

        _departmentsRepository.SaveDepartment(departmentBlank);
        return Result.Success();
    }

    public Department? GetDepartment(Guid id)
    {
        return _departmentsRepository.GetDepartment(id);
    }

    public Department[] GetDepartments()
    {
        return _departmentsRepository.GetDepartments();
    }

    private void PreprocessDepartmentBlank(DepartmentBlank departmentBlank)
    {
        departmentBlank.Id ??= Guid.NewGuid();
        departmentBlank.PhoneNumber ??= departmentBlank.PhoneNumber.NormalizePhoneNumber();
    }

    private Result ValidateDepartmentBlank(DepartmentBlank departmentBlank)
    {
        if (String.IsNullOrWhiteSpace(departmentBlank.Name)) return Result.Fail("Некорректное название отдела");
        if (departmentBlank.PhoneNumber is null) return Result.Fail("Указан некорректный номер телефона");

        return Result.Success();
    }

    public Page<DepartmentStructure> GetDepartmentStructure(Int32 page, Int32 pageSize)
    {
        Page<Department> departmentsPage = _departmentsRepository.GetDepartments(page, pageSize);
        Department[] departments = departmentsPage.Values;

        Guid[] departmentIds = departments.Select(d => d.Id).ToArray();
        Post[] posts = _departmentsRepository.GetPosts(departmentIds);

        DepartmentStructure[] departmentStructures = departments.Select(department =>
        {
            Post[] departmentPosts = posts.Where(p => p.DepartmentId == department.Id).ToArray();
            return new DepartmentStructure(department, departmentPosts);
        }).ToArray();

        return new Page<DepartmentStructure>(departmentsPage.TotalRows, departmentStructures);
    }

    public Result RemoveDepartment(Guid id)
    {
        Department? department = GetDepartment(id);
        if (department is null) return Result.Fail($"{nameof(department)} is null");

        _departmentsRepository.RemoveDepartment(id);
        return Result.Success();
    }

    public Result SavePost(PostBlank postBlank)
    {
        PreprocessPostBlank(postBlank);

        Result validateResult = ValidatePostBlank(postBlank);
        if (!validateResult.IsSuccess) return validateResult;

        _departmentsRepository.SavePost(postBlank);
        return Result.Success();
    }

    private void PreprocessPostBlank(PostBlank postBlank)
    {
        postBlank.Id ??= Guid.NewGuid();
    }

    private Result ValidatePostBlank(PostBlank postBlank)
    {
        if (postBlank.DepartmentId is not { } departmentId) return Result.Fail("Не указан отдел");

        Department? department = _departmentsRepository.GetDepartment(departmentId);
        if (department is null) return Result.Fail("Указанный отдел не найден");

        if (String.IsNullOrWhiteSpace(postBlank.Name)) return Result.Fail("Указано некорректное название должности");
        if (postBlank.Salary is not { } salary || salary <= 0) return Result.Fail("Указан некорректный размер заработной платы");

        return Result.Success();
    }

    public Post? GetPost(Guid id)
    {
        return _departmentsRepository.GetPost(id);
    }

    public Post[] GetPosts(Guid departmentId)
    {
        return _departmentsRepository.GetPosts(departmentId);
    }

    public Result RemovePost(Guid id)
    {
        Post? post = GetPost(id);
        if (post is null) return Result.Fail($"{nameof(post)} is null");

        _departmentsRepository.RemovePost(id);
        return Result.Success();
    }
}
