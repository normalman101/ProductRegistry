using ProductRegistry.Domain.Shared;

namespace ProductRegistry.Domain.ValueObjects;

public sealed record Name
{
    public string Value { get; }

    private Name(string value) => Value = value;

    public static Result<Name> Create(string value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? Result<Name>.Failure(ErrorRegistry.IncorrectName)
            : Result<Name>.Success(new Name(value));
    }
}