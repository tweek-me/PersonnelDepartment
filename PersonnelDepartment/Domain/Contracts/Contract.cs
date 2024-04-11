namespace PersonnelDepartment.Domain.Contracts;

public class Contract
{
    public Guid Id { get; }
    public Guid EmployeeId { get; }
    public DateTime ReceiptDate { get; }

    public Contract(Guid id, Guid employeeId, DateTime receiptDate)
    {
        Id = id;
        EmployeeId = employeeId;
        ReceiptDate = receiptDate;
    }
}
