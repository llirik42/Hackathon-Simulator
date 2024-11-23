using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.mapping;
using HackathonProblem.CsvEmployeeProvider;
using HackathonProblem.HrManager.domain;
using HackathonProblem.HrManager.models;
using HackathonProblem.HrManager.services.hackathonService;
using HackathonProblem.HrManager.services.hrDirectorService;
using HackathonProblem.HrManager.services.wishlistService;
using Microsoft.Extensions.Options;

const string hrDirectorConnectionString = "http://localhost:5000";
const int employeeCount = 5;
const string juniorsUrl = "Juniors5.csv";
const string teamLadsUrl = "Teamleads5.csv";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.Configure<CsvSettings>(builder.Configuration.GetRequiredSection("Csv"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<CsvSettings>>().Value);

builder.Services.AddSingleton(_ =>
    new HrManagerConfig(juniorsUrl, teamLadsUrl, employeeCount, hrDirectorConnectionString));

builder.Services.AddSingleton<IWishlistService, ConcurrentWishlistService>();
builder.Services.AddSingleton<IHackathonService, HackathonService>();
builder.Services.AddSingleton<IHrDirector, HrDirectorService>();
builder.Services.AddSingleton<IHrManager, HrManager>();
builder.Services.AddSingleton(_ => new TeamMapper());
builder.Services.AddSingleton<IEmployeeProvider, CsvEmployeeProvider>();

builder.Services.AddHttpClient();


var app = builder.Build();


app.MapControllerRoute("default", "{controller=Juniors}");
app.MapControllerRoute("default", "{controller=TeamLeads}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
