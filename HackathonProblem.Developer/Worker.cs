using HackathonProblem.Common.domain.contracts;
using Microsoft.Extensions.Hosting;

namespace HackathonProblem.Developer;

public class Worker(IEmployeeProvider employeeProvider) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var juniors = employeeProvider.Provide("Juniors5.csv");
        var teamLeads = employeeProvider.Provide("Teamleads5.csv");
        
        return Task.CompletedTask;
    }
}
