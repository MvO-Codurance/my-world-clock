using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace MyWorldClock.Functions;

public class Languages
{
    private readonly ILanguageService _languageService;

    public Languages(ILanguageService languageService)
    {
        _languageService = languageService;
    }
    
    [Function("languages")]
    public async Task<HttpResponseData> GetLanguages(
        [HttpTrigger(AuthorizationLevel.Function, "get")] 
        HttpRequestData request,
        FunctionContext executionContext)
    {
        var response = request.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(_languageService.GetLanguages());

        return response;
    }
}