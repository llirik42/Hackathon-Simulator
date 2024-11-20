using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.models;

namespace HackathonProblem.Developer.services;

public interface IHrManagerService
{
    Task<DetailResponse> PostWishlist(Wishlist wishlist);
}
