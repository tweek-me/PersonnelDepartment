using Npgsql;
using PersonnelDepartment.Domain.Employees;
using PersonnelDepartment.Services.Employees.Converters;
using PersonnelDepartment.Services.Employees.Repository.Models;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Database;

namespace PersonnelDepartment.Services.Employees.Repository;

public class EmployeeRepository : BaseRepository, IEmployeeRepository
{
    private readonly IMainConnector _mainConnector;

    public EmployeeRepository(IMainConnector mainConnector)
    {
        _mainConnector = mainConnector;
    }

    public void SaveEmployee(EmployeeBlank employeeBlank)
    {
        String expression = """
            INSERT INTO employees
            (
                id, departmentid, postid, name, surname, patronymic, phonenumber,
                email, inn, snils, passportseries, passportnumber, 
                birthday, isdismissed, createddatetimeutc, isremoved
            )
            VALUES (
               @p_id, @p_departmentId, @p_postId, @p_name, @p_surname, @p_patronymic, 
               @p_phoneNumber, @p_email, @p_inn, @p_snils, 
               @p_passportSeries, @p_passportNumber,
               @p_birthDay, @p_isDismissed, @p_currentDateTimeUtc, 
               @p_isRemoved
               
            )
            ON CONFLICT (id) DO
            UPDATE SET
                departmentid = @p_departmentId,
                postid = @p_postId,
                name = @p_name,
                surname = @p_surname,
                patronymic = @p_patronymic,
                phonenumber = @p_phoneNumber,
                email = @p_email,
                inn = @p_inn,
                snils = @p_snils,
                passportSeries = @p_passportSeries,
                passportNumber = @p_passportNumber,
                birthday= @p_birthDay,
                isdismissed = @p_isDismissed,
                modifieddatetimeutc =  @p_currentDateTimeUtc
            """;

        NpgsqlParameter[] parameters =
        {
            new("p_id", employeeBlank.Id),
            new("p_departmentId", employeeBlank.DepartmentId),
            new("p_postId", employeeBlank.PostId),
            new("p_name", employeeBlank.Name),
            new("p_surname", employeeBlank.Surname),
            new("p_patronymic", employeeBlank.Partronymic),
            new("p_phoneNumber", employeeBlank.PhoneNumber),
            new("p_email", employeeBlank.Email),
            new("p_inn", employeeBlank.Inn),
            new("p_snils", employeeBlank.Snils),
            new("p_passportSeries", employeeBlank.PassportSeries),
            new("p_passportNumber", employeeBlank.PassportNumber),
            new("p_birthDay", employeeBlank.BirthDay),
            new("p_isDismissed", employeeBlank.IsDismissed),
            new("p_currentDateTimeUtc", DateTime.UtcNow),
            new("p_isRemoved", false)
        };

        _mainConnector.ExecuteNonQuery(expression, parameters);
    }

    public Employee? GetEmployee(Guid employeeId)
    {
        String expression = """
                        SELECT * FROM employees
                        WHERE id = @p_employeeId AND isremoved = FALSE
                        """;

        NpgsqlParameter[] parameters =
        {
            new("p_employeeId", employeeId)
        };

        return _mainConnector.Get<EmployeeDb?>(expression, parameters)?.ToEmployee();
    }

    //TASK ILYA поменять Limit offset везде, где тянутся page
    public Page<Employee> GetEmployeesPage(Int32 page, Int32 pageSize)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);

        String expression = """
            SELECT COUNT(*) OVER() AS totalRows, emp.* FROM (
                SELECT * FROM employees
                WHERE isremoved = FALSE
                
            ) AS emp
            OFFSET @p_offset
            LIMIT @p_limit
            """;

        NpgsqlParameter[] parameters =
        {
            new("p_offset", offset),
            new("p_limit", limit)
        };

        Page<EmployeeDb> dbPage = _mainConnector.GetPage<EmployeeDb>(expression, parameters);
        return new Page<Employee>(dbPage.TotalRows, dbPage.Values.ToEmployees());
    }

    public void RemoveEmployee(Guid employeeId)
    {
        String expression = """
            UPDATE employees
            SET isremoved = TRUE
            WHERE id = @p_employeeId
            """;

        NpgsqlParameter[] parameters =
        {
            new("p_employeeId", employeeId)
        };

        _mainConnector.ExecuteNonQuery(expression, parameters);
    }
}
