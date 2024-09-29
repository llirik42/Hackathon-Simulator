namespace HackathonProblem.Db.entities;

public class Team
{
    public int HackathonId { get; init; }
    public Hackathon Hackathon { get; init; }

    public int TeamLeadId { get; init; }
    public Member TeamLead { get; init; }

    public int JuniorId { get; init; }
    public Member Junior { get; init; }

    public double Harmonization { get; init; }
}
