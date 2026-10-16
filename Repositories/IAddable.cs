using System.Threading.Tasks;
using ProductRegistry.Domain.Shared;

namespace ProductRegistry.Repositories;

public interface IAddable<in TArgument>
{
    Task<ResultVoid> AddAsync(TArgument argument);
}