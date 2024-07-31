using Catalog.Configuration.Resources;
using Catalog.ExceptionManager.ExceptionBase;
using Microsoft.Extensions.Configuration;

namespace Configuration.Settings;

public static class AppConfiguration
{
    private static IConfigurationRoot configuration;

    static AppConfiguration()
    {
        var configPath = PathsResource.CONFIG_PATH;

        var configFileName = ConfigurationResource.CONFIG_FILE;

        var builder = new ConfigurationBuilder()
            .SetBasePath(configPath)
            .AddJsonFile(configFileName, optional: false, reloadOnChange: true);

        configuration = builder.Build();
    }

    public static string GetConnectionString(string name)
    {
        return configuration.GetConnectionString(name) ?? throw new SettingsNotFoundException(ErrorMessagesResource.CONFIG_NOT_FOUND);
    }

    public static T GetValue<T>(string section, string key)
    {
        var value = configuration.GetSection(section).GetValue<T>(key);

        return value ?? throw new SettingsNotFoundException(ErrorMessagesResource.CONFIG_NOT_FOUND);
    }
}