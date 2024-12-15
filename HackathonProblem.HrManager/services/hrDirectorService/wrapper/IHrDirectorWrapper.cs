using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models;
using HackathonProblem.Common.models.responses;

namespace HackathonProblem.HrManager.services.hrDirectorService.wrapper;

public interface IHrDirectorWrapper : IHrDirector
{
    DetailResponse PostHackathonData(List<Team> teams, List<Wishlist> juniorsWishlists, List<Wishlist> teamLeadsWishlists);
}
