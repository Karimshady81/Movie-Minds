using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieMinds.Data;
using MovieMinds.Models.Entites;
using MovieMinds.Repositories;
using MovieMinds.Repositories.Interfaces;
using System.Net.Http.Headers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

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

// Identity Configuration

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 3;

    // User settings
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
    .AddEntityFrameworkStores<MovieMindsDbContext>();

//This is cookie-based as we are using MVC with views
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();


//This was for testing purposes only - commenting out for now

// JWT Authentication Configuration
//var jwtSecret = builder.Configuration["Jwt:Secret"] ?? 
//                        throw new InvalidOperationException("JWT Secret is missing");

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true, // Check token was created by us
//            ValidateAudience = true, // Check token is for our users
//            ValidateLifetime = true, // Check token hasn't expired
//            ValidateIssuerSigningKey = true, // Check signature is valid
//            ValidIssuer = builder.Configuration["Jwt:Issuer"],
//            ValidAudience = builder.Configuration["Jwt:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(
//            Encoding.UTF8.GetBytes(jwtSecret))
//        };
//    });

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

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapDefaultControllerRoute();

//SignalR endpoints for Blazor server
app.MapBlazorHub();

app.Run();
