using ProductRegistry.Domain.Shared;

namespace ProductRegistry.Domain.ValueObjects;

public sealed record Quantity
{
    public int Value { get; }

    private Quantity(int value) => Value = value;

    public static Result<Quantity> Create(int value)
    {
        return value < 0
            ? Result<Quantity>.Failure(ErrorRegistry.IncorrectQuantityValue)
            : Result<Quantity>.Success(new Quantity(value));
    }
}