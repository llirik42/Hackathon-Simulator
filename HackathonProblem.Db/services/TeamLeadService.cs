using HackathonProblem.Contracts.dto;
using HackathonProblem.Contracts.services;
using HackathonProblem.Db.entities;

namespace HackathonProblem.Db.services;

public class TeamLeadService(DbConfiguration configuration) : ITeamLeadService
{
    public int CreateTeamLead(string name)
    {
        using var db = new ApplicationContext(configuration);
        var teamLeadEntity = new TeamLeadEntity { Name = name };
        db.TeamLeads.Add(teamLeadEntity);
        db.SaveChanges();
        return teamLeadEntity.Id;
    }

    public void CreateTeamLead(int id, string name)
    {
        using var db = new ApplicationContext(configuration);
        var teamLeadEntity = new TeamLeadEntity { Id = id, Name = name };
        db.TeamLeads.Add(teamLeadEntity);
        db.SaveChanges();
    }

    public Employee? FindTeamLead(int id)
    {
        using var db = new ApplicationContext(configuration);
        var teamLeadEntity = db.Juniors.Find(id);
        return teamLeadEntity == null ? null : new Employee(teamLeadEntity.Id, teamLeadEntity.Name);
    }
}
