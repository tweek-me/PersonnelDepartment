namespace PersonnelDepartment.Services.Vacations.Repository.Models;

public class VacationDb
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime BeginDate { get; set; }
    public DateTime EndDate { get; set; }
}
