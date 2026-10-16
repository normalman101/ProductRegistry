using System.Threading.Tasks;

namespace ProductRegistry.Repositories;

public interface IGettable<TReturnValue, in TArgument>
{
    Task<TReturnValue> GetByAsync(TArgument argument);
}