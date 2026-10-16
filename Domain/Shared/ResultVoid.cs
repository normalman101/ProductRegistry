namespace ProductRegistry.Domain.Shared;

public sealed class ResultVoid(bool isSuccess, Error error)
{
    public bool IsSuccess { get; } = isSuccess;
    public bool IsFailure => !IsSuccess;
    public Error Error { get; } = error;

    public static ResultVoid Success() => new(true, ErrorRegistry.None);
    public static ResultVoid Failure(Error error) => new(false, error);
}