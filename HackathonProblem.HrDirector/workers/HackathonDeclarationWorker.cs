using HackathonProblem.Common.models.message;
using HackathonProblem.HrDirector.services.storageService;
using MassTransit;

namespace HackathonProblem.HrDirector.workers;

public class HackathonDeclarationWorker(
    IBus bus,
    ILogger<HackathonDeclarationWorker> logger,
    IStorageService storageService) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var hackathonId = storageService.CreateHackathon();
        logger.LogInformation("Hackathon {Hackathon} has started", hackathonId);
        bus.Publish(new HackathonDeclaration { HackathonId = hackathonId }, cancellationToken);
        logger.LogInformation("Sent declaration of hackathon {Hackathon}", hackathonId);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
