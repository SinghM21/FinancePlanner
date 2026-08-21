namespace FinancePlanner.Services;

public interface IStockService
{
    public Task UpdateStockValuesAsync();
    public decimal? GetStockValue(string stockSymbol);
}