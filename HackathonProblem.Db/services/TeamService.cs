using HackathonProblem.Contracts.dto;
using HackathonProblem.Contracts.services;
using HackathonProblem.Db.entities;

namespace HackathonProblem.Db.services;

public class TeamService(DbConfiguration configuration) : ITeamService
{
    public List<Team> FindTeams(int hackathonId)
    {
        using var db = new ApplicationContext(configuration);
        var teamEntities = db.Teams.Where(te => te.HackathonId == hackathonId).ToList();
        var teams = teamEntities.Select(MapTeamEntity).ToList();
        return teams;
    }

    public void AddTeam(Team team, int hackathonId)
    {
        using var db = new ApplicationContext(configuration);
        var teamEntity = new TeamEntity
        {
            HackathonId = hackathonId,
            TeamLeadId = team.TeamLead.Id,
            JuniorId = team.Junior.Id
        };

        db.Teams.Add(teamEntity);
        db.SaveChanges();
    }

    public void AddTeams(List<Team> teams, int hackathonId)
    {
        using var db = new ApplicationContext(configuration);
        var teamEntities = teams.Select(p => new TeamEntity
            { HackathonId = hackathonId, TeamLeadId = p.TeamLead.Id, JuniorId = p.Junior.Id });

        db.Teams.AddRange(teamEntities);
        db.SaveChanges();
    }

    private static Team MapTeamEntity(TeamEntity teamEntity)
    {
        var teamLead = new Employee(teamEntity.TeamLeadId, teamEntity.TeamLead.Name);
        var junior = new Employee(teamEntity.JuniorId, teamEntity.Junior.Name);
        return new Team(teamLead, junior);
    }
}
