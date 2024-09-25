using System.Text;
using HackathonProblem.Contracts;
using HackathonProblem.CsvEmployeeProvider;
using HackathonProblem.HackathonOrganizer;
using HackathonProblem.Host;
using HackathonProblem.HrDirector;
using HackathonProblem.HrManager;
using HackathonProblem.RandomWishlistsProvider;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<HackathonWorker>();
builder.Services.AddTransient<IEmployeeProvider>(x =>
    ActivatorUtilities.CreateInstance<CsvEmployeeProvider>(x, ";", Encoding.UTF8));
builder.Services.AddTransient<IWishlistProvider, RandomWishlistsProvider>();
builder.Services.AddTransient<IHarmonizationCalculator, HrDirector>();
builder.Services.AddTransient<ITeamBuildingStrategy, HrManager>();
builder.Services.AddTransient<IHackathonOrganizer, HackathonOrganizer>();

var host = builder.Build();

host.Run();
