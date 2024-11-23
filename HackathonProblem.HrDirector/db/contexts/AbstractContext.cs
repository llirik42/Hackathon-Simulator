using HackathonProblem.HrDirector.db.entities;
using Microsoft.EntityFrameworkCore;

namespace HackathonProblem.HrDirector.db.contexts;

public class AbstractContext : DbContext
{
    public DbSet<TeamLeadEntity> TeamLeads { get; set; } = null!;
    public DbSet<JuniorEntity> Juniors { get; set; } = null!;
    public DbSet<HackathonEntity> Hackathons { get; set; } = null!;
    public DbSet<TeamEntity> Teams { get; set; } = null!;
    public DbSet<TeamLeadPreferenceEntity> TeamLeadPreferences { get; set; } = null!;
    public DbSet<JuniorPreferenceEntity> JuniorPreferences { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HackathonEntity>()
            .ToTable(t => t.HasCheckConstraint("Harmonization", "harmonization > 0"));
        modelBuilder.Entity<HackathonEntity>()
            .Property(h => h.CreationDate)
            .HasDefaultValueSql("now()");

        modelBuilder.Entity<TeamEntity>().HasKey(t => new { t.HackathonId, t.TeamLeadId, t.JuniorId });

        modelBuilder.Entity<TeamLeadPreferenceEntity>()
            .ToTable(t => t.HasCheckConstraint("junior priority", "desired_junior_priority >= 0"));
        modelBuilder.Entity<TeamLeadPreferenceEntity>()
            .HasKey(p => new { p.HackathonId, p.TeamLeadId, p.DesiredJuniorId, p.DesiredJuniorPriority });

        modelBuilder.Entity<JuniorPreferenceEntity>()
            .ToTable(t => t.HasCheckConstraint("team-lead priority", "desired_team_lead_priority >= 0"));
        modelBuilder.Entity<JuniorPreferenceEntity>()
            .HasKey(p => new { p.HackathonId, p.JuniorId, p.DesiredTeamLeadId, p.DesiredTeamLeadPriority });
    }

    public override void Dispose()
    {
        base.Dispose();
        GC.SuppressFinalize(this);
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
