namespace MusicApp.Generators;

public static class CoverGenerator
{
    public static string BuildUrl(
        ulong seed,
        string title,
        string artist)
    {
        return $"/Cover?seed={seed}&title={Uri.EscapeDataString(title)}&artist={Uri.EscapeDataString(artist)}";
    }
}