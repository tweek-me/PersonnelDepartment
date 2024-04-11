namespace PersonnelDepartment.Tools.Results;

public class Error
{
    public string ErrorMessage { get; }

    public Error(string message) => ErrorMessage = message;
}

public static class ErrorsExtensions
{
    public static string AsString(this Error[] errors) => string.Join(',', errors.AsEnumerable());
}
