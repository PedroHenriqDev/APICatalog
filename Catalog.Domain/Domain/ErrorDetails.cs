using System.Text.Json;

namespace Catalog.Domain;

public sealed class ErrorDetails
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public string? Trace { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
