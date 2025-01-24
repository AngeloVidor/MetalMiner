﻿using System;
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
            .BuildServiceProvider();

        var metallumService = serviceProvider.GetRequiredService<IMetallumService>();
        var urlService = serviceProvider.GetRequiredService<IUrlService>();


        Console.WriteLine("1. Advance Search | Ajax Response");
        Console.WriteLine("2. Get band ID | String Response");
        int input = int.Parse(Console.ReadLine());
        SwitchMenu menu = new SwitchMenu(urlService, metallumService);
        await menu.ExecuteSwitch(input);
    }
}
