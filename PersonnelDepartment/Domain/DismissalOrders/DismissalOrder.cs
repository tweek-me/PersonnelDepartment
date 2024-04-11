namespace PersonnelDepartment.Domain.DismissalOrders;

public class DismissalOrder
{
    public Guid Id { get; }
    public Guid EmployeeId { get; }
    public DateTime DismissDate { get; }
    public String Reason { get; }

    public DismissalOrder(Guid id, Guid employeeId, DateTime dismissDate, String reason)
    {
        Id = id;
        EmployeeId = employeeId;
        DismissDate = dismissDate;
        Reason = reason;
    }
}
