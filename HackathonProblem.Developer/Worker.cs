using Microsoft.Extensions.Hosting;

namespace HackathonProblem.Developer;

public class Worker : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}
