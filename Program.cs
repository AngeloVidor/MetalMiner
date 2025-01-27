using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MetalMiner.Infra.Interfaces;
using MetalMiner.Infra.Services;
using MetalMiner.Infra;
using MetalMiner.Infra.Services.Songsterr;
using MetalMiner.Infra.Interfaces.Songsterr;
using System.Net.Http;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddScoped<IMetallumService, MetallumService>()
            .AddScoped<IUrlService, UrlService>()
            .AddScoped<ITablatureHandler, TablatureHandler>()
            .AddScoped<ISearchEngine, SearchEngine>()
            .AddSingleton<HttpClient>()
            .BuildServiceProvider();

        var metallumService = serviceProvider.GetRequiredService<IMetallumService>();
        var urlService = serviceProvider.GetRequiredService<IUrlService>();
        var tablatureHandler = serviceProvider.GetRequiredService<ITablatureHandler>();
        var searchEngine = serviceProvider.GetRequiredService<ISearchEngine>();

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        string art = @"
------------------------------------------------------------------------------------------------------------------------------------------
                       |    Sillenius    |                                                 
------------------------------------------------------------------------------------------------------------------------------------------

            |\         This program was made for study and entertainment purposes          
   \`-. _.._| \        This program was made for study and entertainment purposes
    |_,'  __`. \       This program was made for study and entertainment purposes
    (.\ _/.| _  |      This program was made for study and entertainment purposes
   ,'      __ \ |      This program was made for study and entertainment purposes
 ,'     __/||\  |      This program was made for study and entertainment purposes
(Y8P  ,/|||||/  |      This program was made for study and entertainment purposes
   `-'_----    /       This program was made for study and entertainment purposes
      /`-._.-'/        This program was made for study and entertainment purposes
      `-.__.-' jg      This program was made for study and entertainment purposes

------------------------------------------------------------------------------------------------------------------------------------------
>";
        Console.WriteLine(art);
        Console.WriteLine(">");
        Console.WriteLine("-----------Menu------------");
        System.Console.WriteLine((">"));
        Console.WriteLine("1. Find Stuff");
        Console.WriteLine("2. Grab Band_ID");
        Console.WriteLine("3. Check Out Band Profile");
        Console.WriteLine("4. Pull Band_ID from a URL");
        Console.WriteLine("5. Get the Band's Discography");
        Console.WriteLine("6. Fetch Album IDs");
        Console.WriteLine("7. Get the Tracks from an Album");
        Console.WriteLine("8. Download a Tab");
        Console.WriteLine("9. Tab Search Engine");
        Console.WriteLine("10. Get Tab URLs");
        System.Console.WriteLine("11. Get Tabs ID by Tab URLs");
        Console.WriteLine(">");
        Console.WriteLine("-----------End------------");
        Console.ResetColor();


        int input = int.Parse(Console.ReadLine());
        SwitchMenu menu = new SwitchMenu(urlService, metallumService, tablatureHandler, searchEngine);
        await menu.ExecuteSwitch(input);
    }
}
