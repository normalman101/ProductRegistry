using System.Threading.Tasks;
using ProductRegistry.Domain.Shared;

namespace ProductRegistry.Repositories;

public interface IUpdatable<in TArgument>
{
    Task<ResultVoid> UpdateAsync(TArgument argument);
}