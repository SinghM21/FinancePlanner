using FinancePlanner.Contexts;
using FinancePlanner.DTOs;
using FinancePlanner.Mappers;
using FinancePlanner.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public class InvestmentIntegrationTests : IAsyncLifetime
{
    private FinancePlannerContext _context;
    private IInvestmentService _service;

    private ServiceProvider _serviceProvider;
    private SqliteConnection _connection;

    public async Task InitializeAsync()
    {
        var serviceCollection = new ServiceCollection();

        _connection = new SqliteConnection("Filename=:memory:");
        await _connection.OpenAsync();
        
        // Mimic app setup
        serviceCollection.AddDbContext<FinancePlannerContext>(options =>
            options.UseSqlite(_connection));
        serviceCollection.AddScoped<IInvestmentMapper, InvestmentMapper>();
        serviceCollection.AddScoped<IInvestmentService, InvestmentService>();

        _serviceProvider = serviceCollection.BuildServiceProvider();
        _context = _serviceProvider.GetRequiredService<FinancePlannerContext>();
        _service = _serviceProvider.GetRequiredService<IInvestmentService>();

        await _context.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
        await _context.DisposeAsync();
        await _serviceProvider.DisposeAsync();
    }

    [Fact]
    public async Task CreateInvestment_CreatesDatabaseRecord()
    {
        //Arrange
        var investmentDto = new InvestmentDto { Name = "Investment Test", Description = "Test Description" };

        // Act
        await _service.CreateInvestmentAsync(investmentDto);

        // Assert
        var createdInvestment = _context.Investment.FirstOrDefault(i => i.Name == "Investment Test");
        Assert.NotNull(createdInvestment);
        Assert.Equal(investmentDto.Name, createdInvestment.Name);
        Assert.Equal(investmentDto.Description, createdInvestment.Description);
    }

    [Fact]
    public async Task UpdateInvestment_UpdatesDatabaseRecord()
    {
        //Arrange
        var investmentDto = new InvestmentDto { Name = "Investment Test", Description = "Test Description" };
        await _service.CreateInvestmentAsync(investmentDto);
        var updatedInvestmentDto = new InvestmentDto
            { Name = "Updated Investment", Description = "Updated Description" };

        // Act
        var createdInvestment = _context.Investment.First(i => i.Name == "Investment Test");
        await _service.UpdateInvestmentAsync(createdInvestment.ID, updatedInvestmentDto);

        // Assert
        var updatedInvestment = await _context.Investment.FindAsync(createdInvestment.ID);
        Assert.NotNull(updatedInvestment);
        Assert.Equal(updatedInvestmentDto.Name, updatedInvestment.Name);
        Assert.Equal(updatedInvestmentDto.Description, updatedInvestment.Description);
    }

    [Fact]
    public async Task DeleteInvestment_RemovesDatabaseRecord()
    {
        //Arrange
        var investmentDto = new InvestmentDto { ID = 1, Name = "Investment Test", Description = "Test Description" };
        await _service.CreateInvestmentAsync(investmentDto);

        // Act
        bool investmentDeleted = await _service.DeleteInvestmentAsync(investmentDto.ID);

        // Assert
        Assert.True(investmentDeleted);
        await using (var freshContext = CreateFreshContext())
        {
            var deletedInvestment = await freshContext.Investment.FindAsync(investmentDto.ID);
            Assert.Null(deletedInvestment);
        }
    }
    
    private FinancePlannerContext CreateFreshContext()
    {
        return new FinancePlannerContext(
            new DbContextOptionsBuilder<FinancePlannerContext>()
                .UseSqlite(_connection)
                .Options);
    }
}