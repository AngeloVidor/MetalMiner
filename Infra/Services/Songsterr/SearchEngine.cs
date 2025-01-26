using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MetalMiner.Infra.Interfaces.Songsterr;
using Microsoft.Playwright;

namespace MetalMiner.Infra.Services.Songsterr
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




        public async Task<IEnumerable<string>> GetTabsByFilterAsync(string band_name, string song_name)
        {
            List<string> tabs = new List<string>();

            if (!string.IsNullOrEmpty(band_name) && band_name.Contains(" "))
            {
                band_name = band_name.Replace(" ", "%20");
            }

            if (!string.IsNullOrEmpty(song_name) && song_name.Contains(" "))
            {
                song_name = song_name.Replace(" ", "%20");
            }
            using (HttpClient client = new HttpClient())
            {
                string url = $"http://localhost:8050/render.html?url=https://www.songsterr.com/?pattern={band_name}%20{song_name}";

                try
                {
                    var htmlContent = await client.GetStringAsync(url);

                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(htmlContent);

                    var data_list_nodes = doc.DocumentNode.SelectNodes("//div[@data-list='songs']//a");
                    if (data_list_nodes != null)
                    {
                        foreach (var song in data_list_nodes)
                        {
                            var song_tab = song.Attributes["href"].Value;
                            tabs.Add(song_tab);
                            System.Console.WriteLine(song_tab);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error accessing content: {ex.Message}");
                }
            }
            return tabs;

        }
    }
}
