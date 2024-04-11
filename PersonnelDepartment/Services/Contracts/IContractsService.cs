using PersonnelDepartment.Domain.Contracts;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Services.Contracts;

public interface IContractsService
{
    Result SaveContract(ContractBlank contractBlank);
    Page<Contract> GetContractsPage(Int32 page, Int32 pageSize);
    Result RemoveContract(Guid id);
}
