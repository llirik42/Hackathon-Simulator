using HackathonProblem.Db.entities;
using Microsoft.EntityFrameworkCore;

namespace HackathonProblem.Db;

public class ApplicationContext : DbContext
{
    private readonly string _connectionString;
    private readonly StreamWriter _logStream = new("db-log.txt", true);

    public ApplicationContext(DbConfiguration configuration)
    {
        var address = configuration.Address;
        var port = configuration.Port;
        var dbName = configuration.DbName;
        var userName = configuration.UserName;
        var password = configuration.Password;
        _connectionString = $"Host={address};Port={port};Database={dbName};Username={userName};Password={password}";
        Database.EnsureCreated();
    }

    public DbSet<TeamLeadEntity> TeamLeads { get; set; } = null!;
    public DbSet<JuniorEntity> Juniors { get; set; } = null!;
    public DbSet<HackathonEntity> Hackathons { get; set; } = null!;
    public DbSet<TeamEntity> Teams { get; set; } = null!;
    public DbSet<TeamLeadPreferenceEntity> TeamLeadPreferences { get; set; } = null!;
    public DbSet<JuniorPreferenceEntity> JuniorPreferences { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString).UseSnakeCaseNamingConvention();
        optionsBuilder.LogTo(_logStream.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HackathonEntity>()
            .ToTable(t => t.HasCheckConstraint("Harmonization", "harmonization > 0"));
        modelBuilder.Entity<HackathonEntity>()
            .Property(h => h.CreationDate)
            .HasDefaultValueSql("now()");

        modelBuilder.Entity<TeamEntity>()
            .ToTable(t => t.HasCheckConstraint("Harmonization", "harmonization > 0"));
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
        _logStream.Dispose();
        GC.SuppressFinalize(this);
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await _logStream.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
