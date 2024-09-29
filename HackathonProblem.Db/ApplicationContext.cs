using HackathonProblem.Db.entities;
using Microsoft.EntityFrameworkCore;

namespace HackathonProblem.Db;

public class ApplicationContext : DbContext
{
    private readonly StreamWriter logStream = new("db-log.txt", true);
    
    public ApplicationContext()
    {
        Database.EnsureCreated();
    }

    public DbSet<Member> Members { get; set; } = null!;
    public DbSet<Hackathon> Hackathons { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Preference> Preferences { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string connectionString =
            "Host=localhost;Port=5432;Database=hackathon;Username=hackathon;Password=hackathon-password";
        optionsBuilder.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        
        optionsBuilder.LogTo(logStream.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hackathon>()
            .ToTable(t => t.HasCheckConstraint("Harmonization", "harmonization > 0"));
        modelBuilder.Entity<Hackathon>()
            .Property(h => h.CreationDate)
            .HasDefaultValueSql("now()");

        modelBuilder.Entity<Team>()
            .ToTable(t => t.HasCheckConstraint("Harmonization", "harmonization > 0"));
        modelBuilder.Entity<Team>().HasKey(t => new { t.HackathonId, t.TeamLeadId, t.JuniorId });

        modelBuilder.Entity<Preference>()
            .ToTable(t => t.HasCheckConstraint("Priority", "desired_member_priority > 0"));
        modelBuilder.Entity<Preference>().HasKey(p => new { p.HackathonId, p.MemberId, p.DesiredMemberId });
    }
    
    public override void Dispose()
    {
        base.Dispose();
        logStream.Dispose();
        GC.SuppressFinalize(this);
    }
 
    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await logStream.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
