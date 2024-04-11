namespace PersonnelDepartment.Domain.Employees;

public class EmployeeBlank
{
    public Guid? Id { get; set; }
    public Guid? PostId { get; set; }
    public String? Name { get; set; }
    public String? Surname { get; set; }
    public String? Partronymic { get; set; }
    public String? PhoneNumber { get; set; }
    public String? Email { get; set; }
    public String? Inn { get; set; }
    public String? Snils { get; set; }
    public Int32? PassportSeries { get; set; }
    public Int64? PassportNumber { get; set; }
    public DateTime? BirthDay { get; set; }
    public Boolean IsDismissed { get; set; }
}
