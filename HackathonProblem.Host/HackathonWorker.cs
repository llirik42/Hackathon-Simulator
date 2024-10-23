using HackathonProblem.Contracts.dto;
using HackathonProblem.Contracts.services;
using HackathonProblem.Db.exceptions;
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
        
        const int iterationsCount = 2;
        for (var i = 0; i < iterationsCount; i++)
        {
            var teamLeadsWishlists = wishlistProvider.ProvideTeamLeadsWishlists(juniors, teamLeads);
            var juniorsWishlists = wishlistProvider.ProvideJuniorsWishlists(juniors, teamLeads);
            var hackathon = organizer.Organize(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);

            var hackathonId = dbService.CreateHackathon(hackathon.Harmonization);
            
            dbService.AddTeams(hackathonId, hackathon.Teams);
            dbService.AddJuniorWishlists(hackathonId, juniorsWishlists);
            dbService.AddTeamLeadWishlists(hackathonId, teamLeadsWishlists);
        }

        Console.WriteLine($"\nAvg: {dbService.GetAverageHarmonization()}");

        return Task.CompletedTask;
    }

    private void AddJuniorsToDatabase(List<Employee> juniors)
    {
        foreach (var j in juniors)
        {
            try
            {
                dbService.CreateJunior(j);
            }
            catch (JuniorAlreadyExistsException e)
            {
                Console.WriteLine($"Warning: {e.Message}");
            }
        }
    }

    private void AddTeamLeadsToDatabase(List<Employee> teamLeads)
    {
        foreach (var t in teamLeads)
        {
            try
            {
                dbService.CreateTeamLead(t);
            }
            catch (TeamLeadAlreadyExistsException e)
            {
                Console.WriteLine($"Warning: {e.Message}");
            }
        }

    }
}
