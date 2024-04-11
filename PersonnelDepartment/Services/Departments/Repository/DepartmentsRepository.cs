﻿using Npgsql;
using PersonnelDepartment.Domain.Departments;
using PersonnelDepartment.Domain.Posts;
using PersonnelDepartment.Services.Departments.Converters;
using PersonnelDepartment.Services.Departments.Repository.Models;
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

    //public Page<Department> GetDepartments(Int32 page, Int32 pageSize)
    //{
    //    throw new NotImplementedException();
    //}

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
            new("p_departmentid", postBlank.Departmentid),
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
}