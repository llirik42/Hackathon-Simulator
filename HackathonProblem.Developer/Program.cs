using HackathonProblem.Common.domain.contracts;
using HackathonProblem.CsvEmployeeProvider;
using HackathonProblem.Developer;
using HackathonProblem.Developer.models;
using HackathonProblem.Developer.services.hrManagerService;
using HackathonProblem.RandomWishlistsProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<HrManagerConfig>(builder.Configuration.GetRequiredSection("HrManager"));
builder.Services.Configure<CsvSettings>(builder.Configuration.GetRequiredSection("Csv"));
builder.Services.Configure<DeveloperConfig>(builder.Configuration.GetRequiredSection("Developer"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<HrManagerConfig>>().Value);
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<CsvSettings>>().Value);
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<DeveloperConfig>>().Value);
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IEmployeeProvider, CsvEmployeeProvider>();
builder.Services.AddSingleton<IWishlistProvider, RandomWishlistsProvider>();
builder.Services.AddSingleton<IHrManagerService, HrManagerService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();

host.Run();
