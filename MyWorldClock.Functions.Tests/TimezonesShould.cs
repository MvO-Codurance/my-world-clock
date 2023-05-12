using AutoFixture.Xunit2;
using Microsoft.Azure.Functions.Isolated.TestDoubles;
using Microsoft.Azure.Functions.Isolated.TestDoubles.Extensions;
using Moq;

namespace MyWorldClock.Functions.Tests;

public class TimezonesShould
{
    [Theory]
    [InlineAutoMoqData]
    public async Task Call_ClockService_GetAllTimezones(
        string[] expectedTimezones,
        [Frozen] Mock<IClockService> clockService,
        Timezones sut)
    {
        clockService.Setup(x => x.GetAllTimezones()).Returns(expectedTimezones);
        var request = MockHelpers.CreateHttpRequestData();
        
        var response = await sut.GetAllTimezones(request, request.FunctionContext);

        response.Should().NotBeNull();
        clockService.Verify(x => x.GetAllTimezones(), Times.Once);
    }
    
    [Theory]
    [InlineAutoMoqData]
    public async Task Return_Serialised_AllTimezones(
        string[] expectedTimezones,
        [Frozen] Mock<IClockService> clockService,
        Timezones sut)
    {
        clockService.Setup(x => x.GetAllTimezones()).Returns(expectedTimezones);
        var request = MockHelpers.CreateHttpRequestData();
        
        var response = await sut.GetAllTimezones(request, request.FunctionContext);

        response.Should().NotBeNull();
        var actualTimezones = response.ReadHttpResponseDataAsJson<string[]>();
        actualTimezones.Should().BeEquivalentTo(expectedTimezones);
    }
    
    [Theory]
    [InlineAutoMoqData]
    public async Task Call_ClockService_GetTimezonesForDisplay_Without_Language(
        TimezoneForDisplay[] expectedTimezones,
        [Frozen] Mock<IClockService> clockService,
        Timezones sut)
    {
        clockService.Setup(x => x.GetTimezonesForDisplay()).Returns(expectedTimezones);
        var request = MockHelpers.CreateHttpRequestData();
        
        var response = await sut.GetTimezonesForDisplay(
            request, 
            language: null, 
            request.FunctionContext);

        response.Should().NotBeNull();
        clockService.Verify(x => x.GetTimezonesForDisplay(), Times.Once);
    }
    
    [Theory]
    [InlineAutoMoqData]
    public async Task Call_ClockService_GetTimezonesForDisplay_With_Language(
        TimezoneForDisplay[] expectedTimezones,
        string language,
        [Frozen] Mock<IClockService> clockService,
        Timezones sut)
    {
        clockService.Setup(x => x.GetTimezonesForDisplay(It.IsAny<string>())).Returns(expectedTimezones);
        var request = MockHelpers.CreateHttpRequestData();
        
        var response = await sut.GetTimezonesForDisplay(
            request, 
            language: language, 
            request.FunctionContext);

        response.Should().NotBeNull();
        clockService.Verify(x => x.GetTimezonesForDisplay(language), Times.Once);
    }
    
    [Theory]
    [InlineAutoMoqData(null!)]
    [InlineAutoMoqData("en-GB")]
    public async Task Return_Serialised_TimezonesForDisplay(
        string language,
        TimezoneForDisplay[] expectedTimezones,
        [Frozen] Mock<IClockService> clockService,
        Timezones sut)
    {
        clockService.Setup(x => x.GetTimezonesForDisplay()).Returns(expectedTimezones);
        clockService.Setup(x => x.GetTimezonesForDisplay(It.IsAny<string>())).Returns(expectedTimezones);
        var request = MockHelpers.CreateHttpRequestData();
        
        var response = await sut.GetTimezonesForDisplay(
            request, 
            language: language, 
            request.FunctionContext);

        response.Should().NotBeNull();
        var actualTimezones = response.ReadHttpResponseDataAsJson<TimezoneForDisplay[]>();
        actualTimezones.Should().BeEquivalentTo(expectedTimezones);
    }
}