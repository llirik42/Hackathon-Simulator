using Hackathon_Simulator.services.impl;

var employeeProvider = new CsvEmployeeProvider
{
    Delimiter = ";"
};
var juniors = employeeProvider.Provide("assets/Juniors100.csv");
var teamLeads = employeeProvider.Provide("assets/Teamleads100.csv");

var wishlistsProvider = new RandomWishlistsProvider(juniors, teamLeads);
var harmonizationCalculator = new HarmonizationCalculatorImpl();

var strategy = new WikipediaStrategy(harmonizationCalculator);

double avg = 0;
const int iterationsCount = 100;
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
