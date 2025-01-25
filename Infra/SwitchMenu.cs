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
            switch (input)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("1. Advance Search");
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
                    Console.WriteLine("2. Get Band_ID");
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
                    Console.WriteLine("3. Get Band Profile");
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
                    Console.WriteLine("4. Extract Band ID from URL");
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
                    Console.WriteLine("5. Get Band Discography By Band ID");
                    Console.ResetColor();

                    Console.WriteLine("Band name:");
                    name = Console.ReadLine();

                    band_Id = await _metallumService.GetBandIdAsync(name);
                    var discography = await _metallumService.GetBandDiscographyByBandIdAsync(band_Id);

                    Console.ForegroundColor = ConsoleColor.Green;
                    foreach (var album in discography)
                    {
                        Console.WriteLine($"Name: {album.album_name}");
                        System.Console.WriteLine("------------------");
                        Console.WriteLine($"Band_ID: {album.band_id}");
                        System.Console.WriteLine("------------------");
                        Console.WriteLine("Album_IDs: " + string.Join(", ", album.album_ids));

                    }


                    Console.ResetColor();
                    break;

                case 6:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("6. Get Album Songs By Album_ID");
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
                case 7:
                    System.Console.WriteLine("Band name:");
                    name = Console.ReadLine();
                    band_Id = await _metallumService.GetBandIdAsync(name);

                    var albumIds = await _metallumService.GetAlbumIdsByBandIdAsync(band_Id);
                    foreach (var albumId in albumIds)
                    {
                        System.Console.WriteLine($"ID: {albumId}");
                    }

                    Console.WriteLine("Enter the album ID to view its songs:");
                    long song_albumId = long.Parse(Console.ReadLine());

                    await _metallumService.GetAlbumSongsByAlbumIdAsync(song_albumId);

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
