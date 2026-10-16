using ProductRegistry.Domain.Shared;

namespace ProductRegistry.Domain.ValueObjects;

public sealed record Price
{
    public decimal Value { get; }

    private Price(decimal value) => Value = value;

    public static Result<Price> Create(decimal value)
    {
        return value <= 0
            ? Result<Price>.Failure(ErrorRegistry.IncorrectPriceValue)
            : Result<Price>.Success(new Price(value));
    }
}