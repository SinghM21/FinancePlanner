using System.Collections.Concurrent;
using FinancePlanner.Mappers;
using FinancePlanner.Models.Stocks;

namespace FinancePlanner.Services;

public class StockService : IStockService
{
    private ConcurrentDictionary<string, decimal> _stockValues = new();
    private readonly HttpClient _httpClient = new();
    private readonly string _apiUrl = "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=IBM&interval=5min&apikey=demo";
    private readonly IStockDataParser _stockDataParser;

    public StockService(IStockDataParser stockDataParser)
    {
        _stockDataParser = stockDataParser;
    }
    
    public async Task UpdateStockValuesAsync(CancellationToken stoppingToken)
    {
        var response = await _httpClient.GetAsync(_apiUrl, stoppingToken);
        response.EnsureSuccessStatusCode();
        
        var json = await response.Content.ReadAsStringAsync(stoppingToken);
        StockData? stockData = _stockDataParser.ParseStockData(json);
        
        if (stockData != null)
        {
            SetStockValue("IBM", stockData.TimeSeries.First().Value.Close + (decimal)Random.Shared.NextDouble()); // Randomly adjust the stock value for demonstration
        }
    }
    
    private void SetStockValue(string stockSymbol, decimal stockValue)
    {
        _stockValues[stockSymbol] = stockValue;
    }

    public decimal? GetStockValue(string stockSymbol)
    {
        if (_stockValues.TryGetValue(stockSymbol, out var value))
        {
            return value;
        }
        return null;
    }
}