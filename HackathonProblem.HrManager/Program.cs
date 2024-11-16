using System.Collections.Concurrent;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.HrManager.models;

var count = 5;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var juniorWishlists = new ConcurrentQueue<Wishlist>();
var teamLeadWishlists = new ConcurrentQueue<Wishlist>();

app.MapPost("/juniors", (JuniorWishlistRequest request) =>
{
    juniorWishlists.Enqueue(new Wishlist(request.JuniorId, request.DesiredTeamLeads));
    
});

app.MapPost("/team-leads", (TeamLeadWithlistRequest request) =>
{
    teamLeadWishlists.Enqueue(new Wishlist(request.TeamLeadId, request.DesiredJuniors));
});


app.Run();
