namespace HackathonProblem.Common.models.message;

public class WishlistDeclaration
{
    public int DeveloperId { get; set; }
    
    public DeveloperType DeveloperType { get; set; }
    
    public int[] DesiredEmployees { get; set; }
}
