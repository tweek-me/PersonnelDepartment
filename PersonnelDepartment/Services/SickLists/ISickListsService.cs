using PersonnelDepartment.Domain.SickLists;
using PersonnelDepartment.Tools;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Services.SickLists;

public interface ISickListsService
{
    Result SaveSickList(SickListBlank sickListBlank);
    Page<SickList> GetSickListsPage(Int32 page, Int32 pageSize);
    Result RemoveSickList(Guid id);
}
