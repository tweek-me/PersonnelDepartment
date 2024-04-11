using Npgsql;
using PersonnelDepartment.Domain.DismissalOrders;
using PersonnelDepartment.Services.DismissalOrders.Converters;
using PersonnelDepartment.Services.DismissalOrders.Repository.Models;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Database;

namespace PersonnelDepartment.Services.DismissalOrders.Repository;

public class DismissalOrdersRepository : BaseRepository, IDismissalOrdersRepository
{
    private readonly IMainConnector _mainConnector;

    public DismissalOrdersRepository(IMainConnector mainConnector)
    {
        _mainConnector = mainConnector;
    }

    public void SaveDissmisalOrder(DismissalOrderBlank dismissalOrderBlank)
    {
        String expression = """
            INSERT INTO dismissalorders (id, employeeid, dismissdate, reason, createddatetimeutc, isremoved)
            VALUES (@p_id, @p_employeeId, @p_dismissDate, @p_reason, @p_currentDateTimeUtc, @p_isRemoved)
            ON CONFLICT (id) DO
            UPDATE SET
                employeeid = @p_employeeId,
                dismissdate = @p_dismissDate,
                reason = @p_reason,
                modifieddatetimeutc = @p_currentDateTimeUtc,
                isremoved = @p_isRemoved
            """
        ;

        NpgsqlParameter[] parameters =
        {
            new("p_id", dismissalOrderBlank.Id),
            new("p_employeeId", dismissalOrderBlank.EmployeeId),
            new("p_dismissDate", dismissalOrderBlank.DismissDate),
            new("p_reason", dismissalOrderBlank.Reason),
            new("p_currentDateTimeUtc", DateTime.UtcNow),
            new("p_isRemoved", false)
        };

        _mainConnector.ExecuteNonQuery(expression, parameters);
    }

    public DismissalOrder? GetDismissalOrder(Guid id)
    {
        String expression = """
                            SELECT * FROM dismissalorders
                            WHERE id = @p_id AND isremoved = FALSE
                            """;

        NpgsqlParameter[] parameters =
        {
            new("p_id", id)
        };

        return _mainConnector.Get<DismissalOrderDb?>(expression, parameters)?.ToDismissalOrder();
    }

    public Page<DismissalOrder> GetDissmisalOrdersPage(Int32 page, Int32 pageSize)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);

        String experssion = """
            SELECT COUNT(*) OVER() AS totalRows, d.* FROM (
                SELECT * FROM dismissalorders
                WHERE isremoved = FALSE
                OFFSET @p_offset
                LIMIT @p_limit
            ) AS d
            """;

        NpgsqlParameter[] parameters =
        {
            new("p_offset", offset),
            new("p_limit", limit)
        };

        Page<DismissalOrderDb> dbPage = _mainConnector.GetPage<DismissalOrderDb>(experssion, parameters);
        return new Page<DismissalOrder>(dbPage.TotalRows, dbPage.Values.ToDismissalOrders());
    }

    public void RemoveDissmisalOrder(Guid id)
    {
        String expression = """
            UPDATE dismissalorders
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
