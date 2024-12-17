namespace HackathonProblem.Common.models.message;

public class WishlistDeclaration
{
    public required int DeveloperId { get; init; }
    
    public required DeveloperType DeveloperType { get; init; }
    
    public required int[] DesiredEmployees { get; init; }

    public required int HackathonId { get; init; }
}
