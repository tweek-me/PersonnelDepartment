using PersonnelDepartment.Domain.Contracts;
using PersonnelDepartment.Services.Contracts.Repository.Models;

namespace PersonnelDepartment.Services.Contracts.Converters;

public static class ContractsConverter
{
    public static Contract ToContract(this ContractDb db)
    {
        return new Contract(db.Id, db.EmployeeId, db.ReceiptDate);
    }

    public static Contract[] ToContracts(this ContractDb[] dbs)
    {
        return dbs.Select(ToContract).ToArray();
    }
}
