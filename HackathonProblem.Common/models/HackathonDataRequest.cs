using System.ComponentModel.DataAnnotations;
using HackathonProblem.Common.domain.entities;

namespace HackathonProblem.Common.models;

public class HackathonDataRequest
{
    [Required] public required List<ShortTeam> Teams { get; set; }

    [Required] public required List<Wishlist> JuniorsWishlists { get; set; }

    [Required] public required List<Wishlist> TeamLeadsWishlists { get; set; }
}
