using Bogus;
using System.Text;
using MusicApp.Models;

namespace MusicApp.Generators;

public class SongContentGenerator
{
    private readonly string _lang;
    private readonly Faker _faker;

    private static readonly Dictionary<string, string> FakerLocales = new()
    {
        ["en"] = "en",
        ["ru"] = "ru"
    };

    private static readonly string[] EnglishTitleWords =
    {
        "Midnight", "Fire", "Shadow", "Light",
        "Dream", "Storm", "Echo", "Waves",
        "Silence", "Sky", "Gravity", "Flame"
    };

    private static readonly string[] RussianTitleWords =
    {
        "Ночь", "Огонь", "Тень", "Свет",
        "Мечта", "Буря", "Эхо", "Волны",
        "Тишина", "Небо", "Сила", "Пламя"
    };

    private static readonly Dictionary<string, string[]> TitleTemplates = new()
    {
        ["en"] = new[]
        {
            "{0}",
            "{0} of {1}",
            "The {0}",
            "{0} and {1}"
        },
        ["ru"] = new[]
        {
            "{0}",
            "{0} и {1}",
            "Только {0}",
            "{0} внутри"
        }
    };

    public SongContentGenerator(string lang)
    {
        _lang = string.IsNullOrWhiteSpace(lang)
            ? "en"
            : lang.Trim().ToLower();

        if (!FakerLocales.ContainsKey(_lang))
            _lang = "en";

        _faker = new Faker(FakerLocales[_lang]);
    }

    public Song Generate(int index, ulong seed)
    {
        Randomizer.Seed = new Random((int)(seed % int.MaxValue));

        return new Song
        {
            Index = index,
            Title = GenerateTitle(),
            Artist = _faker.Name.FullName(),
            Album = GenerateAlbum(),
            Genre = _faker.Music.Genre(),
            Lyrics = GenerateLyrics()
        };
    }

    private string GenerateTitle()
    {
        var template = _faker.PickRandom(TitleTemplates[_lang]);

        var words = _lang == "ru"
            ? RussianTitleWords
            : EnglishTitleWords;

        return string.Format(
            template,
            _faker.PickRandom(words),
            _faker.PickRandom(words)
        );
    }

    private string GenerateAlbum()
    {
        var word = _lang == "ru"
            ? _faker.PickRandom(RussianTitleWords)
            : _faker.PickRandom(EnglishTitleWords);

        return _lang == "ru"
            ? $"Альбом «{word}»"
            : $"Album \"{word}\"";
    }

    private string GenerateLyrics()
    {
        var sb = new StringBuilder();

        AppendSection(sb, 1);
        AppendSection(sb, 2);

        return sb.ToString().Trim();
    }

    private void AppendSection(StringBuilder sb, int verse)
    {
        sb.AppendLine(_lang == "ru" ? $"Куплет {verse}" : $"Verse {verse}");
        sb.AppendLine(GenerateLines(4));
        sb.AppendLine();

        sb.AppendLine(_lang == "ru" ? "Припев" : "Chorus");
        sb.AppendLine(GenerateLines(2, repeatLast: true));
        sb.AppendLine();
    }

    private string GenerateLines(int count, bool repeatLast = false)
    {
        var sb = new StringBuilder();
        string? lastLine = null;

        for (int i = 0; i < count; i++)
        {
            var line = GenerateLyricLine();
            sb.AppendLine(line);
            lastLine = line;
        }

        if (repeatLast && lastLine != null)
            sb.AppendLine(lastLine);

        return sb.ToString().TrimEnd();
    }

    private string GenerateLyricLine()
    {
        if (_lang == "ru")
        {
            var templates = new[]
            {
                "{emotion} {noun} начинает {verb}",
                "Мы {verb} сквозь {noun}",
                "Твой {noun} делает меня {emotion}",
                "Я слышу {noun} в тишине",
                "Сегодня мы будем {verb}"
            };

            return ApplyTemplate(_faker.PickRandom(templates));
        }
        else
        {
            var templates = new[]
            {
                "I feel {emotion} when the {noun} starts to {verb}",
                "We {verb} through the {noun}",
                "Your {noun} makes me feel {emotion}",
                "I hear the {noun} in the night",
                "Tonight we will {verb}"
            };

            return ApplyTemplate(_faker.PickRandom(templates));
        }
    }

    private string ApplyTemplate(string template)
    {
        return template
            .Replace("{emotion}", _faker.Hacker.Adjective())
            .Replace("{noun}", _faker.Hacker.Noun())
            .Replace("{verb}", _faker.Hacker.Verb());
    }
}