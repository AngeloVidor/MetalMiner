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
        private long band_Id;

        public SwitchMenu(IUrlService urlService, IMetallumService metallumService)
        {
            _urlService = urlService;
            _metallumService = metallumService;
        }

        public async Task ExecuteSwitch(int input)
        {
            // Cabeçalho
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----------- Menu de Opções -----------");
            Console.ResetColor();

            switch (input)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("1. Advance Search | Ajax Search Response");
                    Console.ResetColor();

                    Console.WriteLine("Band name:");
                    name = Console.ReadLine();

                    Console.WriteLine("Band genre:");
                    string genre = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Green;

                    var advanceSearcUrl = await _urlService.AdvanceSearchUrlAsync(name, genre);
                    Console.WriteLine(advanceSearcUrl);
                    Console.ResetColor();
                    break;

                case 2:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("2. Get Band ID | String Response");
                    Console.ResetColor();

                    Console.WriteLine("Band name:");
                    name = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Green;

                    long bandId = await _metallumService.GetBandIdAsync(name);
                    Console.WriteLine(bandId);
                    Console.ResetColor();
                    break;

                case 3:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("3. Get Band Profile | String Response");
                    Console.ResetColor();

                    Console.WriteLine("Band name:");
                    name = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Green;

                    string profile = await _metallumService.GetBandsProfilesUrlsAsync(name);
                    Console.WriteLine(profile);
                    Console.ResetColor();
                    break;

                case 4:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("4. Extract Band ID from URL | String Response");
                    Console.ResetColor();

                    Console.WriteLine("Band name:");
                    name = Console.ReadLine();

                    var url = await _metallumService.GetBandsProfilesUrlsAsync(name);
                    Console.ForegroundColor = ConsoleColor.Green;

                    string extractedId = await _urlService.ExtractBandIdFromUrlAsync(url);
                    Console.WriteLine(extractedId);
                    Console.ResetColor();
                    break;

                case 5:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("5. Get Band Discography By Band ID | String Response");
                    Console.ResetColor();

                    Console.WriteLine("Band name:");
                    name = Console.ReadLine();

                    band_Id = await _metallumService.GetBandIdAsync(name);
                    var discography = await _metallumService.GetBandDiscographyByBandIdAsync(band_Id);
                    Console.ForegroundColor = ConsoleColor.Green;

                    foreach (var album in discography)
                    {
                        Console.WriteLine(album);
                    }
                    Console.ResetColor();
                    break;

                case 6:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("6. Get Album Songs By Album ID | String Response");
                    Console.ResetColor();

                    Console.WriteLine("Band name:");
                    name = Console.ReadLine();

                    band_Id = await _metallumService.GetBandIdAsync(name);
                    var discoIds = await _metallumService.GetAlbumIdsByBandIdAsync(band_Id);
                    Console.ForegroundColor = ConsoleColor.Green;

                    foreach (var disco in discoIds)
                    {
                        Console.WriteLine(disco);
                    }
                    Console.ResetColor();
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    Console.ResetColor();
                    break;
            }
        }

    }
}
