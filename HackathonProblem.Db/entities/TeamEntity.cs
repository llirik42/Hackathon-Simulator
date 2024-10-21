namespace HackathonProblem.Db.entities;

public class TeamEntity
{
    public int HackathonId { get; init; }
    public HackathonEntity HackathonEntity { get; init; }

    public int TeamLeadId { get; init; }
    public TeamLeadEntity TeamLeadEntity { get; init; }

    public int JuniorId { get; init; }
    public JuniorEntity JuniorEntity { get; init; }

    public double Harmonization { get; init; }
}
