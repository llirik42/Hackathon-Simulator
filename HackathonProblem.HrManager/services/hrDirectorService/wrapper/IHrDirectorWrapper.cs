using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models.responses;

namespace HackathonProblem.HrManager.services.hrDirectorService.wrapper;

public interface IHrDirectorWrapper : IHrDirector
{
    DetailResponse PostTeams(List<Team> teams, int hackathonId);
}
