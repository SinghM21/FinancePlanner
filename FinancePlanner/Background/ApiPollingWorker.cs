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
        await DoWork(stoppingToken);
    }
    
    private async Task DoWork(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _stockService.UpdateStockValuesAsync(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred in ApiPollingWorker: {ex.Message}");
        }
    }
}