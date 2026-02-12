namespace MusicApp.Generators;

public static class LikesGenerator
{
    public static int Generate(double avgLikes, ulong seed)
    {
        if (avgLikes <= 0) return 0;
        if (avgLikes >= 10) return 10;

        int baseLikes = (int)Math.Floor(avgLikes);
        double probability = avgLikes - baseLikes;

        var rng = new Random(SeedHelper.ToInt32(seed));
        return baseLikes + (rng.NextDouble() < probability ? 1 : 0);
    }
}