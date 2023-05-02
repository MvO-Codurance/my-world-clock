using NodaTime;

namespace MyWorldClock;

public record struct WorldClock(DateTimeZone Timezone, Instant Instant, ZonedDateTime ZonedDateTime, LocalDateTime LocalDateTime);