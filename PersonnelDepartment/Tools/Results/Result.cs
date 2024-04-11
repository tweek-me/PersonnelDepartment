namespace PersonnelDepartment.Tools.Results;

//TASK ILYA ДОДЕЛАТЬ КЛАСС
public class Result
{
    public bool IsSuccess => Errors.Length == 0;
    public Error[] Errors { get; set; }

    public string ErrorsAsString => Errors.AsString();

    public Result(params Error[] errors)
    {
        Errors = errors;
    }

    public static Result Success() => new Result();

    public static Result Fail(string message) => new Result(new Error(message));
    public static Result Fail(Error error) => new Result(error);
    public static Result Fil(Error[] errors) => new Result(errors);
}
