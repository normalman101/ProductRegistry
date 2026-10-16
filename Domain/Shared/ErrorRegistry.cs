namespace ProductRegistry.Domain.Shared;

public static class ErrorRegistry
{
    public static Error None => new("None", "Ошибка отсутствует");

    public static Error IncorrectIdValue => new(
        "IncorrectIdValue",
        "Неправильное значение артикула"
    );

    public static Error IncorrectNameValue => new(
        "IncorrectNameValue",
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

    public static class Product
    {
        public static Error ProductsUnavailable => new(
            "NoProductsAvailable",
            "Товары отсутствуют"
        );

        public static Error NotFound => new(
            "ProductNotFound",
            "Товар не найден"
        );

        public static Error AlreadyExists => new(
            "ProductAlreadyExists",
            "Товар уже существует"
        );
    }
}