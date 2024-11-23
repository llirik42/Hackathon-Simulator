using System.ComponentModel.DataAnnotations;
using HackathonProblem.Common.domain.entities;

namespace HackathonProblem.Common.models;

public class WishlistRequest
{
    [Required] [Range(1, int.MaxValue)] public int EmployeeId { get; set; }

    [Required] [MinLength(1)] public required int[] DesiredEmployees { get; set; }

    public Wishlist ToWishlist()
    {
        return new Wishlist(EmployeeId, DesiredEmployees);
    }
}
