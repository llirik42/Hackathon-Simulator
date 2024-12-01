using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.mapping;
using HackathonProblem.Common.models;
using HackathonProblem.HrDirector.models;
using HackathonProblem.HrDirector.services.hackathonOrganizer;
using HackathonProblem.HrDirector.services.storageService;
using Microsoft.AspNetCore.Mvc;

namespace HackathonProblem.HrDirector.controllers;

[Route("hackathons")]
public class HackathonController(
    HrDirectorConfig config,
    IStorageService storageService,
    IEmployeeProvider employeeProvider,
    IHackathonOrganizer hackathonOrganizer,
    TeamMapper teamMapper)
{
    [HttpPost]
    public DetailResponse PostHackathon([FromBody] HackathonDataRequest request)
    {
        var juniors = employeeProvider.Provide(config.JuniorsUrl);
        var teamLeads = employeeProvider.Provide(config.TeamLeadsUrl);
        var teams = request.Teams.Select(t => teamMapper.ShortTeamToTeam(t, juniors, teamLeads)).ToList();
        var hackathon = hackathonOrganizer.Organize(request.TeamLeadsWishlists, request.JuniorsWishlists, teams);

        AddJuniorsToDatabase(juniors);
        AddTeamLeadsToDatabase(teamLeads);
        var hackathonId = storageService.CreateHackathon(hackathon.Harmonization);

        storageService.AddTeams(hackathonId, hackathon.Teams);
        storageService.AddJuniorWishlists(hackathonId, request.JuniorsWishlists);
        storageService.AddTeamLeadWishlists(hackathonId, request.TeamLeadsWishlists);

        return new DetailResponse("Teams and wishlists accepted");
    }

    private void AddJuniorsToDatabase(List<Employee> juniors)
    {
        foreach (var j in juniors.Where(j => !storageService.CreateJunior(j)))
            Console.WriteLine($"Warning: junior {j.Id} already exists!");
    }

    private void AddTeamLeadsToDatabase(List<Employee> teamLeads)
    {
        foreach (var t in teamLeads.Where(j => !storageService.CreateTeamLead(j)))
            Console.WriteLine($"Warning: team-lead {t.Id} already exists!");
    }
}
