namespace TechnicalPursuitApi.Infrastructure.Settings;

public class SerilogOptions
{
    public bool UseConsole { get; set; } = true;
    public string? ElasticSearchUrl { get; set; }

    public string LogTemplate { get; set; } =
        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level} - {Message:lj}{NewLine}{Exception}";
}