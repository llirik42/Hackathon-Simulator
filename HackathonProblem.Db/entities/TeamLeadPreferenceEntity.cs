namespace HackathonProblem.Db.entities;

public class TeamLeadPreferenceEntity
{
    public int HackathonId { get; init; }
    public HackathonEntity HackathonEntity { get; init; }

    public int TeamLeadId { get; init; }
    public TeamLeadEntity TeamLeadEntity { get; init; }

    public int DesiredJuniorId { get; init; }
    public JuniorEntity DesiredJuniorEntity { get; init; }

    public int DesiredJuniorPriority { get; init; }
}
