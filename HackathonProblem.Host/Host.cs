using HackathonProblem.Contracts.services;
using HackathonProblem.CsvEmployeeProvider;
using HackathonProblem.Db;
using HackathonProblem.Db.contexts;
using HackathonProblem.HackathonOrganizer;
using HackathonProblem.Host;
using HackathonProblem.HrDirector;
using HackathonProblem.HrManager;
using HackathonProblem.RandomWishlistsProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<DbSettings>(builder.Configuration.GetRequiredSection("Db"));
builder.Services.Configure<CsvSettings>(builder.Configuration.GetRequiredSection("Csv"));

builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<DbSettings>>().Value);
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<CsvSettings>>().Value);

builder.Services.AddHostedService<HackathonWorker>();
builder.Services.AddSingleton<IEmployeeProvider, CsvEmployeeProvider>();
builder.Services.AddSingleton<IWishlistProvider, RandomWishlistsProvider>();
builder.Services.AddSingleton<IHarmonizationCalculator, HrDirector>();
builder.Services.AddSingleton<ITeamBuildingStrategy, HrManager>();
builder.Services.AddSingleton<IHackathonOrganizer, HackathonOrganizer>();

builder.Services.AddDbContextFactory<PostgresContext>();
builder.Services.AddTransient<IDbService, DbService<PostgresContext>>();

var host = builder.Build();

host.Run();
