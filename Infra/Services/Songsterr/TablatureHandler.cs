using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MetalMiner.Infra.Interfaces;
using Microsoft.Playwright;

namespace MetalMiner.Infra.Services
{
    public class TablatureHandler : ITablatureHandler
    {
        public TablatureHandler()
        {

        }
        public async Task<string> TakeScreenshotAsync()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            string baseUrl = "https://www.songsterr.com/a/wsa/pantera-floods-tab-s84836";
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

            int index = 1;
            foreach (var element in elements)
            {
                Console.WriteLine($"Element screenshot {index}...");

                await element.ScrollIntoViewIfNeededAsync();
                await page.WaitForTimeoutAsync(500);

                await element.ScreenshotAsync(new ElementHandleScreenshotOptions
                {
                    Path = @$"C:\Users\angel\Desktop\metallum-scraper\player_key_screenshot_{index}.png"
                });
                Console.WriteLine($"Screenshot of elemnent {index} saved");
                index++;
            }

            return $"Captured {index - 1} images!";
        }




    }
}