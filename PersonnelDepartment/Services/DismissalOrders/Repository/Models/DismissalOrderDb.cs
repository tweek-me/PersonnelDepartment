namespace PersonnelDepartment.Services.DismissalOrders.Repository.Models;

public class DismissalOrderDb
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime DismissDate { get; set; }
    public String Reason { get; set; }
}
