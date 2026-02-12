using Bogus;

namespace MusicApp.Generators;

public static class FakeDataGenerator
{
    public static Faker Create(string locale, ulong seed)
    {
        Randomizer.Seed = new Random(SeedHelper.ToInt32(seed));
        return new Faker(locale);
    }
}