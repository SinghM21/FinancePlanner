using FinancePlanner.DTOs;
using FinancePlannerTests.IntegrationTests;

public class InvestmentIntegrationTests : IClassFixture<InvestmentFixture>
{
    private InvestmentFixture _fixture;
    
    public InvestmentIntegrationTests(InvestmentFixture fixture)
    {
        this._fixture = fixture;
    }
    
    [Fact]
    public async Task CreateInvestment_PersistsToDatabase()
    {
        var investmentDto = new InvestmentDto { ID = 1, Name = "Investment Test", Description = "Test Description" };

        // Act
        await _fixture.Service.CreateInvestmentAsync(investmentDto);

        // Assert
        var persisted = await _fixture.Context.Investment.FindAsync(investmentDto.ID);
        Assert.NotNull(persisted);
        Assert.Equal(investmentDto.Name, persisted.Name);
        Assert.Equal(investmentDto.Description, persisted.Description);
    }
}