namespace ProductRegistry.Domain.Shared;

public static class ErrorRegistry
{
    public static Error None => new("None", "Ошибка отсутствует");

    public static Error IncorrectId => new(
        "IncorrectId",
        "Неправильное значение артикула"
    );

    public static Error IncorrectName => new(
        "IncorrectName",
        "Неправильное значение имени"
    );

    public static Error IncorrectPriceValue => new(
        "IncorrectPriceValue",
        "Неправильное значение цены"
    );

    public static Error IncorrectQuantityValue => new(
        "IncorrectQuantityValue",
        "Неправльное значение количества"
    );

    public static Error ProductsUnavailable => new(
        "NoProductsAvailable",
        "Товары отсутствуют"
    );

    public static Error ProductNotFound => new(
        "ProductNotFound",
        "Товар не найден"
    );

    public static Error ProductAlreadyExists => new(
        "ProductAlreadyExists",
        "Товар уже существует"
    );
}