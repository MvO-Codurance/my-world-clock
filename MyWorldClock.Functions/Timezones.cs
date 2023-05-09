using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace MyWorldClock.Functions;

public class Timezones
{
    private readonly IClockService _clockService;

    public Timezones(IClockService clockService)
    {
        _clockService = clockService;
    }
    
    [Function(nameof(GetAllTimezones))]
    public async Task<HttpResponseData> GetAllTimezones(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "timezones/all")] 
        HttpRequestData request,
        FunctionContext executionContext)
    {
        var response = request.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(_clockService.GetAllTimezones());

        return response;
    }
    
    [Function(nameof(GetTimezonesForDisplay))]
    public async Task<HttpResponseData> GetTimezonesForDisplay(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "timezones/for-display/{language?}")] 
        HttpRequestData request,
        string? language,
        FunctionContext executionContext)
    {
        var response = request.CreateResponse(HttpStatusCode.OK);
        var timezones = string.IsNullOrWhiteSpace(language) ? _clockService.GetTimezonesForDisplay() : _clockService.GetTimezonesForDisplay(language);
        await response.WriteAsJsonAsync(timezones);

        return response;
    }
}