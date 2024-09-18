var employeeProvider = new CsvEmployeeProvider.CsvEmployeeProvider
{
    Delimiter = ";"
};
var juniors = employeeProvider.Provide("assets/Juniors50.csv");
var teamLeads = employeeProvider.Provide("assets/Teamleads50.csv");

var wishlistsProvider = new RandomWishlistsProvider.RandomWishlistsProvider(juniors, teamLeads);
var organizer = new HackathonOrganizer.HackathonOrganizer();

double avg = 0;
const int iterationsCount = 1000;
for (var i = 0; i < iterationsCount; i++)
{
    var teamLeadsWishlists = wishlistsProvider.ProvideTeamLeadsWishlists();
    var juniorsWishlists = wishlistsProvider.ProvideJuniorsWishlists();
    var members = organizer.Organize(teamLeads, juniors, teamLeadsWishlists, juniorsWishlists);
    var harmonization = members.Harmonization;
    avg += harmonization;
    Console.WriteLine(harmonization);
}

Console.WriteLine($"\nAvg: {avg / iterationsCount}");
