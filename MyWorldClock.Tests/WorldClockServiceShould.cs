namespace MyWorldClock.Tests;

public class WorldClockServiceShould
{
    [Theory]
    [InlineData("en-GB", "Europe/London", "(UTC+00:00) Dublin, Edinburgh, Lisbon, London")]
    [InlineData("en-GB", "Europe/Istanbul", "(UTC+03:00) Istanbul")]
    [InlineData("fr-FR", "Europe/London", "(UTC+00:00) Dublin, Édimbourg, Lisbonne, Londres")]
    [InlineData("fr-FR", "Europe/Istanbul", "(UTC+03:00) Istanbul")]
    public void Resolve_The_Correct_Single_Timezone(
        string language, 
        string timezoneId, 
        string displayName)
    {
        var sut = GetWorldClockService();

        var actualClocks = sut.GetClocks(language, new []{ timezoneId }).ToList();

        actualClocks.Should().HaveCount(1);
        
        var worldClock = actualClocks[0]; 
        worldClock.Should().NotBeNull();
        worldClock.Timezone.Should().Be(displayName);
    }
    
    [Theory]
    [InlineData("en-GB", new []{ "Europe/London", "Europe/Istanbul" }, new []{ "(UTC+00:00) Dublin, Edinburgh, Lisbon, London", "(UTC+03:00) Istanbul" })]
    [InlineData("fr-FR", new []{ "Europe/London", "Europe/Istanbul" }, new []{ "(UTC+00:00) Dublin, Édimbourg, Lisbonne, Londres", "(UTC+03:00) Istanbul" })]
    public void Resolve_The_Correct_Multiple_Timezones(
        string language, 
        string[] timezoneIds,
        string[] displayNames)
    {
        var sut = GetWorldClockService();

        var actualClocks = sut.GetClocks(language, timezoneIds).ToList();

        actualClocks.Should().HaveCount(timezoneIds.Length);

        var londonClock = actualClocks[0]; 
        londonClock.Should().NotBeNull();
        londonClock.Timezone.Should().Be(displayNames[0]);
        
        var istanbulClock = actualClocks[1]; 
        istanbulClock.Should().NotBeNull();
        istanbulClock.Timezone.Should().Be(displayNames[1]);
    }
    
    [Theory]
    [InlineData("en-GB", "Europe/London", "2022-11-20T10:30:00Z", "2022-11-20T10:30:00 Europe/London (+00)", "20/11/2022 10:30:00")]
    [InlineData("en-GB", "Europe/Istanbul", "2022-11-20T10:30:00Z", "2022-11-20T13:30:00 Europe/Istanbul (+03)", "20/11/2022 13:30:00")]
    public void Resolve_The_Correct_Times_For_A_Single_Timezone(
        string language,
        string timezoneId,
        string instantDatetime,
        string zonedDateTime,
        string localDateTime)
    {
        var sut = GetWorldClockService();
    
        var actualClocks = sut.GetClocks(language, new []{ timezoneId }).ToList();
    
        actualClocks.Should().HaveCount(1);
        
        var worldClock = actualClocks[0]; 
        worldClock.Should().NotBeNull();
        worldClock.Instant.ToString().Should().Be(instantDatetime);
        worldClock.ZonedDateTime.ToString().Should().Be(zonedDateTime);
        worldClock.LocalDateTime.ToString().Should().Be(localDateTime);
    }
    
    [Fact]
    public void Ignore_Unknown_Timezone_Ids()
    {
        var sut = GetWorldClockService();

        var actualClocks = sut.GetClocks("en-GB", new [] { "not a valid timezone id" }).ToList();

        actualClocks.Should().HaveCount(0);
    }
    
    private static WorldClockService GetWorldClockService()
    {
        var clock = new FakeClock(Instant.FromUtc(2022, 11, 20, 10, 30));
        return new WorldClockService(clock);
    }
}