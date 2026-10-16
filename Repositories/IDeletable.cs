using System.Threading.Tasks;
using ProductRegistry.Domain.Shared;

namespace ProductRegistry.Repositories;

public interface IDeletable<in TArgument>
{
    Task<ResultVoid> DeleteAsync(TArgument argument);
}