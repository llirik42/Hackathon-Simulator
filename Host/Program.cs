var employeeProvider = new CsvEmployeeProvider.CsvEmployeeProvider
{
    Delimiter = ";"
};
var juniors = employeeProvider.Provide("assets/Juniors50.csv");
var teamLeads = employeeProvider.Provide("assets/Teamleads50.csv");

var wishlistsProvider = new RandomWishlistsProvider.RandomWishlistsProvider(juniors, teamLeads);
var harmonizationCalculator = new HrDirector.HrDirector();

var strategy = new HrManager.HrManager(harmonizationCalculator);

double avg = 0;
const int iterationsCount = 1000;
for (var i = 0; i < iterationsCount; i++)
{
    var teamLeadsWishlists = wishlistsProvider.ProvideTeamLeadsWishlists().ToList();
    var juniorsWishlists = wishlistsProvider.ProvideJuniorsWishlists().ToList();
    var teams = strategy.BuildTeams(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
    var harmonization = harmonizationCalculator.Calculate(teams, teamLeadsWishlists, juniorsWishlists);
    avg += harmonization;
    Console.WriteLine(harmonization);
}

Console.WriteLine($"\nAvg: {avg / iterationsCount}");
