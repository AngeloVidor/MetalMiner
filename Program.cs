using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using scrap;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=> ******* Welcome! ******* <=");
        Console.WriteLine("1. Search for a band");
        Console.WriteLine("2. Discography of a band");
        Console.WriteLine("3. Get band Id");
        int input = int.Parse(Console.ReadLine());
        Console.WriteLine($"Input: {input}");
        Console.WriteLine("-------------Menu-Ends-------------");

        Menu menu = new Menu();
        await menu.DisplayMenu(input);

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





// </div>

// <div id="band_content">
//         <div class="tool_strip right writeAction">
//             <ul>
//                                                         <li><a title="Report a mistake or additional information for this page" href="javascript:popupReportDialog(1, 29);" class="btn_report_error"> </a></li>
//                             </ul>
//         </div>

//         <div id="band_info">

//                         <h1 class="band_name"><a href="https://www.metal-archives.com/bands/Summoning/29">Summoning</a></h1>


//                                 <div class="clear block_spacer_5"></div>