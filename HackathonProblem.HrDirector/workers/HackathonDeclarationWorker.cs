using HackathonProblem.HrDirector.services.hackathonService;

namespace HackathonProblem.HrDirector.workers;

public class HackathonDeclarationWorker(IHackathonService hackathonService) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        hackathonService.StartNewHackathon();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
