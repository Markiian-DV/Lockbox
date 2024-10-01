namespace Lockbox.Application.Models;

public class Result
{
    public bool Success { get; set; } = true;
    public bool Failure => !Success;
    public string Error { get; set; } = string.Empty;

    public static Result FailedWithErrors(IEnumerable<string> errors)
    {
        return new()
        {
            Success = false,
            Error = string.Join("\n", errors)
        };
    }

    public static Result Succeeded() => new() { Success = true};
}

public class Result<T> : Result
{
    public T Value { get; set; } = default!;
}