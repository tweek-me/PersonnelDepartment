namespace PersonnelDepartment.Services.Contracts.Repository.Models;

public class ContractDb
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime ReceiptDate { get; set; }
}
