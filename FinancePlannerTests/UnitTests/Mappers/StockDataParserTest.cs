using FinancePlanner.Mappers;
using FinancePlanner.Models.Stocks;
using JetBrains.Annotations;
using JsonException = System.Text.Json.JsonException;

namespace FinancePlannerTests.UnitTests.Mappers;

[TestSubject(typeof(StockDataParser))]
public class StockDataParserTest
{

    [Fact]
    public void ParseStockData_ReturnsExpectedStockData()
    {
        StockDataParser parser = new StockDataParser();
        string json = """
                      {
                      "Meta Data": {
                          "1. Information": "Intraday (5min) open, high, low, close prices and volume",
                          "2. Symbol": "IBM",
                          "3. Last Refreshed": "2026-08-13 19:55:00",
                          "4. Interval": "5min",
                          "5. Output Size": "Compact",
                          "6. Time Zone": "US/Eastern"
                      },
                      "Time Series (5min)": {
                          "2026-08-13 19:55:00": {
                              "1. open": "236.3600",
                              "2. high": "236.5000",
                              "3. low": "236.3531",
                              "4. close": "236.4800",
                              "5. volume": "1071"
                          },
                          "2026-08-13 19:50:00": {
                              "1. open": "236.4200",
                              "2. high": "236.5000",
                              "3. low": "236.3200",
                              "4. close": "236.3436",
                              "5. volume": "723"
                          }
                      }
                      }
                      """;
        StockData expected = new StockData()
        {
            MetaData = new MetaData(){
                Information = "Intraday (5min) open, high, low, close prices and volume",
                Symbol = "IBM",
                LastRefreshed = "2026-08-13 19:55:00",
                Interval = "5min",
                OutputSize = "Compact",
                TimeZone = "US/Eastern"
            },
            TimeSeries = new Dictionary<string, Values>()
            {
                { "2026-08-13 19:55:00", new Values()
                    {
                        Open = 236.3600m,
                        High = 236.5000m,
                        Low = 236.3531m,
                        Close = 236.4800m,
                        Volume = 1071
                    }
                },
                { "2026-08-13 19:50:00", new Values()
                    {
                        Open = 236.4200m,
                        High = 236.5000m,
                        Low = 236.3200m,
                        Close = 236.3436m,
                        Volume = 723
                    }
                }
            }
        };
        
        var result = parser.ParseStockData(json);

        if (result != null)
        {
            Assert.True(result.MetaData == expected.MetaData);
            Assert.True(result.TimeSeries.OrderBy(kvp => kvp.Key)
                .SequenceEqual(expected.TimeSeries.OrderBy(kvp => kvp.Key)));
        }
        else 
        {
            Assert.Fail("Result is null");
        }
    }

    [Fact]
    public void ParseStockData_ReturnsNullForEmptyJson()
    {
        StockDataParser parser = new StockDataParser();
        string json = "";

        var result = parser.ParseStockData(json);

        Assert.Null(result);
    }
    
    [Fact]
    public void ParseStockData_throwsExceptionForInvalidJson()
    {
        StockDataParser parser = new StockDataParser();
        string json = "{ invalid json }";
        
        Assert.Throws<JsonException>(() => parser.ParseStockData(json));
    }
}