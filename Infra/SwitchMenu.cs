using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using metallumscraper.Infra.Interfaces;

namespace metallumscraper.Infra
{
    public class SwitchMenu
    {
        private readonly IUrlService _urlService;
        private readonly IMetallumService _metallumService;
        private string name;

        public SwitchMenu(IUrlService urlService, IMetallumService metallumService)
        {
            _urlService = urlService;
            _metallumService = metallumService;
        }

        public async Task ExecuteSwitch(int input)
        {
            switch (input)
            {
                case 1:
                    Console.WriteLine("1. Advance Search | Ajax Search Response");

                    Console.WriteLine("Band name:");
                    name = Console.ReadLine();

                    Console.WriteLine("Band genre:");
                    string genre = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Green;

                    if (input == 1)
                    {
                        var advanceSearcUrl = await _urlService.AdvanceSearchUrlAsync(name, genre);
                        Console.WriteLine(advanceSearcUrl);
                    }
                    Console.ResetColor();
                    break;
                case 2:
                    Console.WriteLine("2. Get band ID | String Response");

                    Console.WriteLine("Band name:");
                    name = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (input == 2)
                    {
                        long bandId = await _metallumService.GetBandIdAsync(name);
                        Console.WriteLine(bandId);
                    }
                    Console.ResetColor();
                    break;
                case 3:
                    Console.WriteLine("3. Get Band Profile | String Response");
                    Console.WriteLine("Band name:");
                    name = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (input == 3)
                    {
                        string profile = await _metallumService.GetBandsProfilesUrlsAsync(name);
                        Console.WriteLine(profile);
                    }
                    Console.ResetColor();
                    break;
                case 4:
                    Console.WriteLine("4. Extract Band ID from URL | String Response");

                    Console.WriteLine("Band name:");
                    name = Console.ReadLine();

                    var url = await _metallumService.GetBandsProfilesUrlsAsync(name);
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (input == 4)
                    {
                        string extractedId = await _urlService.ExtractBandIdFromUrlAsync(url);
                        Console.WriteLine(extractedId);
                    }
                    Console.ResetColor();

                    break;
                case 5:
                    Console.WriteLine("5. Get Band Discography By Band ID | String Response");
                    Console.WriteLine("Band name:");
                    name = Console.ReadLine();


                    long band_id = await _metallumService.GetBandIdAsync(name);
                    var discography = await _metallumService.GetBandDiscographyByBandIdAsync(band_id);
                    Console.ForegroundColor = ConsoleColor.Green;
                    foreach (var album in discography)
                    {
                        System.Console.WriteLine(album);
                    }
                    Console.ResetColor();

                    break;

            }
        }
    }
}