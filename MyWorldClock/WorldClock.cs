using NodaTime;

namespace MyWorldClock;

public record struct WorldClock(string Timezone, Instant Instant, ZonedDateTime ZonedDateTime, LocalDateTime LocalDateTime);