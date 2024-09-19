using Contracts;
using Microsoft.Extensions.Hosting;

public class HackathonWorker(
    IEmployeeProvider employeeProvider,
    IHackathonOrganizer organizer
) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var juniors = employeeProvider.Provide("assets/Juniors50.csv");
        var teamLeads = employeeProvider.Provide("assets/Teamleads50.csv");

        double avg = 0;
        const int iterationsCount = 1000;
        for (var i = 0; i < iterationsCount; i++)
        {
            var members = organizer.Organize(teamLeads, juniors);
            var harmonization = members.Harmonization;
            avg += harmonization;
            Console.WriteLine(harmonization);
        }

        Console.WriteLine($"\nAvg: {avg / iterationsCount}");

        return Task.CompletedTask;
    }
}
