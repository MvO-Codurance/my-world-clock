using NodaTime;

namespace MyWorldClock;

public interface IClockService
{
    DateTimeZone TimeZone { get; }

    Instant Now { get; }

    LocalDateTime LocalNow { get; }

    Instant? ToInstant(LocalDateTime? local);

    LocalDateTime? ToLocal(Instant? instant);

    IEnumerable<string> GetAllTimezones();

    IEnumerable<TimezoneForDisplay> GetTimezonesForDisplay(string languageCode);
    
    IEnumerable<TimezoneForDisplay> GetTimezonesForDisplay();
}