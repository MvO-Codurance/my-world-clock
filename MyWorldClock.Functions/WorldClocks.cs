using System.Globalization;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace MyWorldClock.Functions;

public class WorldClocks
{
    private readonly IWorldClockService _worldClockService;

    public WorldClocks(IWorldClockService worldClockService)
    {
        _worldClockService = worldClockService;
    }
    
    [Function(nameof(GetClocks))]
    public async Task<HttpResponseData> GetClocks(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "worldclocks/{language?}")] 
        HttpRequestData request,
        string? language,
        FunctionContext executionContext)
    {
        var response = request.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(_worldClockService.GetClocks(
            string.IsNullOrWhiteSpace(language) ? CultureInfo.CurrentUICulture.Name : language,
            new []
            {
                "America/Los_Angeles",
                "America/New_York",
                "Europe/London",
                "Europe/Paris",
                "Europe/Rome",
                "Asia/Tokyo"    
            }));

        return response;
    }
}