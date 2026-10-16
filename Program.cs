using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductRegistry.Application;
using ProductRegistry.Domain.Entities;
using ProductRegistry.Domain.Shared;
using ProductRegistry.Repositories;

namespace Homework;

class Program
{
    static async Task Main(string[] args)
    {
        List<Product> products =
        [
            Product.Create("1F", "Chips", 100, 2).Value,
            Product.Create("1T", "Keyboard", 500, 5).Value,
            Product.Create("2F", "Chair", 1000, 3).Value,
            Product.Create("3F", "Bed", 1500, 0).Value,
            Product.Create("2T", "Monitor", 2200, 4).Value,
            Product.Create("3T", "GPU", 5000, 0).Value,
            Product.Create("4T", "Headphones", 2200, 6).Value,
            Product.Create("2T", "Mouse", 1600, 3).Value
        ];

        Logger logger = new(@"C:\Projects\Rider\Homework\Data\logs.csv");

        ProductQueryRepository productQueryRepository = new(
            @"C:\Projects\Rider\Homework\Data\products.csv",
            logger
        );
        ProductCommandRepository productCommandRepository = new(
            @"C:\Projects\Rider\Homework\Data\products.csv",
            productQueryRepository,
            logger
        );
        UseCaseFindMostExpensiveProduct useCaseFindMostExpensiveProduct = new(productQueryRepository);
        UseCaseGetTotalCost useCaseGetTotalCost = new(productQueryRepository);
        UseCaseGetUnavailableProducts useCaseGetUnavailableProducts = new(productQueryRepository);

        foreach (var product in products)
        {
            await productCommandRepository.AddAsync(product);
        }

        var mostExpensiveProduct = await useCaseFindMostExpensiveProduct.ExecuteAsync();
        Console.WriteLine(mostExpensiveProduct.Value.ToString());

        var totalCost = await useCaseGetTotalCost.ExecuteAsync();
        Console.WriteLine(totalCost.Value);

        var unavailableProducts = await useCaseGetUnavailableProducts.ExecuteAsync();
        foreach (var product in unavailableProducts.Value)
        {
            Console.WriteLine(product.ToString());
        }
    }
}