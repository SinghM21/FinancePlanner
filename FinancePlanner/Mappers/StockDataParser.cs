using FinancePlanner.Background;

namespace FinancePlanner.Mappers;

public class StockDataParser : IStockDataParser
{
    public StockData? ParseStockData(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }
        
        return System.Text.Json.JsonSerializer.Deserialize<StockData>(json);
    }
}