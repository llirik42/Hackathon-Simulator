namespace HackathonProblem.Db.entities;

public class TeamEntity
{
    public int HackathonId { get; init; }
    public HackathonEntity? Hackathon { get; init; }

    public int TeamLeadId { get; init; }
    public TeamLeadEntity? TeamLead { get; init; }

    public int JuniorId { get; init; }
    public JuniorEntity? Junior { get; init; }
}
