using HackathonProblem.Contracts.services;
using Microsoft.Extensions.Hosting;

namespace HackathonProblem.Host;

public class HackathonWorker(
    IEmployeeProvider employeeProvider,
    IHackathonOrganizer organizer,
    IWishlistProvider wishlistProvider,
    IHackathonService hackathonService) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var juniors = employeeProvider.Provide("assets/Juniors50.csv");
        var teamLeads = employeeProvider.Provide("assets/Teamleads50.csv");

        double avg = 0;
        const int iterationsCount = 1;
        for (var i = 0; i < iterationsCount; i++)
        {
            var teamLeadsWishlists = wishlistProvider.ProvideTeamLeadsWishlists(juniors, teamLeads);
            var juniorsWishlists = wishlistProvider.ProvideJuniorsWishlists(juniors, teamLeads);
            var hackathon = organizer.Organize(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
            hackathonService.SaveHackathon(hackathon, teamLeadsWishlists, juniorsWishlists);
            var harmonization = hackathon.Harmonization;
            avg += harmonization;
            Console.WriteLine(harmonization);
        }

        Console.WriteLine($"\nAvg: {avg / iterationsCount}");

        return Task.CompletedTask;
    }
}
