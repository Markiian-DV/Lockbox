namespace Lockbox.Application.Models;

public class Result
{
    public bool Success { get; set; } = true;
    public bool Failure => !Success;
    public string Error { get; set; } = string.Empty;
}

public class Result<T> : Result
{
    public T Value { get; set; }
}