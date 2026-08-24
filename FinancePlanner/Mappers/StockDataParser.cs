using System.Text.Json;
using System.Text.Json.Serialization;
using FinancePlanner.Models.Stocks;

namespace FinancePlanner.Mappers;

public class StockDataParser : IStockDataParser
{
    public StockData? ParseStockData(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }
        var options = new JsonSerializerOptions { NumberHandling = JsonNumberHandling.AllowReadingFromString }; 
        return JsonSerializer.Deserialize<StockData>(json, options);
    }
}