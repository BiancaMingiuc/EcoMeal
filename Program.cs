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

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(options => options.DetailedErrors = true);
builder.Services.AddCascadingAuthenticationState();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EcoMealDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<EcoMealUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<EcoMealDbContext>()
  .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.AccessDeniedPath = "/account/access-denied";

    options.SlidingExpiration = true;
});

builder.Services.Configure<SecurityStampValidatorOptions>(options=>
{
    options.ValidationInterval = TimeSpan.FromMinutes(30);
});

builder.Services.AddScoped<IBusinessesRepository, BusinessesRepository>();
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IBusinessesService, BusinessesService>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();

builder.Services.AddHostedService<ExpiredOrdersWorker>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    await DbSeeder.SeedAsync(scope.ServiceProvider, app.Configuration);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
