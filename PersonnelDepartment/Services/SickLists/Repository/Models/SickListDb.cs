namespace PersonnelDepartment.Services.SickLists.Repository.Models;

public class SickListDb
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
}
