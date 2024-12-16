using HackathonProblem.Common.domain.contracts;
using HackathonProblem.CsvEmployeeProvider;
using HackathonProblem.Developer.consumers;
using HackathonProblem.Developer.models;
using HackathonProblem.RandomWishlistsProvider;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<CsvConfig>(builder.Configuration.GetRequiredSection("Csv"));
builder.Services.Configure<DeveloperConfig>(builder.Configuration.GetRequiredSection("Developer"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<CsvConfig>>().Value);
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<DeveloperConfig>>().Value);
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IEmployeeProvider, CsvEmployeeProvider>();
builder.Services.AddSingleton<IWishlistProvider, RandomWishlistsProvider>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<HackathonDeclarationConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("hackathon");
            h.Password("password");
        });
        cfg.ReceiveEndpoint($"developer-{Guid.NewGuid()}", e =>
        {
            e.ConfigureConsumer<HackathonDeclarationConsumer>(context);
        });
        cfg.ConfigureEndpoints(context);
    });
});

var host = builder.Build();

host.Run();
