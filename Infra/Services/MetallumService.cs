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

        public async Task<string> GetBandDiscographyByBandIdAsync(int bandId)
        {
            string discographyUrl = $"https://www.metal-archives.com/band/discography/id/{bandId}/tab/all";
            HttpClient http = new HttpClient();
            var disco = await http.GetStringAsync(discographyUrl);

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(disco);

            var discographyNodes = html.DocumentNode.SelectNodes("//table//a");
            if (discographyNodes != null && discographyNodes.Count > 0)
            {
                foreach (var node in discographyNodes)
                {
                    var albumLink = node.Attributes["href"].Value;
                    Console.WriteLine(albumLink);
                }
            }
            return discographyUrl;
        }

        public async Task<string> GetBandIdAsync(string bandName)
        {
            var searchUrl = await _urlService.GetUrlAllBandsOccurrencesAsync(bandName);

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

        public async Task<string> GetBandsProfilesUrlsAsync(string name)
        {
            var bandOccurrencesUrl = await _urlService.GetUrlAllBandsOccurrencesAsync(name);

            HttpClient http = new HttpClient();
            string htmlContent = await http.GetStringAsync(bandOccurrencesUrl);
            //Console.WriteLine(htmlContent);

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(htmlContent);

            var namesNodes = html.DocumentNode.SelectNodes("//div[@id='content_wrapper']//ul/li/a/@href");

            if (namesNodes != null)
            {
                foreach (var node in namesNodes)
                {
                    string bandUrl = node.Attributes["href"].Value;
                    return bandUrl;
                }
            }
            else
            {
                Console.WriteLine("namesNodes is null or empty");
            }
            return name;
        }
    }
}