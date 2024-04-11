using PersonnelDepartment.Domain.Vacations;
using PersonnelDepartment.Tools;

namespace PersonnelDepartment.Services.Vacations.Repository;

public interface IVacationsRepository
{
    void SaveVacation(VacationBlank vacationBlank);
    Vacation? GetVacation(Guid id);
    Page<Vacation> GetVacationsPage(Int32 page, Int32 pageSize);
    void RemoveVacation(Guid id);
}
