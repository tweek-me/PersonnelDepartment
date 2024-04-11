namespace PersonnelDepartment.Tools;

public class Page<T>
{
    public Int32 TotalRows { get; set; }
    public T[] Values { get; set; }

    public Page() { }

    public Page(Int32 totalRows, IEnumerable<T> values)
    {
        TotalRows = totalRows;
        Values = values.ToArray();
    }
}
