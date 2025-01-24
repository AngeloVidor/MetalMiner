using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using metallumscraper.Infra.Interfaces;
using metallumscraper.Infra.Services;
using metallumscraper.Infra;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddScoped<IMetallumService, MetallumService>()
            .AddScoped<IUrlService, UrlService>()
            .AddSingleton<HttpClient>()
            .BuildServiceProvider();

        var metallumService = serviceProvider.GetRequiredService<IMetallumService>();
        var urlService = serviceProvider.GetRequiredService<IUrlService>();

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        string asciiArt = @"
------------------------------------------------------------------------------------------------------------------------------------------
                                                            ABOUT THE SYSTEM
------------------------------------------------------------------------------------------------------------------------------------------
███████████████████████████ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
███████▀▀▀░░░░░░░▀▀▀███████ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
████▀░░░░░░░░░░░░░░░░░▀████ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
███│░░░░░░░░░░░░░░░░░░░│███ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
██▌│░░░░░░░░░░░░░░░░░░░│▐██ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
██░└┐░░░░░░░░░░░░░░░░░┌┘░██ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
██░░└┐░░░░░░░░░░░░░░░┌┘░░██ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
██░░┌┘▄▄▄▄▄░░░░░▄▄▄▄▄└┐░░██ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
██▌░│██████▌░░░▐██████│░▐██ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
███░│▐███▀▀░░▄░░▀▀███▌│░███ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
██▀─┘░░░░░░░▐█▌░░░░░░░└─▀██ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
██▄░░░▄▄▄▓░░▀█▀░░▓▄▄▄░░░▄██ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
████▄─┘██▌░░░░░░░▐██└─▄████ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
█████░░▐█─┬┬┬┬┬┬┬─█▌░░█████ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
████▌░░░▀┬┼┼┼┼┼┼┼┬▀░░░▐████ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
█████▄░░░└┴┴┴┴┴┴┴┘░░░▄█████ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
███████▄░░░░░░░░░░░▄███████ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
██████████▄▄▄▄▄▄▄██████████ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
███████████████████████████ This program was made for study and entertainment purposes. None of the content flowing through here will be sold or monetized in any way.
Developed by {!!Sillenius!!}
------------------------------------------------------------------------------------------------------------------------------------------";
Console.WriteLine(asciiArt);

        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("-----------Menu------------");
        Console.WriteLine("1. Advance Search | Ajax Response");
        Console.WriteLine("2. Get Band_ID | String Response");
        Console.WriteLine("3. Get Band Profile | String Response");
        Console.WriteLine("4. Extract Band_ID from URL | String Response");
        Console.WriteLine("5. Get Band Discography By Band_ID | String Response");
        Console.WriteLine("6. Get Album Songs By Album_ID | String Response");
        Console.WriteLine("-----------End------------");
        Console.ResetColor();

        int input = int.Parse(Console.ReadLine());
        SwitchMenu menu = new SwitchMenu(urlService, metallumService);
        await menu.ExecuteSwitch(input);
    }
}
