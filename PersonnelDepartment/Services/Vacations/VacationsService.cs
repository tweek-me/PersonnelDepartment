using PersonnelDepartment.Domain.Vacations;
using PersonnelDepartment.Services.Vacations.Repository;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Services.Vacations;

public class VacationsService : IVacationsService
{
    private readonly IVacationsRepository _vacationsRepository;

    public VacationsService(IVacationsRepository vacationsRepository)
    {
        _vacationsRepository = vacationsRepository;
    }

    public Result SaveVacation(VacationBlank vacationBlank)
    {
        _vacationsRepository.SaveVacation(vacationBlank);
        return Result.Success();
    }

    private Vacation? GetVacation(Guid id)
    {
        return _vacationsRepository.GetVacation(id);
    }

    public Page<Vacation> GetVacationsPage(Int32 page, Int32 pageSize)
    {
        return _vacationsRepository.GetVacationsPage(page, pageSize);
    }

    public Result RemoveVacation(Guid id)
    {
        Vacation? vacation = GetVacation(id);
        if (vacation is null) return Result.Fail("Выбранного отпуска не существует");

        _vacationsRepository.RemoveVacation(id);
        return Result.Success();
    }
}
