using System.Text.Json.Serialization;

namespace FinancePlanner.Background;

public class StockData
{
    [JsonPropertyName("Meta Data")]
    public Dictionary<string, string> MetaData {get; set;}
    [JsonPropertyName("Time Series (5min)")]
    public Dictionary<string, Dictionary<string, string>> TimeSeries {get; set;}
}