using System.Globalization;
using System.Text.Json;
using AutoFixture.Xunit2;
using Azure.Core.Serialization;
using Microsoft.Azure.Functions.Isolated.TestDoubles;
using Microsoft.Azure.Functions.Isolated.TestDoubles.Extensions;
using Moq;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

namespace MyWorldClock.Functions.Tests;

public class WorldClocksShould
{
    [Theory]
    [InlineAutoMoqData]
    public async Task Call_WorldClockService_GetClocks_WithoutLanguage(
        WorldClock[] expectedClocks,
        [Frozen] Mock<IWorldClockService> worldClockService,
        WorldClocks sut)
    {
        worldClockService.Setup(x => x.GetClocks(It.IsAny<string>(), It.IsAny<string[]>()))
            .Returns(expectedClocks);
        var request = MockHelpers.CreateHttpRequestData();
        
        var response = await sut.GetClocks(
            request, 
            language: null, 
            request.FunctionContext);

        response.IsHttpResponseBodyEmpty().Should().BeFalse();
        worldClockService.Verify(x => x.GetClocks(CultureInfo.CurrentUICulture.Name, It.IsAny<string[]>()), Times.Once);
    }
    
    [Theory]
    [InlineAutoMoqData]
    public async Task Call_WorldClockService_GetClocks_With_Language(
        string language,
        WorldClock[] expectedClocks,
        [Frozen] Mock<IWorldClockService> worldClockService,
        WorldClocks sut)
    {
        worldClockService.Setup(x => x.GetClocks(It.IsAny<string>(), It.IsAny<string[]>()))
            .Returns(expectedClocks);
        var request = MockHelpers.CreateHttpRequestData();
        
        var response = await sut.GetClocks(
            request, 
            language: language, 
            request.FunctionContext);

        response.IsHttpResponseBodyEmpty().Should().BeFalse();
        worldClockService.Verify(x => x.GetClocks(language, It.IsAny<string[]>()), Times.Once);
    }
    
    [Theory]
    [InlineAutoMoqData(null!)]
    [InlineAutoMoqData("en-GB")]
    public async Task Return_Serialised_TimezonesForDisplay(
        string language,
        WorldClock[] expectedClocks,
        [Frozen] Mock<IWorldClockService> worldClockService,
        WorldClocks sut)
    {
        worldClockService.Setup(x => x.GetClocks(It.IsAny<string>(), It.IsAny<string[]>()))
            .Returns(expectedClocks);

        // ensure the JSON serializer can handle NodaTime types correctly
        var jsonSerializerOptions = new JsonSerializerOptions();
        jsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
        jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        var objectSerializer = new JsonObjectSerializer(jsonSerializerOptions);
        
        var request = MockHelpers.CreateHttpRequestData(objectSerializer: objectSerializer);
        
        var response = await sut.GetClocks(
            request, 
            language: language, 
            request.FunctionContext);
    
        response.IsHttpResponseBodyEmpty().Should().BeFalse();
        var actualClocks = response.ReadHttpResponseDataAsJson<WorldClock[]>(jsonSerializerOptions);
        actualClocks.Should().BeEquivalentTo(expectedClocks);
    }
}