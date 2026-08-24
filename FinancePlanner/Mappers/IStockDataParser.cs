using FinancePlanner.Models.Stocks;

namespace FinancePlanner.Mappers;

public interface IStockDataParser
{
    public StockData? ParseStockData(string json);
}