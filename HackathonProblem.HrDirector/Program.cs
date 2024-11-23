using HackathonProblem.Common.domain.contracts;
using HackathonProblem.CsvEmployeeProvider;
using HackathonProblem.HrDirector.db;
using HackathonProblem.HrDirector.db.contexts;
using HackathonProblem.HrDirector.domain;
using HackathonProblem.HrDirector.models;
using HackathonProblem.HrDirector.services;
using HackathonProblem.HrDirector.services.impl;
using Microsoft.Extensions.Options;

const string juniorsUrl = "Juniors5.csv";
const string teamLadsUrl = "Teamleads5.csv";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<DbSettings>(builder.Configuration.GetRequiredSection("Db"));
builder.Services.Configure<CsvSettings>(builder.Configuration.GetRequiredSection("Csv"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<CsvSettings>>().Value);
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<DbSettings>>().Value);
builder.Services.AddSingleton(_ => new HrDirectorConfig(juniorsUrl, teamLadsUrl));
builder.Services.AddDbContextFactory<PostgresContext>();
builder.Services.AddTransient<IStorageService, DbStorageService<PostgresContext>>();
builder.Services.AddSingleton<IEmployeeProvider, CsvEmployeeProvider>();
builder.Services.AddSingleton<IHackathonOrganizer, HackathonOrganizer>();
builder.Services.AddSingleton<IHrDirector, HrDirector>();
builder.Services.AddSingleton(_ => new TeamMapper());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllerRoute("default", "{controller=Hackathon}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
