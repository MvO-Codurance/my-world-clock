using NodaTime;
using NodaTime.Extensions;
using TimeZoneNames;

namespace MyWorldClock;

public class WorldClockService : IWorldClockService
{
    private readonly IClock _clock;

    public WorldClockService(IClock clock)
    {
        _clock = clock;
    }
    
    public IEnumerable<WorldClock> GetClocks(
        string language,
        string[] timezoneIds)
    {
        List<WorldClock> worldClocks = new(timezoneIds.Length);

        foreach (string timezoneId in timezoneIds)
        {
            var timezone = DateTimeZoneProviders.Tzdb.GetZoneOrNull(timezoneId);
            if (timezone == null)
            {
                continue;
            }
             
            var zonedClock = _clock.InZone(timezone);

            worldClocks.Add(
                new WorldClock(
                    Timezone: TZNames.GetDisplayNameForTimeZone(timezone.Id, language) ?? timezone.Id,
                    Instant: _clock.GetCurrentInstant(),
                    ZonedDateTime: zonedClock.GetCurrentZonedDateTime(),
                    LocalDateTime: zonedClock.GetCurrentLocalDateTime())
            );
        }

        return worldClocks;
    }
}