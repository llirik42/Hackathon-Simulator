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

const DeveloperType type = DeveloperType.Junior;
const int id = 133;
const string hrManagerConnectionString = "http://localhost:5000";
const string juniorsUrl = "Juniors5.csv";
const string teamLadsUrl = "Teamleads5.csv";


builder.Services.Configure<CsvSettings>(builder.Configuration.GetRequiredSection("Csv"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<CsvSettings>>().Value);

builder.Services.AddSingleton(_ =>
    new DeveloperConfig(id, type, juniorsUrl, teamLadsUrl));

builder.Services.AddSingleton(_ => new HrManagerConfig(hrManagerConnectionString));

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IEmployeeProvider, CsvEmployeeProvider>();
builder.Services.AddSingleton<IWishlistProvider, RandomWishlistsProvider>();
builder.Services.AddSingleton<IHrManagerService, HrManagerService>();


builder.Services.AddHostedService<Worker>();

var host = builder.Build();

host.Run();
