using Npgsql;
using PersonnelDepartment.Domain.Vacations;
using PersonnelDepartment.Services.Vacations.Converters;
using PersonnelDepartment.Services.Vacations.Repository.Models;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Database;

namespace PersonnelDepartment.Services.Vacations.Repository;

public class VacationsRepository : BaseRepository, IVacationsRepository
{
    private readonly IMainConnector _mainConnector;

    public VacationsRepository(IMainConnector mainConnector)
    {
        _mainConnector = mainConnector;
    }

    public void SaveVacation(VacationBlank vacationBlank)
    {
        String expression = """
            INSERT INTO vacations (id, employeeid, begindate, enddate, createddatetimeutc, isremoved)
            VALUES (@p_id, @p_employeeId, @p_beginDate, @p_endDate, @p_currentDateTimeUtc, @p_isRemoved)
            ON CONFLICT (id) DO
            UPDATE SET
                employeeid = @p_employeeid,
                begindate = @p_beginDate,
                enddate = @p_endDate,
                modifieddatetimeutc = @p_currentDateTimeUtc,
                isremoved = @p_isRemoved
            """
        ;

        NpgsqlParameter[] parameters =
        {
            new("p_id", vacationBlank.Id),
            new("p_employeeId", vacationBlank.EmployeeId),
            new("p_beginDate", vacationBlank.BeginDate),
            new("p_endDate", vacationBlank.EndDate),
            new("p_currentDateTimeUtc", DateTime.UtcNow),
            new("p_isRemoved", false)
        };

        _mainConnector.ExecuteNonQuery(expression, parameters);
    }

    public Vacation? GetVacation(Guid id)
    {
        String expression = """
                        SELECT * FROM vacations
                        WHERE id = @p_id AND isremoved = FALSE
                        """;

        NpgsqlParameter[] parameters =
        {
            new("p_id", id)
        };

        return _mainConnector.Get<VacationDb?>(expression, parameters)?.ToVacation();
    }

    public Page<Vacation> GetVacationsPage(Int32 page, Int32 pageSize)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);

        String expression = """
            SELECT COUNT(*) OVER() AS totalRows, v.* FROM (
                SELECT * FROM vacations
                WHERE isremoved = FALSE
                OFFSET @p_offset
                LIMIT @p_limit
            ) AS v
            """;

        NpgsqlParameter[] parameters =
        {
            new("p_offset", offset),
            new("p_limit", limit)
        };

        Page<VacationDb> dbPage = _mainConnector.GetPage<VacationDb>(expression, parameters);
        return new Page<Vacation>(dbPage.TotalRows, dbPage.Values.ToVacations());
    }

    public void RemoveVacation(Guid id)
    {
        String expression = """
            UPDATE vacations
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
