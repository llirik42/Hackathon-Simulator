using HackathonProblem.Common.domain.contracts;
using HackathonProblem.Common.mapping;
using HackathonProblem.CsvEmployeeProvider;
using HackathonProblem.HrManager.domain;
using HackathonProblem.HrManager.models;
using HackathonProblem.HrManager.services.hackathonService;
using HackathonProblem.HrManager.services.hrDirectorService;
using HackathonProblem.HrManager.services.wishlistService;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CsvConfig>(builder.Configuration.GetRequiredSection("Csv"));
builder.Services.Configure<HrDirectorConfig>(builder.Configuration.GetRequiredSection("HrDirector"));
builder.Services.Configure<HrManagerConfig>(builder.Configuration.GetRequiredSection("HrManager"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<CsvConfig>>().Value);
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<HrDirectorConfig>>().Value);
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<HrManagerConfig>>().Value);
builder.Services.AddSingleton<IWishlistService, ConcurrentWishlistService>();
builder.Services.AddSingleton<IHackathonService, HackathonService>();
builder.Services.AddSingleton<IHrDirector, HrDirectorService>();
builder.Services.AddSingleton<IHrManager, HrManager>();
builder.Services.AddSingleton(_ => new TeamMapper());
builder.Services.AddSingleton<IEmployeeProvider, CsvEmployeeProvider>();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

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
