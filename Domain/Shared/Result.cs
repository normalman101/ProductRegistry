namespace ProductRegistry.Domain.Shared;

public sealed class Result<TValue>(bool isSuccess, TValue value, Error error)
{
    public bool IsSuccess { get; } = isSuccess;
    public bool IsFailure => !IsSuccess;
    public TValue Value { get; } = value;
    public Error Error { get; } = error;

    public static Result<TValue> Success(TValue value) => new(true, value, ErrorRegistry.None);
    public static Result<TValue> Failure(Error error) => new(false, default!, error);
}