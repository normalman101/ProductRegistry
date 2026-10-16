using System.IO;
using System.Threading.Tasks;
using ProductRegistry.Domain.Entities;
using ProductRegistry.Domain.Shared;
using ProductRegistry.Domain.ValueObjects;

namespace ProductRegistry.Repositories;

public sealed class ProductCommandRepository(
    string filePath,
    ProductQueryRepository productQueryRepository,
    Logger logger
) :
    IAddable<Product>,
    IUpdatable<Product>,
    IDeletable<Id>
{
    public async Task<ResultVoid> AddAsync(Product data)
    {
        var result = await productQueryRepository.GetByAsync(data.Id);

        if (result.IsSuccess)
        {
            await logger.SendAsync(LogType.Err, ErrorRegistry.Product.AlreadyExists.Message);

            return ResultVoid.Failure(ErrorRegistry.Product.AlreadyExists);
        }

        await File.AppendAllLinesAsync(filePath, [Product.ToCsv(data)]);
        await logger.SendAsync(LogType.Info, "Товар добавился");

        return ResultVoid.Success();
    }

    public async Task<ResultVoid> UpdateAsync(Product data)
    {
        string[] savedLines = [];
        var isFound = false;
        var isFirst = true;

        await foreach (var line in File.ReadLinesAsync(filePath))
        {
            if (isFirst)
            {
                isFirst = false;
                continue;
            }

            if (line[..line.IndexOf(',')] == data.Id.Value)
            {
                isFound = true;
                savedLines = [.. savedLines, Product.ToCsv(data)];
                continue;
            }

            savedLines = [.. savedLines, line];
        }


        if (!isFound)
        {
            await logger.SendAsync(LogType.Err, ErrorRegistry.Product.NotFound.Message);

            return ResultVoid.Failure(ErrorRegistry.Product.NotFound);
        }

        await File.WriteAllTextAsync(filePath, string.Empty);
        await File.AppendAllLinesAsync(filePath, savedLines);
        await logger.SendAsync(LogType.Info, "Товар обновился");

        return ResultVoid.Success();
    }

    public async Task<ResultVoid> DeleteAsync(Id data)
    {
        string[] savedLines = [];
        var isFound = false;
        var isFirst = true;

        await foreach (var line in File.ReadLinesAsync(filePath))
        {
            if (isFirst)
            {
                isFirst = false;
                continue;
            }

            if (line[..line.IndexOf(',')] == data.Value)
            {
                isFound = true;
                continue;
            }

            savedLines = [.. savedLines, line];
        }


        if (!isFound)
        {
            await logger.SendAsync(LogType.Err, ErrorRegistry.Product.NotFound.Message);

            return ResultVoid.Failure(ErrorRegistry.Product.NotFound);
        }

        await File.WriteAllTextAsync(filePath, string.Empty);
        await File.AppendAllLinesAsync(filePath, savedLines);
        await logger.SendAsync(LogType.Info, "Товар удалился");

        return ResultVoid.Success();
    }
}