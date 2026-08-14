namespace FinancePlanner.Background;

public class ApiPollingWorker: BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            string apiUrl = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=TSCO.LON&outputsize=full&apikey=demo";
            using var client = new HttpClient()
            {
                
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred in ApiPollingWorker: {ex.Message}");
        }
        
        await Task.Delay(TimeSpan.FromMinutes(5),stoppingToken);
    }
}