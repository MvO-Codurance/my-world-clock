namespace Microsoft.Azure.Functions.Isolated.TestDoubles.Extensions;

public static class StreamExtensions
{
    public static bool IsEmpty(this Stream stream)
    {
        // length < 3 to account for the BOM (Byte Order Mark) for UTF-8 streams 
        return stream?.Length < 3;
    }
}