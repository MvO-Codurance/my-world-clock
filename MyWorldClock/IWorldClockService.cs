namespace MyWorldClock;

public interface IWorldClockService
{
    IEnumerable<WorldClock> GetClocks(params string[] timezoneIds);
}