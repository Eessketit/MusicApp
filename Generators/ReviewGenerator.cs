using Bogus;

namespace MusicApp.Generators;

public static class ReviewGenerator
{
    public static string Generate(string locale, ulong seed)
    {
        Randomizer.Seed = new Random(SeedHelper.ToInt32(seed));
        var faker = new Faker(locale);

        return faker.Lorem.Sentences(2);
    }
}