using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.mapping;
using HackathonProblem.CsvEmployeeProvider;
using HackathonProblem.HrDirector.consumers;
using HackathonProblem.HrDirector.db;
using HackathonProblem.HrDirector.db.contexts;
using HackathonProblem.HrDirector.domain;
using HackathonProblem.HrDirector.models;
using HackathonProblem.HrDirector.services.hackathonOrganizer;
using HackathonProblem.HrDirector.services.hackathonService;
using HackathonProblem.HrDirector.services.storageService;
using HackathonProblem.HrDirector.workers;
using MassTransit;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<DbConfig>(builder.Configuration.GetRequiredSection("Db"));
builder.Services.Configure<CsvConfig>(builder.Configuration.GetRequiredSection("Csv"));
builder.Services.Configure<HrDirectorConfig>(builder.Configuration.GetRequiredSection("HrDirector"));

builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<CsvConfig>>().Value);
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<DbConfig>>().Value);
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<HrDirectorConfig>>().Value);

builder.Services.AddHealthChecks();
builder.Services.AddDbContextFactory<PostgresContext>();
builder.Services.AddTransient<IStorageService, DbStorageService<PostgresContext>>();
builder.Services.AddSingleton<IEmployeeProvider, CsvEmployeeProvider>();
builder.Services.AddSingleton<IHackathonOrganizer, HackathonOrganizer>();
builder.Services.AddSingleton<IHrDirector, HrDirector>();
builder.Services.AddSingleton(_ => new TeamMapper());
builder.Services.AddSingleton<IHackathonService, HackathonService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<HackathonDeclarationWorker>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<WishlistDeclarationConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("hackathon");
            h.Password("password");
        });
        cfg.ReceiveEndpoint("wishlists", e =>
        {
            e.ConfigureConsumer<WishlistDeclarationConsumer>(context);
        });
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.MapHealthChecks("/health");
app.UseRouting();

#pragma warning disable ASP0014
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
#pragma warning restore ASP0014

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.Run();
