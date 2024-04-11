namespace PersonnelDepartment.Domain.Employees;

public class Employee
{
    public Guid Id { get; }
    public Guid PostId { get; }
    public String Name { get; }
    public String Surname { get; }
    public String Partronymic { get; }
    public String PhoneNumber { get; }
    public String Email { get; }
    public String Inn { get; }
    public String Snils { get; }
    public Int32 PassportSeries { get; }
    public Int64 PassportNumber { get; }
    public DateTime BirthDay { get; }
    public Boolean IsDismissed { get; }

    public Employee(
        Guid id, Guid postId, String name, String surname,
        String partronymic, String phoneNumber, String email,
        String inn, String snils, Int32 passportSeries, Int64 passportNumber,
        DateTime birthDay, Boolean isDismissed
    )
    {
        Id = id;
        PostId = postId;
        Name = name;
        Surname = surname;
        Partronymic = partronymic;
        PhoneNumber = phoneNumber;
        Email = email;
        Inn = inn;
        Snils = snils;
        PassportSeries = passportSeries;
        PassportNumber = passportNumber;
        BirthDay = birthDay;
        IsDismissed = isDismissed;
    }
}

