using PersonnelDepartment.Domain.Posts;
using PersonnelDepartment.Services.Departments.Repository.Models;

namespace PersonnelDepartment.Services.Departments.Converters;

public static class PostsConverter
{
    public static Post ToPost(this PostDb db)
    {
        return new Post(db.Id, db.Departmentid, db.Name, db.Salary);
    }

    public static Post[] ToPosts(this PostDb[] dbs)
    {
        return dbs.Select(ToPost).ToArray();
    }
}
