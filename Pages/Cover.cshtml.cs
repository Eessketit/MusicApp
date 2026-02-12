using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicApp.Generators;
using System.Text;
using System.Security;

namespace MusicApp.Pages;

public class CoverModel : PageModel
{
    public IActionResult OnGet(ulong seed, string title, string artist)
    {
        var rng = new Random(SeedHelper.ToInt32(seed));

        int r = rng.Next(40, 200);
        int g = rng.Next(40, 200);
        int b = rng.Next(40, 200);

        string bg = $"rgb({r},{g},{b})";

        string safeTitle = SecurityElement.Escape(title);
        string safeArtist = SecurityElement.Escape(artist);

        var svg = $"""
        <svg xmlns="http://www.w3.org/2000/svg" width="400" height="400">
            <defs>
                <linearGradient id="grad" x1="0" y1="0" x2="1" y2="1">
                    <stop offset="0%" stop-color="{bg}" />
                    <stop offset="100%" stop-color="black" />
                </linearGradient>
            </defs>

            <rect width="100%" height="100%" fill="url(#grad)" />

            <text x="20" y="270"
                  font-size="22"
                  fill="white"
                  font-weight="bold"
                  font-family="Arial, sans-serif">
                {safeTitle}
            </text>

            <text x="20" y="320"
                  font-size="14"
                  fill="white"
                  font-family="Arial, sans-serif">
                {safeArtist}
            </text>
        </svg>
        """;

        return Content(svg, "image/svg+xml", Encoding.UTF8);
    }
}