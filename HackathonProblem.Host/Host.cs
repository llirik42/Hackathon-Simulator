using System.Text;
using HackathonProblem.Contracts.services;
using HackathonProblem.CsvEmployeeProvider;
using HackathonProblem.Db;
using HackathonProblem.Db.services;
using HackathonProblem.HackathonOrganizer;
using HackathonProblem.Host;
using HackathonProblem.HrDirector;
using HackathonProblem.HrManager;
using HackathonProblem.RandomWishlistsProvider;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

var dbConfiguration = new DbConfiguration("hackathon", "password", "hackathon", "localhost", 5432);

builder.Services.AddHostedService<HackathonWorker>();
builder.Services.AddTransient<IEmployeeProvider>(x =>
    ActivatorUtilities.CreateInstance<CsvEmployeeProvider>(x, ";", Encoding.UTF8));
builder.Services.AddTransient<IWishlistProvider, RandomWishlistsProvider>();
builder.Services.AddTransient<IHarmonizationCalculator, HrDirector>();
builder.Services.AddTransient<ITeamBuildingStrategy, HrManager>();
builder.Services.AddTransient<IHackathonOrganizer, HackathonOrganizer>();
builder.Services.AddTransient<IHackathonService>(x =>
    ActivatorUtilities.CreateInstance<HackathonService>(x, dbConfiguration));

var host = builder.Build();

host.Run();
