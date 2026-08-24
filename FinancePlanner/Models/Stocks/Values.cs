using System.Text.Json.Serialization;

namespace FinancePlanner.Models.Stocks;

public record class Values
{
    [JsonPropertyName("1. open")]
    public required decimal Open { get; set; }
    [JsonPropertyName("2. high")]
    public required decimal High { get; set; }
    [JsonPropertyName("3. low")]
    public required decimal Low { get; set; }
    [JsonPropertyName("4. close")]
    public required decimal Close { get; set; }
    [JsonPropertyName("5. volume")]
    public required long Volume { get; set; }
}