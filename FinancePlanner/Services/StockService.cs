using System.Text.Json;
using FinancePlanner.Background;
using FinancePlanner.Mappers;

namespace FinancePlanner.Services;

public class StockService : IStockService
{
    private Dictionary<string, decimal> _stockValues = new();
    private readonly HttpClient _httpClient = new();
    private readonly string _apiUrl = "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=IBM&interval=5min&apikey=demo";
    private readonly IStockDataParser _stockDataParser;

    public StockService(IStockDataParser stockDataParser)
    {
        _stockDataParser = stockDataParser;
    }
    
    public async Task UpdateStockValuesAsync()
    {
        var response = await _httpClient.GetAsync(_apiUrl);
        response.EnsureSuccessStatusCode();
            
        StockData? stockData = _stockDataParser.ParseStockData(await response.Content.ReadAsStringAsync());
        if (stockData != null)
        {
            SetStockValue("IBM", Decimal.Parse(stockData.TimeSeries.First().Value["4. close"]));
        }
    }
    
    private void SetStockValue(string stockSymbol, decimal stockValue)
    {
        _stockValues[stockSymbol] = stockValue;
    }

    public decimal? GetStockValue(string stockSymbol)
    {
        if (_stockValues.ContainsKey(stockSymbol))
        {
            return _stockValues[stockSymbol];
        }
        return null;
    }
}