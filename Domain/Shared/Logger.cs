using System;
using System.IO;
using System.Threading.Tasks;

namespace ProductRegistry.Domain.Shared;

public enum LogType : byte
{
    Info,
    Warn,
    Err
}

public sealed class Logger(string filePath)
{
    public async Task SendAsync(LogType type, string message)
    {
        await File.AppendAllLinesAsync(
            filePath,
            [$"{type},{message},{DateTime.Now}"]
        );
    }
}