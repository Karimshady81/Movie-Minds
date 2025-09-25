using Microsoft.EntityFrameworkCore;
using MovieMinds.Data;
using MovieMinds.Models.SeedData;
using MovieMinds.Repositories;
using MovieMinds.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MovieMindsDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:MovieMindsDbContextConnection"]);
});


var app = builder.Build();

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapDefaultControllerRoute();
DbInitializer.Seed(app);

app.Run();
