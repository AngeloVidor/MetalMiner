using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using metallumscraper.Infra.Data;
using metallumscraper.Infra.Interfaces;
using scrap;

namespace metallumscraper.Infra.Services
{
    public class MetallumService : IMetallumService
    {
        private readonly IUrlService _urlService;

        public MetallumService(IUrlService urlService)
        {
            _urlService = urlService;
        }

        public Task<string> BuildBandJsonSearchUrlAsync(string name, string genre)
        {
            string baseUrl = "https://www.metal-archives.com/search/ajax-advanced/searching/bands/";
            string nameParam = Uri.EscapeDataString(name);
            string genreParam = Uri.EscapeDataString(genre);
            string url = $"{baseUrl}?bandName={nameParam}&genre={genreParam}&country=&yearCreationFrom=&yearCreationTo=&bandNotes=&status=&themes=&location=&bandLabelName=&sEcho=1&iColumns=3&sColumns=&iDisplayStart=0&iDisplayLength=200&mDataProp_0=0&mDataProp_1=1&mDataProp_2=2";
            return Task.FromResult(url);
        }

        public async Task<string> GetBandIdAsync(string bandName)
        {
            var searchUrl = await _urlService.GetUrlBandOccurrencesAsync(bandName);

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(searchUrl);
            //Console.WriteLine(response);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(response);

            var bandNodes = doc.DocumentNode.SelectNodes("//a");

            List<string> bandIds = new List<string>();

            if (bandNodes != null)
            {
                foreach (var node in bandNodes)
                {
                    string bandUrl = node.Attributes["href"].Value;
                    if (bandUrl.Contains("/bands/") && bandUrl.Split('/').Last().All(char.IsDigit))
                    {
                        string responseName = node.InnerText.Trim();
                        if (responseName.Equals(bandName, StringComparison.OrdinalIgnoreCase))
                        {
                            string bandId = bandUrl.Split('/').Last();
                            bandIds.Add(bandId);
                            Console.WriteLine($"Band: {bandName}");
                            Console.WriteLine($"Band ID: {bandId}");
                            return bandId;
                        }
                    }
                }
            }
            Console.WriteLine("No band IDs found.");
            return "0";
        }
    }
}