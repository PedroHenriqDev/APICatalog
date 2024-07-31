using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Catalog.Application.Logging;

public class CustomLoggerProvider : ILoggerProvider
{
    readonly CustomLoggerProviderConfiguration loggerConfig;
    readonly ConcurrentDictionary<string, CustomerLogger> loggers = new ConcurrentDictionary<string, CustomerLogger>();
    public CustomLoggerProvider(CustomLoggerProviderConfiguration loggerConfig)
    {
        this.loggerConfig = loggerConfig;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return loggers.GetOrAdd(categoryName, name => new CustomerLogger(name, loggerConfig));
     }

    public void Dispose() 
    {
        loggers.Clear();
    }
}
