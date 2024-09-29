namespace HackathonProblem.Db.entities;

public class Preference
{
    public int HackathonId { get; init; }
    public Hackathon Hackathon { get; init; }
    
    public int MemberId { get; init; }
    public Member Member { get; init; }
    
    public int DesiredMemberId { get; init; }
    public Member DesiredMember { get; init; }
    
    public int DesiredMemberPriority { get; init; }
}
