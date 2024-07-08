namespace APICatalog.Options;

public class ApiRateLimitOptions
{
    public const string ApiRateLimit = "ApiRateLimit";
    public int PermitLimit { get; set; } = 5;
    public int Window { get; set; } = 20;
    public int ReplenishmentPeriod { get; set; } = 10;
    public int QueuLimit { get; set; } = 4;
    public int SegmentsPerWindow { get; set; } = 3;
    public int TokenLimit { get; set; } = 5;
    public int TokenLimit2 { get; set; } = 10;
    public int TokenPerPeriod = 5;
    public bool AutoReplenishment { get; set; } = false;
}
