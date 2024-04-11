using Npgsql;
using PersonnelDepartment.Domain.SickLists;
using PersonnelDepartment.Services.SickLists.Converters;
using PersonnelDepartment.Services.SickLists.Repository.Models;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Database;

namespace PersonnelDepartment.Services.SickLists.Repository;

public class SickListsRepository : BaseRepository, ISickListsRepository
{
    private readonly IMainConnector _mainConnector;

    public SickListsRepository(IMainConnector mainConnector)
    {
        _mainConnector = mainConnector;
    }

    public void SaveSickList(SickListBlank sickListBlank)
    {
        String expression = """
            INSERT INTO sicklists (id, employeeid, begindate, enddate, createddatetimeutc, isremoved)
            VALUES (@p_id, @p_employeeId, @p_beginDate, @p_endDate, @p_currentDateTimeUtc, @p_isRemoved)
            ON CONFLICT (id) DO
            UPDATE SET
                employeeid = @p_employeeId,
                begindate = @p_beginDate,
                enddate = @p_endDate,
                modifieddatetimeutc = @p_currentDateTimeUtc,
                isremoved = @p_isRemoved
            """
        ;

        NpgsqlParameter[] parameters =
        {
            new("p_id", sickListBlank.Id),
            new("p_employeeId", sickListBlank.EmployeeId),
            new("p_beginDate", sickListBlank.BeginDate),
            new("p_endDate", sickListBlank.EndDate),
            new("p_currentDateTimeUtc", DateTime.UtcNow),
            new("p_isRemoved", false)
        };

        _mainConnector.ExecuteNonQuery(expression, parameters);
    }

    public SickList? GetSickList(Guid id)
    {
        String expression = """
                        SELECT * FROM sicklists
                        WHERE id = @p_id AND isremoved = FALSE
                        """;

        NpgsqlParameter[] parameters =
        {
            new("p_id", id)
        };

        return _mainConnector.Get<SickListDb?>(expression, parameters)?.ToSickList();
    }

    public Page<SickList> GetSickListsPage(Int32 page, Int32 pageSize)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);

        String expression = """
            SELECT COUNT(*) OVER() AS totalRows, s.* FROM (
                SELECT * FROM sicklists
                WHERE isremoved = FALSE
                OFFSET @p_offset
                LIMIT @p_limit
            ) AS s
            """;

        NpgsqlParameter[] parameters =
        {
            new("p_offset", offset),
            new("p_limit", limit)
        };

        Page<SickListDb> dbPage = _mainConnector.GetPage<SickListDb>(expression, parameters);
        return new Page<SickList>(dbPage.TotalRows, dbPage.Values.ToSickLists());
    }

    public void RemoveSickList(Guid id)
    {
        String expression = """
            UPDATE sicklists
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
