using PersonnelDepartment.Tools.Extensions;

namespace PersonnelDepartment.Tools.Database;

public abstract class BaseRepository
{
    public static (Int32 offset, Int32 limit) NormalizeRange(Int32 page, Int32 pageSize)
    {
        Int32 offset = ((page - 1) * pageSize).ButNotLess(0);
        Int32 limit = pageSize.ButNotLess(0);

        return (offset, limit);
    }
}
