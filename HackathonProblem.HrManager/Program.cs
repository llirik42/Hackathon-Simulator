using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.mapping;
using HackathonProblem.CsvEmployeeProvider;
using HackathonProblem.HrManager.consumers;
using HackathonProblem.HrManager.domain;
using HackathonProblem.HrManager.models;
using HackathonProblem.HrManager.services.hrDirectorService;
using HackathonProblem.HrManager.services.hrDirectorService.wrapper;
using HackathonProblem.HrManager.services.teamService;
using HackathonProblem.HrManager.services.wishlistService;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<CsvConfig>(builder.Configuration.GetRequiredSection("Csv"));
builder.Services.Configure<HrDirectorConfig>(builder.Configuration.GetRequiredSection("HrDirector"));
builder.Services.Configure<HrManagerConfig>(builder.Configuration.GetRequiredSection("HrManager"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<CsvConfig>>().Value);
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<HrDirectorConfig>>().Value);
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<HrManagerConfig>>().Value);
builder.Services.AddSingleton<IWishlistService, ConcurrentWishlistService>();
builder.Services.AddSingleton<ITeamService, TeamService>();
builder.Services.AddSingleton<IHrDirector, HrDirectorService>();
builder.Services.AddSingleton<IHrManager, HrManager>();
builder.Services.AddSingleton(_ => new TeamMapper());
builder.Services.AddSingleton<IEmployeeProvider, CsvEmployeeProvider>();
builder.Services.AddSingleton<IHrDirectorWrapper, HrDirectorWrapper>();
builder.Services.AddSingleton<IHrDirector, HrDirectorService>();
builder.Services.AddHttpClient();
builder.Services.AddSingleton(_ => new Locker());
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

app.Run();
