using PersonnelDepartment.Domain.Contracts;
using PersonnelDepartment.Domain.Employees;
using PersonnelDepartment.Services.Contracts.Repository;
using PersonnelDepartment.Services.Employees;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Services.Contracts;

public class ContractsService : IContractsService
{
    private readonly IContractsRepository _contractsRepository;
    private readonly IEmployeeService _employeeService;

    public ContractsService(IContractsRepository contractsRepository, IEmployeeService employeeService)
    {
        _contractsRepository = contractsRepository;
        _employeeService = employeeService;
    }

    public Result SaveContract(ContractBlank contractBlank)
    {
        PreprocessContractBlank(contractBlank);

        Result validateResult = ValidateContractBlank(contractBlank);
        if (!validateResult.IsSuccess) return validateResult;

        _contractsRepository.SaveContract(contractBlank);
        return Result.Success();
    }

    private void PreprocessContractBlank(ContractBlank contractBlank)
    {
        contractBlank.Id ??= Guid.NewGuid();
    }

    private Result ValidateContractBlank(ContractBlank contractBlank)
    {
        if (contractBlank.EmployeeId is not { } employeeId) return Result.Fail("Не указан сотрудник");

        Employee? employee = _employeeService.GetEmployee(employeeId);
        if (employee is null) return Result.Fail("Указанный сотрудник не найден");

        if (contractBlank.ReceiptDate is not { } receiptDate || receiptDate < DateTime.Now) return Result.Fail("Указана некорректная дата");

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
