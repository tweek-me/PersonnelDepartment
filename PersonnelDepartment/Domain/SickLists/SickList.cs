namespace PersonnelDepartment.Domain.SickLists;

public class SickList
{
    public Guid Id { get; }
    public Guid EmployeeId { get; }
    public DateTime BeginDate { get; }
    public DateTime EndDate { get; }

    public SickList(Guid id, Guid employeeId, DateTime beginDate, DateTime endDate)
    {
        Id = id;
        EmployeeId = employeeId;
        BeginDate = beginDate;
        EndDate = endDate;
    }
}
