namespace PersonnelDepartment.Tools.Results;

//TASK ДОДЕЛАТЬ КЛАСС
public class Result
{
    public Boolean IsSuccess => Errors.Length == 0;
    public Error[] Errors { get; set; }

    public Result(params Error[] errors)
    {
        Errors = errors;
    }

    public static Result Success() => new Result();

    public static Result Fail(String message) => new Result(new Error(message));
    public static Result Fail(Error error) => new Result(error);
    public static Result Fail(Error[] errors) => new Result(errors);
}
