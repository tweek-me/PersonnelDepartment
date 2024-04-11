using PersonnelDepartment.Domain.SickLists;
using PersonnelDepartment.Services.SickLists.Repository;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Services.SickLists;

public class SickListsService : ISickListsService
{
    private readonly ISickListsRepository _sickListsRepository;

    public SickListsService(ISickListsRepository sickListsRepository)
    {
        _sickListsRepository = sickListsRepository;
    }

    public Result SaveSickList(SickListBlank sickListBlank)
    {
        _sickListsRepository.SaveSickList(sickListBlank);
        return Result.Success();
    }

    private SickList? GetSickList(Guid id)
    {
        return _sickListsRepository.GetSickList(id);
    }

    public Page<SickList> GetSickListsPage(Int32 page, Int32 pageSize)
    {
        return _sickListsRepository.GetSickListsPage(page, pageSize);
    }

    public Result RemoveSickList(Guid id)
    {
        SickList? sickList = GetSickList(id);
        if (sickList is null) throw new Exception($"{nameof(sickList)} is null");

        _sickListsRepository.RemoveSickList(id);
        return Result.Success();
    }
}
