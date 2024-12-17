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
builder.Services.AddSingleton<IWishlistProvider>(x =>
{
    var developerId = int.Parse(builder.Configuration["Developer:Id"] ?? "0");
    var developerType = builder.Configuration["Developer:Type"] ?? "Junior";
    var powerBase = developerType == "Junior" ? 2 : 3;
    var seed = (int)Math.Pow(powerBase, developerId);
    return ActivatorUtilities.CreateInstance<RandomWishlistsProvider>(x, seed);
});

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
