using Npgsql;
using PersonnelDepartment.Domain.Departments;
using PersonnelDepartment.Domain.Posts;
using PersonnelDepartment.Services.Departments.Converters;
using PersonnelDepartment.Services.Departments.Repository.Models;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Database;

namespace PersonnelDepartment.Services.Departments.Repository;

public class DepartmentsRepository : BaseRepository, IDepartmentsRepository
{
    private readonly IMainConnector _mainConnector;

    public DepartmentsRepository(IMainConnector mainConnector)
    {
        _mainConnector = mainConnector;
    }

    public void SaveDepartment(DepartmentBlank departmentBlank)
    {
        String expression = """
            INSERT INTO departments (id, name, phonenumber, createddatetimeutc, isremoved)
            VALUES (@p_id, @p_name, @p_phoneNumber, @p_currentDateTimeUtc, @p_isRemoved)
            ON CONFLICT (id) DO
            UPDATE SET
                name = @p_name,
                phonenumber = @p_phonenumber,
                modifieddatetimeutc = @p_currentDateTimeUtc,
                isremoved = @p_isRemoved
            """
        ;

        NpgsqlParameter[] parameters =
        {
            new("p_id", departmentBlank.Id),
            new("p_name", departmentBlank.Name),
            new("p_phoneNumber", departmentBlank.PhoneNumber),
            new("p_currentDateTimeUtc", DateTime.UtcNow),
            new("p_isRemoved", false)
        };

        _mainConnector.ExecuteNonQuery(expression, parameters);
    }

    public Department? GetDepartment(Guid id)
    {
        String expression = """
                            SELECT * FROM departments
                            WHERE id = @p_id AND isremoved = FALSE
                            """;

        NpgsqlParameter[] parameters =
        {
            new("p_id", id)
        };

        return _mainConnector.Get<DepartmentDb?>(expression, parameters)?.ToDepartment();
    }

    public Department[] GetDepartments()
    {
        String expression = """
                            SELECT * FROM departments
                            WHERE isremoved = FALSE
                            """;

        return _mainConnector.GetList<DepartmentDb>(expression).ToArray().ToDepartments();
    }

    public Page<Department> GetDepartments(Int32 page, Int32 pageSize)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);

        String expression = """
            SELECT COUNT(*) OVER() AS totalRows, d.* FROM (
                SELECT * FROM departments
                WHERE isremoved = FALSE
            ) as d
            OFFSET @p_offset
            LIMIT @p_limit
            """;

        NpgsqlParameter[] parameters =
        {
            new("p_offset", offset),
            new("p_limit", limit)
        };

        Page<DepartmentDb> departmentPage = _mainConnector.GetPage<DepartmentDb>(expression, parameters);
        return new Page<Department>(departmentPage.TotalRows, departmentPage.Values.ToDepartments());
    }

    public void RemoveDepartment(Guid id)
    {
        String expression = """
            UPDATE departments
            SET isremoved = TRUE
            WHERE id = @p_id
            """;

        NpgsqlParameter[] parameters =
        {
            new("p_id", id)
        };

        RemovePosts(id);
        _mainConnector.ExecuteNonQuery(expression, parameters);
    }

    public void SavePost(PostBlank postBlank)
    {
        String expression = """
            INSERT INTO posts (id, departmentid, name, salary, createddatetimeutc, isremoved)
            VALUES (@p_id, @p_departmentId, @p_name, @p_salary, @p_currentDateTimeUtc, @p_isRemoved)
            ON CONFLICT (id) DO
            UPDATE SET
                departmentid = @p_departmentId,
                name = @p_name,
                salary = @p_salary,
                modifieddatetimeutc = @p_currentDateTimeUtc,
                isremoved = @p_isRemoved
            """
        ;

        NpgsqlParameter[] parameters =
        {
            new("p_id", postBlank.Id),
            new("p_departmentid", postBlank.DepartmentId),
            new("p_name", postBlank.Name),
            new("p_salary", postBlank.Salary),
            new("p_currentDateTimeUtc", DateTime.UtcNow),
            new("p_isRemoved", false)
        };

        _mainConnector.ExecuteNonQuery(expression, parameters);
    }

    public Post? GetPost(Guid id)
    {
        String expression = """
                            SELECT * FROM posts
                            WHERE id = @p_id AND isremoved = FALSE
                            """;

        NpgsqlParameter[] parameters =
        {
            new("p_id", id)
        };

        return _mainConnector.Get<PostDb?>(expression, parameters)?.ToPost();
    }

    public Post[] GetPosts(Guid departmentId)
    {
        String expression = """
                            SELECT * FROM posts
                            WHERE departmentid = @p_departmentId AND isremoved = FALSE
                            """;

        NpgsqlParameter[] parameters =
        {
            new("p_departmentId", departmentId)
        };

        return _mainConnector.GetList<PostDb>(expression, parameters).ToArray().ToPosts();
    }

    public Post[] GetPosts(Guid[] departmentIds)
    {
        String expression = """
            SELECT * FROM posts
            WHERE 
                departmentid = ANY(@p_departmentIds)
                AND isremoved = FALSE
            """;

        NpgsqlParameter[] parameters =
        {
            new("p_departmentIds", departmentIds)
        };

        return _mainConnector.GetList<PostDb>(expression, parameters).ToArray().ToPosts();
    }

    public void RemovePost(Guid id)
    {
        String expression = """
            UPDATE posts
            SET isremoved = TRUE
            WHERE id = @p_id
            """;

        NpgsqlParameter[] parameters =
        {
            new("p_id", id)
        };

        _mainConnector.ExecuteNonQuery(expression, parameters);
    }

    private void RemovePosts(Guid departmentId)
    {
        String expression = """
            UPDATE posts
            SET isremoved = TRUE
            WHERE departmentid = @p_departmentId
            """;

        NpgsqlParameter[] parameters =
        {
            new("p_departmentId", departmentId)
        };

        _mainConnector.ExecuteNonQuery(expression, parameters);
    }
}
