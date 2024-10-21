using HackathonProblem.Contracts.dto;

namespace HackathonProblem.Contracts.services;

public interface ITeamLeadService
{
    int CreateTeamLead(string name);

    void CreateTeamLead(int id, string name);

    Employee? FindTeamLead(int id);
}
