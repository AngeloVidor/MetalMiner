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
                    string name = Console.ReadLine();

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
                    string bandName = Console.ReadLine();
                    if (input == 2)
                    {
                        string bandId = await _metallumService.GetBandIdAsync(bandName);
                        Console.WriteLine(bandId);
                    }
                    break;

            }
        }
    }
}