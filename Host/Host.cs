using System.Text;
using Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<HackathonWorker>();
builder.Services.AddTransient<IEmployeeProvider>(x =>
    ActivatorUtilities.CreateInstance<CsvEmployeeProvider.CsvEmployeeProvider>(x, ";", Encoding.UTF8));
builder.Services.AddTransient<IWishlistProvider, RandomWishlistsProvider.RandomWishlistsProvider>();
builder.Services.AddTransient<IHarmonizationCalculator, HrDirector.HrDirector>();
builder.Services.AddTransient<ITeamBuildingStrategy, HrManager.HrManager>();
builder.Services.AddTransient<IHackathonOrganizer, HackathonOrganizer.HackathonOrganizer>();

var host = builder.Build();

host.Run();
