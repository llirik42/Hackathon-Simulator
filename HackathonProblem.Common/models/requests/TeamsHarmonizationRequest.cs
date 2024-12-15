using System.ComponentModel.DataAnnotations;
using HackathonProblem.Common.domain.entities;

namespace HackathonProblem.Common.models.requests;

public class TeamsHarmonizationRequest
{
    [Required] public required List<Team> Teams { get; set; }

    [Required] public required List<Wishlist> TeamLeadsWishlists { get; set; }

    [Required] public required List<Wishlist> JuniorsWishlists { get; set; }
}
