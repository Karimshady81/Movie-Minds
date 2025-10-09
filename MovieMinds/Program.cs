using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MovieMinds.Data;
using MovieMinds.Repositories;
using MovieMinds.Repositories.Interfaces;
using System.Net.Http.Headers;
using MovieMinds.Models.Entites;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.AddControllersWithViews();
builder.Services.AddServerSideBlazor()
                .AddCircuitOptions(o => { o.DetailedErrors = true; });

builder.Services.AddDbContext<MovieMindsDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:MovieMindsDbContextConnection"]);
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<MovieMindsDbContext>()
    .AddDefaultTokenProviders();

var tmdbBase = builder.Configuration["Tmdb:BaseUrl"];
var tmdbToken = builder.Configuration["Tmdb:Token"] ?? 
                          throw new InvalidOperationException("TMDB token missing. Use dotnet user-secrets to set it.");

if (!string.IsNullOrWhiteSpace(tmdbBase) && !string.IsNullOrWhiteSpace(tmdbToken))
{
    builder.Services.AddHttpClient<IMovieApiClient, TmdbMovieApiClient>(client =>
    {
        client.BaseAddress = new Uri(tmdbBase);
        client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tmdbToken);
    });
}


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();

// *** Identity middleware order matters ***
app.UseAuthentication();
app.UseAuthorization();


app.MapDefaultControllerRoute();

//SignalR endpoints for Blazor server
app.MapBlazorHub();

app.Run();
