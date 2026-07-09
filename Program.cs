using EcoMeal1.Components;
using EcoMeal1.Controllers;
using EcoMeal1.Database_CodeFirst;
using EcoMeal1.Entities_CodeFirst;
using EcoMeal1.Repositories;
using EcoMeal1.Repositories.Interfaces;
using EcoMeal1.Services;
using EcoMeal1.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EcoMealDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<EcoMealUser, IdentityRole>()
    .AddEntityFrameworkStores<EcoMealDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IBusinessesRepository, BusinessesRepository>();
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
//builder.Services.AddSingleton<IBusinessesRepository, BusinessesRepository>
builder.Services.AddScoped<IBusinessesService, BusinessesService>();
builder.Services.AddScoped<IPackageService, PackageService>();

builder.Services.AddControllers();
builder.Services.AddScoped<BusinessesController>();
builder.Services.AddScoped<PackageController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
