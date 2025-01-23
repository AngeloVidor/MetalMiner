using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using scrap;

class Program
{
    static async Task Main(string[] args)
    {
        CollectData data = new CollectData();
        await data.SearchForDataAsync("Spectral Lament", "Atmospheric Black Metal");
                
    }
}

// string url = "https://www.metal-archives.com/bands/Summoning/29";

//         HttpClient client = new HttpClient();
//         string htmlContent = await client.GetStringAsync(url);

//         HtmlDocument doc = new HtmlDocument();
//         doc.LoadHtml(htmlContent);

//         var bandStatsNode = doc.DocumentNode.SelectSingleNode("//div[@id='band_stats']");
//         var bandInfoNode = doc.DocumentNode.SelectSingleNode("//div[@id='band_info']");

//         var terms = bandStatsNode.SelectNodes(".//dt");
//         var descriptions = bandStatsNode.SelectNodes(".//dd");
//         var bandInfo = bandInfoNode.SelectNodes(".//h1").First().InnerText.Trim();

//         if (terms == null || descriptions == null || bandInfo == null)
//         {
//             throw new Exception();
//         }

//         Console.WriteLine("Band Stats");
//         Console.WriteLine($"Band: {bandInfo}");
//         for (int i = 0; i < terms.Count; i++)
//         {
//             string term = terms[i].InnerText.Trim();
//             string description = descriptions[i].InnerText.Trim();
//             Console.WriteLine($"{term} {description}");
//         }


