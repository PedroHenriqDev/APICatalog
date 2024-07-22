using Configuration.Resources;
using Configuration.Settings;

namespace Configuration.Options;

public static class ApiRateLimitOptions
{
    private static string section => ConfigurationResource.CONFIG_RATE_LIMIT_SECTION;
    public static int QueuLimit => AppConfiguration.GetValue<int>(section, ConfigurationResource.CONFIG_RATE_LIMIT_QUEULIMIT);
    public static int SegmentsPerWindow => AppConfiguration.GetValue<int>(section, ConfigurationResource.CONFIG_RATE_LIMIT_SEGMENTS_PER_WINDOW);
    public static int TokenLimit => AppConfiguration.GetValue<int>(section, ConfigurationResource.CONFIG_RATE_LIMIT_TOKEN_LIMIT);
    public static int TokenPerPeriod => AppConfiguration.GetValue<int>(section, ConfigurationResource.CONFIG_RATE_LIMIT_TOKEN_PER_PERIOD);
    public static bool AutoReplenishment => AppConfiguration.GetValue<bool>(section, ConfigurationResource.CONFIG_RATE_LIMIT_AUTO_REPLENISHMENT);
    public static int PermitLimit => AppConfiguration.GetValue<int>(section, ConfigurationResource.CONFIG_RATE_LIMIT_PERMIT_LIMIT); 
    public static int Window => AppConfiguration.GetValue<int>(section, ConfigurationResource.CONFIG_RATE_LIMIT_WINDOW);
    public static int ReplenishmentPeriod => AppConfiguration.GetValue<int>(section, ConfigurationResource.CONFIG_RATE_LIMIT_REPLENISHMENTPERIOD);
}
