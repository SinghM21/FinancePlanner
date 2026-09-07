namespace FinancePlanner.Services;

public interface IStockService
{
    public Task UpdateStockValuesAsync(CancellationToken stoppingToken);
    public decimal? GetStockValue(string stockSymbol);
}