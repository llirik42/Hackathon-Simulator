using HackathonProblem.Contracts.dto;

namespace HackathonProblem.Contracts.services;

public interface ITeamService
{
    List<Team> FindTeams(int hackathonId);

    void AddTeam(Team team, int hackathonId);

    void AddTeams(List<Team> teams, int hackathonId);
}
