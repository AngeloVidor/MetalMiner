using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetalMiner.Infra.Interfaces;
using MetalMiner.Infra.Interfaces.Songsterr;

namespace MetalMiner.Infra
{
    public class SwitchMenu
    {
        private readonly IUrlService _urlService;
        private readonly IMetallumService _metallumService;
        private readonly ITablatureHandler _tablatureHandler;
        private readonly ISearchEngine _searchEngine;
        private string name;
        private long band_Id;

        public SwitchMenu(IUrlService urlService, IMetallumService metallumService, ITablatureHandler tablatureHandler, ISearchEngine searchEngine)
        {
            _urlService = urlService;
            _metallumService = metallumService;
            _tablatureHandler = tablatureHandler;
            _searchEngine = searchEngine;
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
                        Console.WriteLine($"Album Name: {album.album_name}");
                        Console.WriteLine($"Band ID: {album.band_id}");
                        Console.WriteLine($"Album ID: {album.album_id}");
                        Console.WriteLine($"Album URL: {album.album_url}");
                        Console.WriteLine(new string('-', 30));

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

                    System.Console.WriteLine("Choose an album");
                    var discos = await _metallumService.GetBandDiscographyByBandIdAsync(band_Id);
                    Console.ForegroundColor = ConsoleColor.Green;
                    foreach (var disco in discos)
                    {
                        System.Console.WriteLine($"Album Name: {disco.album_name}");
                        System.Console.WriteLine($"Album ID: {disco.album_id}");
                        Console.WriteLine("--------------------------");
                    }
                    Console.ResetColor();

                    Console.WriteLine("Enter the album ID to view its songs:");
                    long song_albumId = long.Parse(Console.ReadLine());

                    var selectedAlbum = discos.FirstOrDefault(d => d.album_id == song_albumId);
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (selectedAlbum != null)
                    {
                        var songs = await _metallumService.GetAlbumSongsByAlbumIdAsync(song_albumId, selectedAlbum.album_name, selectedAlbum.album_name);
                        foreach (var song in songs)
                        {
                            System.Console.WriteLine(song);
                        }
                    }
                    Console.ResetColor();
                    break;

                case 8:
                    await _tablatureHandler.TakeScreenshotAsync();
                    break;

                case 9:
                    Console.WriteLine("Enther the band name:");
                    //name = Console.ReadLine();

                    Console.WriteLine("Enther the song name:");
                    //string song_name = Console.ReadLine();

                    //var response = await _searchEngine.GetTabASync(name, song_name);
                    //System.Console.WriteLine(response);
                    break;

                case 10:
                    Console.WriteLine("Enther the band name:");
                    name = Console.ReadLine();

                    Console.WriteLine("Enther the song name:");
                    string song_name = Console.ReadLine();

                    var urls = await _searchEngine.GetTabsByFilterAsync(name, song_name);
                    foreach (var tabUrl in urls)
                    {
                        System.Console.WriteLine(tabUrl);

                    }
                    break;

                case 11:
                    Console.WriteLine("Enther the band name:");
                    name = Console.ReadLine();

                    Console.WriteLine("Enther the song name:");
                    string songName = Console.ReadLine();

                    Console.ForegroundColor = ConsoleColor.Green;
                    var tabs = await _searchEngine.GetTabsByFilterAsync(name, songName);
                    foreach (var tab in tabs)
                    {
                        System.Console.WriteLine(tab);
                    }
                    Console.ResetColor();

                    Console.WriteLine("Now choose a URL: ");
                    var chosenUrl = Console.ReadLine();
                    var id = _searchEngine.ExtractTabIdByTabUrlAsync(chosenUrl);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Here is your ID: {id}");
                    Console.ResetColor();
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid option! Try again.");
                    Console.ResetColor();
                    break;
            }
        }

    }
}
