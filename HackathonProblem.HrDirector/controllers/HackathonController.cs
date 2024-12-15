using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.mapping;
using HackathonProblem.Common.models;
using HackathonProblem.HrDirector.models;
using HackathonProblem.HrDirector.services.hackathonOrganizer;
using HackathonProblem.HrDirector.services.storageService;
using Microsoft.AspNetCore.Mvc;

namespace HackathonProblem.HrDirector.controllers;

public class HackathonController(
    HrDirectorConfig config,
    IStorageService storageService,
    IEmployeeProvider employeeProvider,
    IHackathonOrganizer hackathonOrganizer,
    TeamMapper teamMapper,
    ILogger<HackathonController> logger
)
{
    [HttpPost("hackathons")]
    public DetailResponse PostHackathon([FromBody] HackathonDataRequest request)
    {
        var juniors = employeeProvider.Provide(config.JuniorsUrl);
        var teamLeads = employeeProvider.Provide(config.TeamLeadsUrl);
        var teams = request.Teams.Select(t => teamMapper.ShortTeamToTeam(t, juniors, teamLeads)).ToList();
        var hackathon = hackathonOrganizer.Organize(request.TeamLeadsWishlists, request.JuniorsWishlists, teams);

        AddJuniorsToDatabase(juniors);
        AddTeamLeadsToDatabase(teamLeads);
        var (createdHackathonId, createdHackathonHarmonization) =
            storageService.CreateHackathon(hackathon.Harmonization);

        storageService.AddTeams(createdHackathonId, hackathon.Teams);
        storageService.AddJuniorWishlists(createdHackathonId, request.JuniorsWishlists);
        storageService.AddTeamLeadWishlists(createdHackathonId, request.TeamLeadsWishlists);

        logger.LogInformation("Hackathon {HackathonId} with harmonization {Harmonization} is created",
            createdHackathonId, createdHackathonHarmonization);

        return new DetailResponse("Teams and wishlists accepted");
    }

    private void AddJuniorsToDatabase(List<Employee> juniors)
    {
        foreach (var j in juniors.Where(j => !storageService.CreateJunior(j)))
            logger.LogWarning("Junior {JuniorId} already exists", j.Id);
    }

    private void AddTeamLeadsToDatabase(List<Employee> teamLeads)
    {
        foreach (var t in teamLeads.Where(j => !storageService.CreateTeamLead(j)))
            logger.LogWarning("Team lead {TeamLeadId} already exists", t.Id);
    }
}
