using HackathonProblem.Contracts.dto;
using HackathonProblem.Contracts.services;
using HackathonProblem.Db.entities;
using HackathonProblem.Db.exceptions;
using Microsoft.EntityFrameworkCore;

namespace HackathonProblem.Db;

public class DbService(DbConfiguration configuration) : IDbService
{
    public void CreateJunior(Employee junior)
    {
        using var db = CreateApplicationContext();
        var juniorId = junior.Id;
        var entity = new JuniorEntity { Id = juniorId, Name = junior.Name };
        db.Juniors.Add(entity);
        
        try
        {
            db.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new JuniorAlreadyExistsException(juniorId, e);
        }
    }

    public void CreateTeamLead(Employee teamLead)
    {
        using var db = CreateApplicationContext();
        var teamLeadId = teamLead.Id;
        var entity = new TeamLeadEntity { Id = teamLeadId, Name = teamLead.Name };
        db.TeamLeads.Add(entity);
        
        try
        {
            db.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new TeamLeadAlreadyExistsException(teamLeadId, e);
        }
    }

    public void AddJuniorWishlists(int hackathonId, List<Wishlist> juniorWishlists)
    {
        using var db = new ApplicationContext(configuration);
        
        var entities = new List<JuniorPreferenceEntity>();

        foreach (var (currentJuniorId, desiredTeamLeads) in juniorWishlists)
        {
            var currentJuniorPreferences = desiredTeamLeads.Select((desiredTeamLeadId, i) =>
                new JuniorPreferenceEntity
                {
                    HackathonId = hackathonId,
                    JuniorId = currentJuniorId,
                    DesiredTeamLeadId = desiredTeamLeadId,
                    DesiredTeamLeadPriority = i
                });
            
            entities.AddRange(currentJuniorPreferences);
        }
        
        db.JuniorPreferences.AddRange(entities);
        db.SaveChanges();
    }

    public void AddTeamLeadWishlists(int hackathonId, List<Wishlist> teamLeadWishlists)
    {
        using var db = new ApplicationContext(configuration);
        
        var entities = new List<TeamLeadPreferenceEntity>();

        foreach (var (currentTeamLeadId, desiredJuniors) in teamLeadWishlists)
        {
            var currentTeamLeadPreferences = desiredJuniors.Select((desiredJuniorId, i) =>
                new TeamLeadPreferenceEntity
                {
                    HackathonId = hackathonId,
                    TeamLeadId = currentTeamLeadId,
                    DesiredJuniorId = desiredJuniorId,
                    DesiredJuniorPriority = i
                });
                
            entities.AddRange(currentTeamLeadPreferences);
        }
        
        db.TeamLeadPreferences.AddRange(entities);
        db.SaveChanges();
    }

    public void AddTeams(int hackathonId, List<Team> teams)
    {
        using var db = new ApplicationContext(configuration);
        var teamEntities = teams.Select(p => new TeamEntity
            { HackathonId = hackathonId, TeamLeadId = p.TeamLead.Id, JuniorId = p.Junior.Id });

        db.Teams.AddRange(teamEntities);
        db.SaveChanges();
    }

    public int CreateHackathon(double harmonization)
    {
        using var db = new ApplicationContext(configuration);
        var hackathonEntity = new HackathonEntity { Harmonization = harmonization };
        db.Hackathons.Add(hackathonEntity);
        db.SaveChanges();
        return hackathonEntity.Id;
    }

    public double GetAverageHarmonization()
    {
        using var db = new ApplicationContext(configuration);

        if (!db.Hackathons.Any())
        {
            throw new NoHackathonsFoundException();
        }
        
        return db.Hackathons.Average(h => h.Harmonization);
    }

    private ApplicationContext CreateApplicationContext()
    {
        return new ApplicationContext(configuration);
    }
}
