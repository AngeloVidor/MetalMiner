﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using metallumscraper.Infra.Interfaces;
using metallumscraper.Infra.Services;
using metallumscraper.Infra;
using metallumscraper.Infra.Services.Songsterr;
using metallumscraper.Infra.Interfaces.Songsterr;

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
        Console.WriteLine("1. Advance Search");
        Console.WriteLine("2. Get Band_ID");
        Console.WriteLine("3. Get Band Profile");
        Console.WriteLine("4. Extract Band_ID from URL");
        Console.WriteLine("5. Get Band Discography By Band_ID");
        Console.WriteLine("6. Get Album_Ids By Band_ID");
        Console.WriteLine("7. Get Album Songs");
        Console.WriteLine("8. Download Tab");
        Console.WriteLine("9. Tab Search Engine");
        Console.WriteLine("-----------End------------");
        Console.ResetColor();

        int input = int.Parse(Console.ReadLine());
        SwitchMenu menu = new SwitchMenu(urlService, metallumService, tablatureHandler, searchEngine);
        await menu.ExecuteSwitch(input);
    }
}
