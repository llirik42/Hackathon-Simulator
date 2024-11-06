namespace HackathonProblem.Db.entities;

public class TeamLeadPreferenceEntity
{
    public int HackathonId { get; init; }
    public HackathonEntity? Hackathon { get; init; }

    public required int TeamLeadId { get; init; }
    public TeamLeadEntity? TeamLead { get; init; }

    public required int DesiredJuniorId { get; init; }
    public JuniorEntity? DesiredJunior { get; init; }

    public required int DesiredJuniorPriority { get; init; }
}
