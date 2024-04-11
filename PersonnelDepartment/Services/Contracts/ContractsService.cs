using PersonnelDepartment.Domain.Contracts;
using PersonnelDepartment.Services.Contracts.Repository;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Services.Contracts;

public class ContractsService : IContractsService
{
    private readonly IContractsRepository _contractsRepository;

    public ContractsService(IContractsRepository contractsRepository)
    {
        _contractsRepository = contractsRepository;
    }

    public Result SaveContract(ContractBlank contractBlank)
    {
        _contractsRepository.SaveContract(contractBlank);
        return Result.Success();
    }

    private Contract? GetContract(Guid id)
    {
        return _contractsRepository.GetContract(id);
    }

    public Page<Contract> GetContractsPage(Int32 page, Int32 pageSize)
    {
        return _contractsRepository.GetContractsPage(page, pageSize);
    }

    public Result RemoveContract(Guid id)
    {
        Contract? contract = GetContract(id);
        if (contract is null) return Result.Fail($"{nameof(contract)} is null");

        _contractsRepository.RemoveContract(id);
        return Result.Success();
    }
}
