using ProductRegistry.Domain.Shared;
using ProductRegistry.Domain.ValueObjects;

namespace ProductRegistry.Domain.Entities;

public sealed class Product
{
    public Id Id { get; }
    public Name Name { get; }
    public Price Price { get; }
    public Quantity Quantity { get; }
    public bool IsAvailable { get; }

    private Product(Id id, Name name, Price price, Quantity quantity, bool isAvailable)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
        IsAvailable = isAvailable;
    }

    public static Result<Product> Create(string id, string name, decimal price, int quantity)
    {
        var idResult = Id.Create(id);
        if (idResult.IsFailure) return Result<Product>.Failure(idResult.Error);
        var nameResult = Name.Create(name);
        if (nameResult.IsFailure) return Result<Product>.Failure(nameResult.Error);
        var priceResult = Price.Create(price);
        if (priceResult.IsFailure) return Result<Product>.Failure(priceResult.Error);
        var quantityResult = Quantity.Create(quantity);
        if (quantityResult.IsFailure) return Result<Product>.Failure(quantityResult.Error);

        return Result<Product>.Success(new Product(
            idResult.Value,
            nameResult.Value,
            priceResult.Value,
            quantityResult.Value,
            quantityResult.Value.Value != 0
        ));
    }

    public override string ToString()
    {
        return $"""
                Id: {Id.Value}
                Name: {Name.Value}
                Price: {Price.Value}
                Quantity: {Quantity.Value}
                IsAvailable: {IsAvailable}
                """;
    }
    
    public static string ToCsv(Product product)
    {
        return $"{product.Id.Value}," +
               $"{product.Name.Value}," +
               $"{product.Price.Value}," +
               $"{product.Quantity.Value}," +
               $"{product.IsAvailable}";
    }
    
    public static Product ToProduct(string product)
    {
        var properties = product.Split(',');

        var id = properties[0];
        var name = properties[1];
        var price = decimal.Parse(properties[2]);
        var quantity = int.Parse(properties[3]);

        return Create(id, name, price, quantity).Value;
    }
}