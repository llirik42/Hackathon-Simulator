using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models;

namespace HackathonProblem.Common.mapping;

public class TeamMapper
{
    public Team ShortTeamToTeam(ShortTeam shortTeam, List<Employee> juniors, List<Employee> teamLeads)
    {
        var teamLead = teamLeads.Find(t => t.Id == shortTeam.TeamLeadId);
        var junior = juniors.Find(j => j.Id == shortTeam.JuniorId);

        if (teamLead == null || junior == null) throw new InvalidShortTeamException();

        return new Team(teamLead, junior);
    }
}
