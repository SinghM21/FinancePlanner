using FinancePlanner.Background;
using FinancePlanner.Contexts;
using FinancePlanner.Mappers;
using FinancePlanner.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vite.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddViteServices(options =>
{
    options.Server.AutoRun = true;
    options.Server.PackageDirectory = "Frontend";
});
builder.Services.AddDbContext<FinancePlannerContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Register mapper and service for DI
builder.Services.AddScoped<IInvestmentMapper, InvestmentMapper>();
builder.Services.AddScoped<IInvestmentService, InvestmentService>();
builder.Services.AddHostedService<ApiPollingWorker>();
builder.Services.AddSingleton<IStockService, StockService>();
builder.Services.AddScoped<IStockDataParser, StockDataParser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<FinancePlannerContext>();
        dbContext.Database.Migrate();
    }
    app.UseViteDevelopmentServer(true);
}

app.Run();
