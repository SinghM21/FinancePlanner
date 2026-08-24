using System.Text.Json.Serialization;

namespace FinancePlanner.Models.Stocks;

public record class StockData
{
    [JsonPropertyName("Meta Data")]
    public required MetaData MetaData {get; set;}
    [JsonPropertyName("Time Series (5min)")]
    public required Dictionary<string, Values> TimeSeries {get; set;}
}