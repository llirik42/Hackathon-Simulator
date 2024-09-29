using HackathonProblem.Db.entities;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace HackathonProblem.Db;

public class ApplicationContext : DbContext
{
    public DbSet<Member> Members { get; set; } = null!;
    public DbSet<Hackathon> Hackathons { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Preference> Preferences { get; set; } = null!;
    
    public ApplicationContext()
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string connectionString = "Host=localhost;Port=5432;Database=hackathon;Username=hackathon;Password=hackathon-password";
        optionsBuilder.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
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
        modelBuilder.Entity<Team>().HasKey(t => new { t.HackathonId, t.TeamLeadId, t.JuniorId});
        
        modelBuilder.Entity<Preference>()
            .ToTable(t => t.HasCheckConstraint("Priority", "desired_member_priority > 0"));
        modelBuilder.Entity<Preference>().HasKey(p => new { p.HackathonId, p.MemberId, p.DesiredMemberId});
    }
}
