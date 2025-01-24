using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using metallumscraper.Infra.Interfaces;
using metallumscraper.Infra.Services;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddScoped<IMetallumService, MetallumService>()  
            .BuildServiceProvider();

        var metallumService = serviceProvider.GetRequiredService<IMetallumService>();

        Console.WriteLine("Insert the band name:");
        string bandName = Console.ReadLine();

        Console.WriteLine("Insert the band genre:");
        string genre = Console.ReadLine();

        string url = await metallumService.BuildBandJsonSearchUrlAsync(bandName, genre);
        Console.WriteLine($"SearchUrl: {url}");
    }
}
