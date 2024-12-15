namespace HackathonProblem.Common.models.events;

public class WishlistDeclarationEvent
{
    public int DeveloperId { get; set; }
    
    public DeveloperType DeveloperType { get; set; }
    
    public int[] DesiredEmployees { get; set; }
}
