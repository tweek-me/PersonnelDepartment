using PersonnelDepartment.Domain.SickLists;
using PersonnelDepartment.Tools;

namespace PersonnelDepartment.Services.SickLists.Repository;

public interface ISickListsRepository
{
    void SaveSickList(SickListBlank sickListBlank);
    SickList? GetSickList(Guid id);
    Page<SickList> GetSickListsPage(Int32 page, Int32 pageSize);
    void RemoveSickList(Guid id);
}
