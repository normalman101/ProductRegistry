using System.IO;
using System.Threading.Tasks;
using ProductRegistry.Domain.Entities;
using ProductRegistry.Domain.Shared;
using ProductRegistry.Domain.ValueObjects;

namespace ProductRegistry.Repositories;

public sealed class ProductQueryRepository(
    string filePath,
    Logger logger
) : IGettable<Result<Product>, Id>
{
    public async Task<Result<Product>> GetByAsync(Id id)
    {
        var isFirst = true;

        await foreach (var line in File.ReadLinesAsync(filePath))
        {
            if (isFirst)
            {
                isFirst = false;
                continue;
            }

            if (line[..line.IndexOf(',')] != id.Value) continue;

            await logger.SendAsync(LogType.Info, "Товар нашелся");

            return Result<Product>.Success(Product.ToProduct(line));
        }

        await logger.SendAsync(LogType.Err, ErrorRegistry.ProductNotFound.Message);

        return Result<Product>.Failure(ErrorRegistry.ProductNotFound);
    }

    public async Task<Result<Product>> GetMostExpensiveAsync()
    {
        decimal max = 0;
        Product? mostExpensive = null;
        var isFirst = true;

        await foreach (var line in File.ReadLinesAsync(filePath))
        {
            if (isFirst)
            {
                isFirst = false;
                continue;
            }

            var current = decimal.Parse(line.Split(',')[2]);

            if (current <= max) continue;

            max = current;
            mostExpensive = Product.ToProduct(line);
        }

        if (mostExpensive is null)
        {
            await logger.SendAsync(LogType.Err, ErrorRegistry.ProductNotFound.Message);
            return Result<Product>.Failure(ErrorRegistry.ProductNotFound);
        }

        await logger.SendAsync(LogType.Info, "Самый дорогой товар нашелся");
        return Result<Product>.Success(mostExpensive);
    }

    public async Task<Result<Price>> GetTotalCostAsync()
    {
        decimal totalCost = 0;
        var isFirst = true;

        await foreach (var line in File.ReadLinesAsync(filePath))
        {
            if (isFirst)
            {
                isFirst = false;
                continue;
            }

            totalCost += decimal.Parse(line.Split(',')[2]) * int.Parse(line.Split(',')[3]);
        }

        if (totalCost == 0)
        {
            await logger.SendAsync(LogType.Err, ErrorRegistry.ProductsUnavailable.Message);
            return Result<Price>.Failure(ErrorRegistry.ProductsUnavailable);
        }

        await logger.SendAsync(LogType.Info, "Итоговая сумма получена");
        return Price.Create(totalCost);
    }

    public async Task<Result<Product[]>> GetUnavailableProductsAsync()
    {
        Product[] unavailableProducts = [];
        var isFirst = true;

        await foreach (var line in File.ReadLinesAsync(filePath))
        {
            if (isFirst)
            {
                isFirst = false;
                continue;
            }

            var product = Product.ToProduct(line);

            if (product.IsAvailable) continue;

            unavailableProducts = [.. unavailableProducts, product];
        }

        return Result<Product[]>.Success(unavailableProducts);
    }
}