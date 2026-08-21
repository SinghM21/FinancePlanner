using FinancePlanner.Background;

namespace FinancePlanner.Mappers;

public interface IStockDataParser
{
    public StockData? ParseStockData(string json);
}