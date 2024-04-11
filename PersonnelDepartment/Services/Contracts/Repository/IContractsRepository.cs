using PersonnelDepartment.Domain.Contracts;
using PersonnelDepartment.Tools;

namespace PersonnelDepartment.Services.Contracts.Repository;

public interface IContractsRepository
{
    void SaveContract(ContractBlank contractBlank);
    Contract? GetContract(Guid id);
    Page<Contract> GetContractsPage(Int32 page, Int32 pageSize);
    void RemoveContract(Guid id);
}
