namespace PersonnelDepartment.Domain.SickLists;

public class SickListBlank
{
    public Guid? Id { get; set; }
    public Guid? EmployeeId { get; set; }
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
}
