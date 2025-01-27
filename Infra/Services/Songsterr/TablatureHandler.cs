using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MetalMiner.Infra.Interfaces;
using MetalMiner.Infra.Interfaces.Songsterr;
using Microsoft.Playwright;

namespace MetalMiner.Infra.Services
{
    public class TablatureHandler : ITablatureHandler
    {
        private readonly ISearchEngine _searchEngine;
        public TablatureHandler(ISearchEngine searchEngine)
        {
            _searchEngine = searchEngine;
        }
        public async Task<string> PrintAllVersesAsync(string tabUrl)
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            var page = await browser.NewPageAsync();
            string baseUrl = $"https://www.songsterr.com{tabUrl}";
            await page.GotoAsync(baseUrl);

            await page.Locator("//*[@id='tablature']").WaitForAsync();

            //blocking ads
            await page.EvaluateAsync(@"() => {
                const adSection = document.querySelector('#showroom');
                if (adSection) {
                    adSection.style.display = 'none';
                }
            }");

            var elements = await page.Locator("//*[@id='tablature']//div[@data-player-key='tab']").ElementHandlesAsync();

            string tabId = _searchEngine.ExtractTabIdByTabUrlAsync(tabUrl);

            string folderPath = @$"C:\Users\angel\Desktop\MetalMiner\{tabId}";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }


            int index = 1;
            foreach (var element in elements)
            {
                await element.ScrollIntoViewIfNeededAsync();
                await page.WaitForTimeoutAsync(500);

                await element.ScreenshotAsync(new ElementHandleScreenshotOptions
                {
                    Path = Path.Combine(folderPath, $"player_key_screenshot_{index}.png")
                });
                Console.WriteLine($"Screenshot of element {index} saved in folder {tabId}");
                index++;
            }

            return $"Captured {index - 1} images!";
        }
    }
}