using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using metallumscraper.Infra.Interfaces;
using Microsoft.Playwright;

namespace metallumscraper.Infra.Services
{
    public class TablatureHandler : ITablatureHandler
    {
        public TablatureHandler()
        {

        }

        public async Task<string> TakeScreenshotAsync()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true
            });

            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://playwright.dev/dotnet");
            var screenshot = await page.ScreenshotAsync(new()
            {
                Path = @"C:\Users\angel\Desktop\metallum-scraper/screenshot.png"
            });

            if(screenshot == null)
            {
                System.Console.WriteLine("Screenshot not taken");
            }
            

            return "1";



        }
    }
}