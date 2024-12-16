using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.mapping;
using HackathonProblem.Common.models;
using HackathonProblem.Common.models.message;
using HackathonProblem.HrDirector.models;
using HackathonProblem.HrDirector.services.hackathonOrganizer;
using HackathonProblem.HrDirector.services.storageService;
using MassTransit;

namespace HackathonProblem.HrDirector.services.hackathonService;

public class HackathonService(
    HrDirectorConfig config,
    IStorageService storageService,
    IEmployeeProvider employeeProvider,
    IHackathonOrganizer hackathonOrganizer,
    TeamMapper teamMapper,
    IBus bus,
    ILogger<HackathonService> logger) : IHackathonService
{
    private List<ShortTeam> _teams = [];
    private List<Wishlist> _juniorsWishlists = [];
    private List<Wishlist> _teamLeadsWishlists = [];
    private int _hackathonId;
    private int _hackathonCount;

    public void StartNewHackathon()
    {
        lock (this)
        {
            StartHackathonWithoutLock();
        }
    }
    
    private void StartHackathonWithoutLock()
    {
        var hackathonId = storageService.CreateHackathon();
        logger.LogInformation("Hackathon-{Hackathon} has started", hackathonId);
        
        _hackathonId = hackathonId;
        _teams = [];
        _juniorsWishlists = [];
        _teamLeadsWishlists = [];
        _hackathonCount++;
        
        var message = new HackathonDeclaration { HackathonId = hackathonId };
        
        bus.Publish(message);
        logger.LogInformation("Sent declaration of hackathon {Hackathon}", hackathonId);
    }

    public void ProcessTeams(List<ShortTeam> teams)
    {
        lock (this)
        {
            _teams = teams;
            Check();
        }
    }

    public void ProcessJuniorWishlist(Wishlist wishlist)
    {
        lock (this)
        {
            _juniorsWishlists.Add(wishlist);
            Check();
        }
    }

    public void ProcessTeamLeadWishlist(Wishlist wishlist)
    {
        lock (this)
        {
            _teamLeadsWishlists.Add(wishlist);
            Check();
        }
    }

    private void Check()
    {
        var employeeCount = config.EmployeeCount;
        
        var f1 = _teams.Count == employeeCount;
        var f2 = _juniorsWishlists.Count == employeeCount;
        var f3 = _teamLeadsWishlists.Count == employeeCount;

        if (!f1 || !f2 || !f3) return;
        
        UpdateCurrentHackathonData();

        if (_hackathonCount < config.HackathonCount)
        {
            StartHackathonWithoutLock();
        }
    }

    private void UpdateCurrentHackathonData()
    {
        var juniors = employeeProvider.Provide(config.JuniorsUrl);
        var teamLeads = employeeProvider.Provide(config.TeamLeadsUrl);
        var teams = _teams.Select(t => teamMapper.ShortTeamToTeam(t, juniors, teamLeads)).ToList();
        var hackathon = hackathonOrganizer.Organize(_teamLeadsWishlists, _juniorsWishlists, teams);

        AddJuniorsToDatabase(juniors);
        AddTeamLeadsToDatabase(teamLeads);

        storageService.SetHackathonHarmonization(_hackathonId, hackathon.Harmonization);
        storageService.AddTeams(_hackathonId, hackathon.Teams);
        storageService.AddJuniorWishlists(_hackathonId, _juniorsWishlists);
        storageService.AddTeamLeadWishlists(_hackathonId, _teamLeadsWishlists);

        logger.LogInformation("Data of hackathon {HackathonId} updated, harmonization is {Harmonization}", _hackathonId,
            hackathon.Harmonization);
    }

    private void AddJuniorsToDatabase(List<Employee> juniors)
    {
        foreach (var j in juniors.Where(j => !storageService.CreateJunior(j)))
            logger.LogWarning("Junior {JuniorId} already exists", j.Id);
    }

    private void AddTeamLeadsToDatabase(List<Employee> teamLeads)
    {
        foreach (var t in teamLeads.Where(j => !storageService.CreateTeamLead(j)))
            logger.LogWarning("Team lead {TeamLeadId} already exists", t.Id);
    }
}
