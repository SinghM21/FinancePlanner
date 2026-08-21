using System.Text.Json;
using FinancePlanner.Services;

namespace FinancePlanner.Background;

public class ApiPollingWorker: BackgroundService
{
    private readonly IStockService _stockService;

    public ApiPollingWorker(IStockService stockService)
    {
        _stockService = stockService;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await _stockService.UpdateStockValuesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred in ApiPollingWorker: {ex.Message}");
        }
        
        await Task.Delay(TimeSpan.FromMinutes(5),stoppingToken);
    }
}