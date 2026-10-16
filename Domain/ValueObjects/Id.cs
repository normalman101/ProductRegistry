using ProductRegistry.Domain.Shared;

namespace ProductRegistry.Domain.ValueObjects;

public sealed record Id
{
    public string Value { get; }

    private Id(string value) => Value = value;

    public static Result<Id> Create(string value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? Result<Id>.Failure(ErrorRegistry.IncorrectId)
            : Result<Id>.Success(new Id(value));
    }
}