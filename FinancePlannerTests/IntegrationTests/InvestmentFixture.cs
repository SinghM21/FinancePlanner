using FinancePlanner.Contexts;
using FinancePlanner.Mappers;
using FinancePlanner.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinancePlannerTests.IntegrationTests;

public class InvestmentFixture : IAsyncLifetime
{
    private ServiceProvider _serviceProvider;
    private SqliteConnection _connection;
    
    public FinancePlannerContext Context;
    public IInvestmentService Service;
    
    
    
    public async Task InitializeAsync()
    {
        var serviceCollection = new ServiceCollection();

        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        
        // Register services
        serviceCollection.AddDbContext<FinancePlannerContext>(options =>
            options.UseSqlite(_connection));
        serviceCollection.AddScoped<IInvestmentMapper, InvestmentMapper>();
        serviceCollection.AddScoped<IInvestmentService, InvestmentService>();
        
        _serviceProvider = serviceCollection.BuildServiceProvider();
        Context = _serviceProvider.GetRequiredService<FinancePlannerContext>();
        Service = _serviceProvider.GetRequiredService<IInvestmentService>();

        await Context.Database.EnsureCreatedAsync();
    }
    
    public async Task DisposeAsync()
    {
        await _connection.CloseAsync();
        await Context.DisposeAsync();
        await _serviceProvider.DisposeAsync();
    }
}