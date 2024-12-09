using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.models;
using Microsoft.AspNetCore.Mvc;

namespace HackathonProblem.HrDirector.controllers;

public class HarmonizationController(IHrDirector hrDirector)
{
    [HttpPost("mean")]
    public DoubleResponse Mean([FromBody] MeanHarmonicRequest request)
    {
        return new DoubleResponse(hrDirector.CalculateValue(request.Numbers));
    }
    
    [HttpPost("teams-harmonization")]
    public DoubleResponse TeamsHarmonization([FromBody] TeamsHarmonizationRequest request)
    {
        return new DoubleResponse(hrDirector.CalculateTeamsHarmonization(request.Teams, request.TeamLeadsWishlists, request.JuniorsWishlists));
    }
    
    [HttpPost("employee-harmonization")]
    public DoubleResponse TeamsHarmonization([FromBody] EmployeeHarmonizationRequest request)
    {
        return new DoubleResponse(
            hrDirector.CalculateEmployeeHarmonization(request.DesiredEmployees, request.DesiredEmployeeId));
    }
}
