using HackathonProblem.Common.models;
using Microsoft.AspNetCore.Mvc;

namespace HackathonProblem.HrManager.controllers;

[Route("heath-check")]
public class HealthCheckController
{
    [HttpGet]
    public DetailResponse HealthCheck()
    {
        return new DetailResponse("I am alive");
    }
}
