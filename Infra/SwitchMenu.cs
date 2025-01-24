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
                    Console.WriteLine("1. Advance Search | Ajax Response");

                    Console.WriteLine("Band name:");
                    name = Console.ReadLine();

                    Console.WriteLine("Band genre:");
                    string genre = Console.ReadLine();

                    if (input == 1)
                    {
                        var advanceSearcUrl = await _urlService.AdvanceSearchUrlAsync(name, genre);
                        Console.WriteLine(advanceSearcUrl);
                    }
                    break;
                case 2:
                    Console.WriteLine("2. Get band ID | String Response");

                    Console.WriteLine("Band name:");
                    name = Console.ReadLine();
                    if (input == 2)
                    {
                        string bandId = await _metallumService.GetBandIdAsync(name);
                        Console.WriteLine(bandId);
                    }
                    break;
                case 3:
                    Console.WriteLine("3. Get Band Profile | String Response");
                    Console.WriteLine("Band name:");
                    name = Console.ReadLine();
                    if (input == 3)
                    {
                        string profile = await _metallumService.GetBandsProfilesUrlsAsync(name);
                        Console.WriteLine(profile);
                    }
                    break;

                case 4:
                    Console.WriteLine("4. Extract Band ID from URL | String Response");

                    Console.WriteLine("Band name:");
                    name = Console.ReadLine();

                    var url = await _metallumService.GetBandsProfilesUrlsAsync(name);
                    if (input == 4)
                    {
                        string extractedId = await _urlService.ExtractBandIdFromUrlAsync(url);
                        Console.WriteLine(extractedId);
                    }
                    break;

            }
        }
    }
}