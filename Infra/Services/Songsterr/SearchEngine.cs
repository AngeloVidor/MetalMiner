using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using metallumscraper.Infra.Interfaces.Songsterr;
using Microsoft.Playwright;

namespace metallumscraper.Infra.Services.Songsterr
{
    public class SearchEngine : ISearchEngine
    {
        private readonly HttpClient _httpClient;

        public SearchEngine(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        //https://songsterr.com/a/wsa/metallica-seek-and-destroy-standard-d-tab-s464241

        public async Task<string> GetTabASync(string band_name, string song_name)
        {
            {
                if (!string.IsNullOrEmpty(band_name) && band_name.Contains(" "))
                {
                    band_name = band_name.Replace(" ", "%20");
                }

                if (!string.IsNullOrEmpty(song_name) && song_name.Contains(" "))
                {
                    song_name = song_name.Replace(" ", "%20");
                }

                using var playwright = await Playwright.CreateAsync();
                await using var browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = true
                });

                var page = await browser.NewPageAsync();
                string baseUrl = $"https://www.songsterr.com/?pattern={band_name}%20{song_name}";
                await page.GotoAsync(baseUrl);

                await page.Locator("//*[@data-list='songs']").WaitForAsync();
                var elements = await page.Locator("//*[@data-list='songs']//a").ElementHandlesAsync();

                int index = 1;
                foreach (var element in elements)
                {
                    string href = await element.GetAttributeAsync("href");
                    Console.WriteLine(href);
                }
                index++;


                return baseUrl;
            }
        }
    }
}