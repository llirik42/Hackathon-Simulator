namespace HackathonProblem.Db.entities;

public class TeamLeadPreferenceEntity
{
    public int HackathonId { get; init; }
    public HackathonEntity Hackathon { get; init; }

    public int TeamLeadId { get; init; }
    public TeamLeadEntity TeamLead { get; init; }

    public int DesiredJuniorId { get; init; }
    public JuniorEntity DesiredJunior { get; init; }

    public int DesiredJuniorPriority { get; init; }
}
