using HackathonProblem.Contracts.dto;

namespace HackathonProblem.Contracts.services;

public interface IHackathonService
{
    int CreateHackathon(double harmonization);

    Hackathon FindHackathon(int hackathonId);

    double GetAverageHarmonization();
}
