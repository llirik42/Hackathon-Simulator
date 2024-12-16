using System.Data;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.HrDirector.db.contexts;
using HackathonProblem.HrDirector.db.entities;
using HackathonProblem.HrDirector.models;
using HackathonProblem.HrDirector.services.storageService;
using Microsoft.EntityFrameworkCore;

namespace HackathonProblem.HrDirector.db;

public class DbStorageService<T>(IDbContextFactory<T> factory) : IStorageService where T : AbstractContext
{
    private readonly AbstractContext _db = factory.CreateDbContext();

    public bool CreateJunior(Employee junior)
    {
        using var transaction = _db.Database.BeginTransaction(IsolationLevel.RepeatableRead);

        if (_db.Juniors.Any(j => j.Id == junior.Id)) return false;

        var juniorId = junior.Id;
        var entity = new JuniorEntity { Id = juniorId, Name = junior.Name };

        _db.Juniors.Add(entity);
        _db.SaveChanges();
        transaction.Commit();

        return true;
    }

    public bool CreateTeamLead(Employee teamLead)
    {
        using var transaction = _db.Database.BeginTransaction(IsolationLevel.RepeatableRead);

        if (_db.TeamLeads.Any(t => t.Id == teamLead.Id)) return false;

        var teamLeadId = teamLead.Id;
        var entity = new TeamLeadEntity { Id = teamLeadId, Name = teamLead.Name };

        _db.TeamLeads.Add(entity);
        _db.SaveChanges();
        transaction.Commit();

        return true;
    }

    public void AddJuniorWishlists(int hackathonId, List<Wishlist> juniorWishlists)
    {
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

        _db.JuniorPreferences.AddRange(entities);
        _db.SaveChanges();
    }

    public void AddTeamLeadWishlists(int hackathonId, List<Wishlist> teamLeadWishlists)
    {
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

        _db.TeamLeadPreferences.AddRange(entities);
        _db.SaveChanges();
    }

    public void AddTeams(int hackathonId, List<Team> teams)
    {
        var teamEntities = teams.Select(p => new TeamEntity
            { HackathonId = hackathonId, TeamLeadId = p.TeamLead.Id, JuniorId = p.Junior.Id });

        _db.Teams.AddRange(teamEntities);
        _db.SaveChanges();
    }

    public int CreateHackathon()
    {
        var hackathonEntity = new HackathonEntity();

        _db.Hackathons.Add(hackathonEntity);
        _db.SaveChanges();

        return hackathonEntity.Id;
    }

    public CreatedHackathon CreateHackathon(double harmonization)
    {
        var hackathonEntity = new HackathonEntity{Harmonization = harmonization};
        
        _db.Hackathons.Add(hackathonEntity);
        _db.SaveChanges();

        return new CreatedHackathon(hackathonEntity.Id, hackathonEntity.Harmonization);
    }

    public void SetHackathonHarmonization(int hackathonId, double harmonization)
    {
        using var transaction = _db.Database.BeginTransaction(IsolationLevel.RepeatableRead);
        
        var hackathonEntity = _db.Hackathons.Find(hackathonId);
        if (hackathonEntity == null)
        {
            throw new HackathonNotFoundException(hackathonId);
        }

        hackathonEntity.Harmonization = harmonization;
        
        _db.SaveChanges();
        transaction.Commit();
    }

    public double GetAverageHarmonization()
    {
        using var transaction = _db.Database.BeginTransaction(IsolationLevel.RepeatableRead);

        if (!_db.Hackathons.Any()) throw new NoHackathonsFoundException();

        var result = _db.Hackathons.Average(h => h.Harmonization);

        if (result == null)
        {
            throw new NoHackathonsFoundException();
        }

        return result.Value;
    }

    public Hackathon? FindHackathon(int hackathonId)
    {
        using var transaction = _db.Database.BeginTransaction(IsolationLevel.RepeatableRead);

        var hackathonEntity = _db.Hackathons.Find(hackathonId);

        if (hackathonEntity == null)
        {
            return null;
        }

       
        var harmonization = hackathonEntity.Harmonization;
        if (harmonization == null)
        {
            throw new NoHarmonizationException(hackathonId);
        }
        
        var teamsEntities = _db.Teams.Where(t => t.HackathonId == hackathonEntity.Id);
        var teams = (from t in teamsEntities.Include(teamEntity => teamEntity.TeamLead)
                .Include(teamEntity => teamEntity.Junior)
            let teamLead = TeamLeadToEmployee(t.TeamLead)
            let junior = JuniorToEmployee(t.Junior)
            select new Team(teamLead, junior)).ToList();

        return new Hackathon(teams, harmonization.Value);
    }

    private static Employee JuniorToEmployee(JuniorEntity junior)
    {
        return new Employee(junior.Id, junior.Name);
    }

    private static Employee TeamLeadToEmployee(TeamLeadEntity teamLead)
    {
        return new Employee(teamLead.Id, teamLead.Name);
    }
}
