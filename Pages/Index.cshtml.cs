using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicApp.Generators;
using MusicApp.Models;

namespace MusicApp.Pages;

public class IndexModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string Lang { get; set; } = "en";

    [BindProperty(SupportsGet = true)]
    public ulong Seed { get; set; } = 1;

    [BindProperty(SupportsGet = true)]
    public double Likes { get; set; } = 1.2;

    [BindProperty(SupportsGet = true)]
    public int PageNumber { get; set; } = 1;

    [BindProperty(SupportsGet = true)]
    public string View { get; set; } = "table";

    public int PageSize { get; } = 10;

    public List<Song> Songs { get; set; } = new();

    public void OnGet()
    {
        Lang = (Lang ?? "en").Trim().ToLower();
        Lang = Lang == "ru" ? "ru" : "en";

        if (PageNumber < 1)
            PageNumber = 1;

        int effectivePage = PageNumber;

        if (View == "gallery" && !Request.Headers.ContainsKey("X-Requested-With"))
        {
            effectivePage = 1;
            PageNumber = 1;
        }

        ulong contentSeed = SeedHelper.Hash(Seed, Lang, effectivePage);
        ulong likesSeed = SeedHelper.Hash(Seed, "likes", effectivePage);
        ulong audioSeed = SeedHelper.Hash(Seed, "audio", effectivePage);

        var contentGenerator = new SongContentGenerator(Lang);

        for (int i = 0; i < PageSize; i++)
        {
            int index = (effectivePage - 1) * PageSize + i + 1;

            ulong recordContentSeed = SeedHelper.Hash(contentSeed, index);
            ulong recordLikesSeed = SeedHelper.Hash(likesSeed, index);

            var song = contentGenerator.Generate(index, recordContentSeed);
            song.Likes = LikesGenerator.Generate(Likes, recordLikesSeed);

            ulong recordAudioSeed = SeedHelper.Hash(audioSeed, index);
            song.Audio = AudioGenerator.Generate(recordAudioSeed);

            Songs.Add(song);
        }
    }
}