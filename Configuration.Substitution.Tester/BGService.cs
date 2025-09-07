using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Configuration.Substitution.Tester;

public class BGService(IOptions<Settings> settings, ILogger<BGService> logger, IHostApplicationLifetime lifetime)
    : BackgroundService
{
    private readonly Settings _settings = settings.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("BGService resolved endpoint {endpoint}", _settings.Endpoint);
        lifetime.StopApplication();
    }
}