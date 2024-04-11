namespace PersonnelDepartment.Domain.DismissalOrders;

public class DismissalOrderBlank
{
    public Guid? Id { get; set; }
    public Guid? EmployeeId { get; set; }
    public DateTime? DismissDate { get; set; }
    public String? Reason { get; set; }
}
