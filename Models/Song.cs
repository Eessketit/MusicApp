namespace MusicApp.Models;

public class Song
{
    public int Index { get; set; }

    public string Title { get; set; } = "";
    public string Artist { get; set; } = "";
    public string Album { get; set; } = "";
    public string Genre { get; set; } = "";

    public int Likes { get; set; }
    public string Review { get; set; } = "";
    public string Lyrics { get; set; } = "";
    public AudioTrack Audio { get; set; } = new();
    public string CoverUrl =>
            $"/images/covers/sample{((Index - 1) % CoverCount) + 1}.jpg";
    private const int CoverCount = 13;


}