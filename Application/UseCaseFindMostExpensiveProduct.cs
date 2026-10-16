using System.Threading.Tasks;
using ProductRegistry.Domain.Entities;
using ProductRegistry.Domain.Shared;
using ProductRegistry.Repositories;

namespace ProductRegistry.Application;

public class UseCaseFindMostExpensiveProduct(ProductQueryRepository productQueryRepository)
{
    public async Task<Result<Product>> ExecuteAsync()
    {
        return await productQueryRepository.GetMostExpensiveAsync();
    }
}