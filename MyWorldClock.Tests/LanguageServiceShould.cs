namespace MyWorldClock.Tests;

public class LanguageServiceShould
{
    [Fact]
    public void Return_The_List_Of_Supported_Language_Codes()
    {
        var sut = new LanguageService();

        var actualCodes = sut.GetLanguages().ToList();

        var expectedCount = 41;
        actualCodes.Should().HaveCount(expectedCount);

        var expectedFirstLanguage = new Language(
            "ar-SA",
            "Arabic (Saudi Arabia) / العربية (المملكة العربية السعودية)",
            "Arabic (Saudi Arabia)",
            "العربية (المملكة العربية السعودية)");
        actualCodes[0].Should().BeEquivalentTo(expectedFirstLanguage);
        
        var expectedLastLanguage = new Language(
            "vi-VN",
            "Vietnamese (Vietnam) / Tiếng Việt (Việt Nam)",
            "Vietnamese (Vietnam)",
            "Tiếng Việt (Việt Nam)");
        actualCodes[expectedCount - 1].Should().BeEquivalentTo(expectedLastLanguage);
    }
}