using MusicApp.Models;

namespace MusicApp.Generators;

public static class AudioGenerator
{
    private static readonly string[] Samples =
    {
        "/audio/sample1.mp3",
        "/audio/sample2.mp3",
        "/audio/sample3.mp3",
        "/audio/sample4.mp3",
        "/audio/sample5.mp3"
    };

    public static AudioTrack Generate(ulong seed)
    {
        int index = SeedHelper.ToInt32(seed) % Samples.Length;
        return new AudioTrack { Url = Samples[index] };
    }
}