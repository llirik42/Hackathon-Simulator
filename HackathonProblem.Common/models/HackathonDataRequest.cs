using System.ComponentModel.DataAnnotations;
using HackathonProblem.Common.domain.entities;

namespace HackathonProblem.Common.models;

public class HackathonDataRequest
{
    [Required]
    public required List<ShortTeam> teams { get; set; }
    
    [Required]
    public required List<Wishlist> juniorsWishlists { get; set; }

    [Required]
    public required List<Wishlist> teamLeadsWishlists { get; set; }
}
