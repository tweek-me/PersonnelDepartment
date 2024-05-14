using Npgsql;
using PersonnelDepartment.Domain.Contracts;
using PersonnelDepartment.Services.Contracts.Converters;
using PersonnelDepartment.Services.Contracts.Repository.Models;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Database;

namespace PersonnelDepartment.Services.Contracts.Repository;

public class ContractsRepository : BaseRepository, IContractsRepository
{
    private readonly IMainConnector _mainConnector;

    public ContractsRepository(IMainConnector manConnector)
    {
        _mainConnector = manConnector;
    }

    public void SaveContract(ContractBlank contractBlank)
    {
        String expression = """
            INSERT INTO contracts (id, employeeid, receiptdate, createddatetimeutc, isremoved)
            VALUES (@p_id, @p_employeeId, @p_receiptDate, @p_currentDateTimeUtc, @p_isRemoved)
            ON CONFLICT (id) DO
            UPDATE SET
                employeeid = @p_employeeid,
                receiptdate = @p_receiptDate,
                modifieddatetimeutc = @p_currentDateTimeUtc,
                isremoved = @p_isRemoved
            """;

        NpgsqlParameter[] parameters =
        {
            new("p_id", contractBlank.Id),
            new("p_employeeId", contractBlank.EmployeeId),
            new("p_receiptDate", contractBlank.ReceiptDate),
            new("p_currentDateTimeUtc", DateTime.UtcNow),
            new("p_isRemoved", false)
        };

        _mainConnector.ExecuteNonQuery(expression, parameters);
    }

    public Contract? GetContract(Guid id)
    {
        String expression = """
                            SELECT * FROM contracts
                            WHERE id = @p_id AND isremoved = FALSE
                            """;

        NpgsqlParameter[] parameters =
        {
            new("p_id", id)
        };

        return _mainConnector.Get<ContractDb?>(expression, parameters)?.ToContract();
    }

    public Page<Contract> GetContractsPage(int page, int pageSize)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);

        String experssion = """
            SELECT COUNT(*) OVER() AS totalRows, c.* FROM (
                SELECT * FROM contracts
                WHERE isremoved = FALSE
            ) AS c
            OFFSET @p_offset
            LIMIT @p_limit
            """;

        NpgsqlParameter[] parameters =
        {
            new("p_offset", offset),
            new("p_limit", limit)
        };

        Page<ContractDb> dbPage = _mainConnector.GetPage<ContractDb>(experssion, parameters);
        return new Page<Contract>(dbPage.TotalRows, dbPage.Values.ToContracts());
    }

    public void RemoveContract(Guid id)
    {
        String expression = """
            UPDATE contracts
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
