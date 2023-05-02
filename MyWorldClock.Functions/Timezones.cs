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
    
    [Function("timezones/all")]
    public async Task<HttpResponseData> GetAllTimezones(
        [HttpTrigger(AuthorizationLevel.Function, "get")] 
        HttpRequestData request,
        FunctionContext executionContext)
    {
        var response = request.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(_clockService.GetAllTimezones());

        return response;
    }
    
    [Function("timezones/for-display")]
    public async Task<HttpResponseData> GetTimezonesForDisplay(
        [HttpTrigger(AuthorizationLevel.Function, "get")] 
        HttpRequestData request,
        FunctionContext executionContext)
    {
        var response = request.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(_clockService.GetTimezonesForDisplay());

        return response;
    }
}