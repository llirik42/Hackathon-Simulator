using Microsoft.Extensions.Hosting;

namespace HackathonProblem.Developer;

public class Worker : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("123");
        
        return Task.CompletedTask;
    }
}
