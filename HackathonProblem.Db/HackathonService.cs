using HackathonProblem.Contracts.dto;
using HackathonProblem.Contracts.services;
using HackathonProblem.Db.entities;

namespace HackathonProblem.Db;

public class HackathonService(string userName, string password, string dbName, string address, int port) : IHackathonService
{
    public List<Employee> GetMembers(int hackathonId)
    {
        throw new NotImplementedException();
    }

    public List<Employee> GetJuniors(int hackathonId)
    {
        throw new NotImplementedException();
    }

    public List<Employee> GetSeniors(int hackathonId)
    {
        throw new NotImplementedException();
    }

    public List<Team> GetTeams(int hackathonId)
    {
        throw new NotImplementedException();
    }

    public double GetHarmonization(int hackathonId)
    {
        throw new NotImplementedException();
    }

    public List<Hackathon> findHackathon(int hackathonId)
    {
        throw new NotImplementedException();
    }

    public double GetAverageHarmonization()
    {
        throw new NotImplementedException();
    }

    public int SaveHackathon(Hackathon hackathon, List<Wishlist> teamLeadsWishlists, List<Wishlist> juniorsWishlists)
    {
        using var db = new ApplicationContext(userName, password, dbName, address, port);
        var hackathonEntity = HackathonToEntity(hackathon);

        var teams = hackathon.Teams.ToList();
        var juniorEntities = teams.Select(t => EmployeeToJunior(t.Junior)).ToList();
        var teamLeadEntities = teams.Select(t => EmployeeToTeamLead(t.TeamLead)).ToList();

        Console.WriteLine("Starting db actions ...");
        db.Hackathons.Add(hackathonEntity);
        Console.WriteLine("Added hackathon");
        Console.WriteLine($"Id of hackathon: {hackathonEntity.Id}");
        // db.Juniors.AddRange(juniorEntities);
        // Console.WriteLine("Added juniors");
        //
        // db.TeamLeads.AddRange(teamLeadEntities);
        // Console.WriteLine("Added team leads");
        
        db.SaveChanges();
        Console.WriteLine("Commited");
        
        return hackathonEntity.Id;
    }

    private static HackathonEntity HackathonToEntity(Hackathon hackathon)
    {
        return new HackathonEntity { Harmonization = hackathon.Harmonization };
    }
    
    private static JuniorEntity EmployeeToJunior(Employee employee)
    {
        return new JuniorEntity {Id = employee.Id, Name = employee.Name};
    }
    
    private static TeamLeadEntity EmployeeToTeamLead(Employee employee)
    {
        return new TeamLeadEntity {Id = employee.Id, Name = employee.Name};
    }
}
