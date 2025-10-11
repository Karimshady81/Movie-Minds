using Microsoft.EntityFrameworkCore;
using MovieMinds.Data;
using MovieMinds.Repositories;
using MovieMinds.Repositories.Interfaces;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddControllersWithViews();
builder.Services.AddServerSideBlazor(options =>
{
    options.DetailedErrors = true;
    options.MaxBufferedUnacknowledgedRenderBatches = 10;
});

builder.Services.AddSignalR(options =>
{
    options.MaximumReceiveMessageSize = 102400000; // 100 MB (increased from default 32KB)
    options.EnableDetailedErrors = true;
});


builder.Services.AddDbContext<MovieMindsDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:MovieMindsDbContextConnection"]);
});

var tmdbBase = builder.Configuration["Tmdb:BaseUrl"];
var tmdbToken = builder.Configuration["Tmdb:Token"] ?? 
                          throw new InvalidOperationException("TMDB token missing. Use dotnet user-secrets to set it.");

builder.Services.AddHttpClient<IMovieApiClient, TmdbMovieApiClient>(client =>
{
    client.BaseAddress = new Uri(tmdbBase);
    client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tmdbToken);
});


var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapDefaultControllerRoute();

//SignalR endpoints for Blazor server
app.MapBlazorHub();

app.Run();
