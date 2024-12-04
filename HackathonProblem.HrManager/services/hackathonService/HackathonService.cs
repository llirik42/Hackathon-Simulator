using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models;
using HackathonProblem.HrManager.models;
using HackathonProblem.HrManager.services.hrDirectorService.wrapper;

namespace HackathonProblem.HrManager.services.hackathonService;

public class HackathonService(
    IEmployeeProvider employeeProvider,
    HrManagerConfig config,
    IHrManager hrManager,
    IHrDirectorWrapper hrDirectorWrapper) : IHackathonService
{
    public DetailResponse BuildTeamsAndPost(List<Wishlist> allJuniorsWishlists,
        List<Wishlist> allTeamLeadsWishlists)
    {
        var allJuniors = employeeProvider.Provide(config.JuniorsUrl);
        var allTeamLeads = employeeProvider.Provide(config.TeamLeadsUrl);

        // Juniors and team leads presented in wishlists
        var juniors = allJuniorsWishlists.Select(w => w.EmployeeId)
            .Select(juniorId => allJuniors.Single(y => y.Id == juniorId)).ToList();
        var teamLeads = allTeamLeadsWishlists.Select(w => w.EmployeeId)
            .Select(teamLeadId => allTeamLeads.Single(y => y.Id == teamLeadId)).ToList();
        
        Console.WriteLine("Teamleads");
        foreach (var x in teamLeads)
        {
            Console.Write(x + " ");
        }
        Console.WriteLine(" ");
        
        Console.WriteLine("Juniors");
        foreach (var x in juniors)
        {
            Console.Write(x + " ");
        }
        Console.WriteLine(" ");

        Console.WriteLine("wishlists: teamleads");
        foreach (var x in allTeamLeadsWishlists)
        {
            Console.Write(x.EmployeeId + " " + string.Join(", ", x.DesiredEmployees) + "\n");
        }
        Console.WriteLine(" ");
        
        Console.WriteLine("wishlists: juniors");
        foreach (var x in allJuniorsWishlists)
        {
            Console.Write(x.EmployeeId + " " + string.Join(", ", x.DesiredEmployees) + "\n");
        }
        Console.WriteLine(" ");
        
        var teams = hrManager.BuildTeams(teamLeads, juniors, allTeamLeadsWishlists, allJuniorsWishlists).ToList();

        Console.WriteLine("Teams built!");

        var response = hrDirectorWrapper.PostHackathonData(teams, allJuniorsWishlists, allTeamLeadsWishlists);
        
        Console.WriteLine($"Teams sent: {response.Detail}");
        
        return response;
    }
}
