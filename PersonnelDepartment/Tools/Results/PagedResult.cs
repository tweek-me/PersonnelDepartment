namespace PersonnelDepartment.Tools.Results;

public class PagedResult<T>
{
    public Page<T> Data { get; set; }
    public Error[] Errors { get; set; }
    public Boolean IsSuccess => !Errors.Any();

    public PagedResult(Page<T> data, Error[] errors)
    {
        Data = data;
        Errors = errors;
    }
}
