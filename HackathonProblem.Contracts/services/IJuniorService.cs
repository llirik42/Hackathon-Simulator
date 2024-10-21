using HackathonProblem.Contracts.dto;

namespace HackathonProblem.Contracts.services;

public interface IJuniorService
{
    int CreateJunior(string name);

    void CreateJunior(int id, string name);

    Employee? FindJunior(int id);
}
