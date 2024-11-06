using HackathonProblem.Contracts.dto;
using HackathonProblem.Contracts.services;
using Microsoft.Extensions.Hosting;

namespace HackathonProblem.Host;

public class HackathonWorker(
    IEmployeeProvider employeeProvider,
    IHackathonOrganizer organizer,
    IWishlistProvider wishlistProvider,
    IDbService dbService) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var juniors = employeeProvider.Provide("assets/Juniors5.csv");
        var teamLeads = employeeProvider.Provide("assets/Teamleads5.csv");

        AddJuniorsToDatabase(juniors);
        AddTeamLeadsToDatabase(teamLeads);

        const int iterationsCount = 1;
        var hackathonId = -1;
        
        for (var i = 0; i < iterationsCount; i++)
        {
            var teamLeadsWishlists = wishlistProvider.ProvideTeamLeadsWishlists(juniors, teamLeads);
            var juniorsWishlists = wishlistProvider.ProvideJuniorsWishlists(juniors, teamLeads);
            var hackathon = organizer.Organize(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

            hackathonId = dbService.CreateHackathon(hackathon.Harmonization);

            dbService.AddTeams(hackathonId, hackathon.Teams);
            dbService.AddJuniorWishlists(hackathonId, juniorsWishlists);
            dbService.AddTeamLeadWishlists(hackathonId, teamLeadsWishlists);
        }

        var result = dbService.FindHackathon(hackathonId);

        if (result != null)
        {
            Console.WriteLine("Found hackathon!");
            Console.WriteLine($"Harmonization: {result.Harmonization}");
            Console.WriteLine($"Teams: {result.Teams.Count}");
        }

        Console.WriteLine($"\nAvg: {dbService.GetAverageHarmonization()}");

        return Task.CompletedTask;
    }

    private void AddJuniorsToDatabase(List<Employee> juniors)
    {
        foreach (var j in juniors.Where(j => !dbService.CreateJunior(j)))
            Console.WriteLine($"Warning: junior {j.Id} already exists!");
    }

    private void AddTeamLeadsToDatabase(List<Employee> teamLeads)
    {
        foreach (var t in teamLeads.Where(j => !dbService.CreateTeamLead(j)))
            Console.WriteLine($"Warning: team-lead {t.Id} already exists!");
    }
}
