using PersonnelDepartment.Domain.Vacations;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Services.Vacations;

public interface IVacationsService
{
    Result SaveVacation(VacationBlank vacationBlank);
    Page<Vacation> GetVacationsPage(Int32 page, Int32 pageSize);
    Result RemoveVacation(Guid id);
}
