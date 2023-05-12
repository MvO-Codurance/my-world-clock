using AutoFixture.Xunit2;
using Microsoft.Azure.Functions.Isolated.TestDoubles;
using Microsoft.Azure.Functions.Isolated.TestDoubles.Extensions;
using Moq;

namespace MyWorldClock.Functions.Tests;

public class LanguagesShould
{
    [Theory]
    [InlineAutoMoqData]
    public async Task Call_LanguageService_GetLanguages(
        Language[] expectedLanguages,
        [Frozen] Mock<ILanguageService> languageService,
        Languages sut)
    {
        languageService.Setup(x => x.GetLanguages()).Returns(expectedLanguages);
        var request = MockHelpers.CreateHttpRequestData();
        
        var response = await sut.GetLanguages(request, request.FunctionContext);

        response.IsHttpResponseBodyEmpty().Should().BeFalse();
        languageService.Verify(x => x.GetLanguages(), Times.Once);
    }
    
    [Theory]
    [InlineAutoMoqData]
    public async Task Return_Serialised_Languages(
        Language[] expectedLanguages,
        [Frozen] Mock<ILanguageService> languageService,
        Languages sut)
    {
        languageService.Setup(x => x.GetLanguages()).Returns(expectedLanguages);
        var request = MockHelpers.CreateHttpRequestData();
        
        var response = await sut.GetLanguages(request, request.FunctionContext);

        response.IsHttpResponseBodyEmpty().Should().BeFalse();
        var actualTimezones = response.ReadHttpResponseDataAsJson<Language[]>();
        actualTimezones.Should().BeEquivalentTo(expectedLanguages);
    }
}