using System.Threading.Tasks;
using ProductRegistry.Domain.Shared;
using ProductRegistry.Domain.ValueObjects;
using ProductRegistry.Repositories;

namespace ProductRegistry.Application;

public class UseCaseGetTotalCost(ProductQueryRepository productQueryRepository)
{
    public async Task<Result<Price>> ExecuteAsync()
    {
        return await productQueryRepository.GetTotalCostAsync();
    }
}