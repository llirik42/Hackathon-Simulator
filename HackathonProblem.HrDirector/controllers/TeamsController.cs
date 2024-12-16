using HackathonProblem.Common.models.requests;
using HackathonProblem.Common.models.responses;
using HackathonProblem.HrDirector.services.hackathonService;
using Microsoft.AspNetCore.Mvc;

namespace HackathonProblem.HrDirector.controllers;

public class TeamsController(IHackathonService hackathonService, ILogger<TeamsController> logger)
{
    [HttpPost("teams")]
    public DetailResponse PostTeams([FromBody] TeamsRequest request)
    {
        var hackathonId = request.HackathonId;
        logger.LogInformation("Received teams of hackathon-{HackathonId}", hackathonId);
        hackathonService.SetCurrentHackathonId(hackathonId);
        hackathonService.ProcessTeams(request.Teams);
        return new DetailResponse("Teams accepted");
    }
}
