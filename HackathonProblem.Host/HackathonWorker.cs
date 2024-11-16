using HackathonProblem.Contracts;
using Microsoft.Extensions.Hosting;

namespace HackathonProblem.Host;

public class HackathonWorker(
    IEmployeeProvider employeeProvider,
    IHackathonOrganizer organizer,
    IWishlistProvider wishlistProvider) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var juniors = employeeProvider.Provide("assets/Juniors50.csv");
        var teamLeads = employeeProvider.Provide("assets/Teamleads50.csv");

        double avg = 0;
        const int iterationsCount = 1000;
        for (var i = 0; i < iterationsCount; i++)
        {
            var teamLeadsWishlists = wishlistProvider.ProvideTeamLeadsWishlists(juniors, teamLeads);
            var juniorsWishlists = wishlistProvider.ProvideJuniorsWishlists(juniors, teamLeads);
            var members = organizer.Organize(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
            var harmonization = members.Harmonization;
            avg += harmonization;
            Console.WriteLine(harmonization);
        }

        Console.WriteLine($"\nAvg: {avg / iterationsCount}");

        return Task.CompletedTask;
    }
}
