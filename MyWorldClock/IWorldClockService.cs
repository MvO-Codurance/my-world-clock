namespace MyWorldClock;

public interface IWorldClockService
{
    IEnumerable<WorldClock> GetClocks(string language, string[] timezoneIds);
}