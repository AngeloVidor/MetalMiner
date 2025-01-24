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
        private readonly HttpClient _httpClient;
        private List<string> albumLinks = new List<string>();

        public MetallumService(IUrlService urlService, HttpClient httpClient)
        {
            _urlService = urlService;
            _httpClient = httpClient;
        }

        public Task<string> BuildBandJsonSearchUrlAsync(string name, string genre)
        {
            string baseUrl = "https://www.metal-archives.com/search/ajax-advanced/searching/bands/";
            string nameParam = Uri.EscapeDataString(name);
            string genreParam = Uri.EscapeDataString(genre);
            string url = $"{baseUrl}?bandName={nameParam}&genre={genreParam}&country=&yearCreationFrom=&yearCreationTo=&bandNotes=&status=&themes=&location=&bandLabelName=&sEcho=1&iColumns=3&sColumns=&iDisplayStart=0&iDisplayLength=200&mDataProp_0=0&mDataProp_1=1&mDataProp_2=2";
            return Task.FromResult(url);
        }

        public async Task<List<string>> GetBandDiscographyByBandIdAsync(long bandId)
        {
            string discographyUrl = $"https://www.metal-archives.com/band/discography/id/{bandId}/tab/all";
            var disco = await _httpClient.GetStringAsync(discographyUrl);

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(disco);

            var discographyNodes = html.DocumentNode.SelectNodes("//table//a");
            if (discographyNodes != null && discographyNodes.Count > 0)
            {
                foreach (var node in discographyNodes)
                {
                    var albumLink = node.Attributes["href"].Value;
                    albumLinks.Add(albumLink);
                }
            }
            return albumLinks;
        }

        public async Task<long> GetBandIdAsync(string bandName)
        {
            var searchUrl = await _urlService.GetUrlAllBandsOccurrencesAsync(bandName);

            var response = await _httpClient.GetStringAsync(searchUrl);
            //Console.WriteLine(response);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(response);

            var bandNodes = doc.DocumentNode.SelectNodes("//a");

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
                            long longId = long.Parse(bandId);

                            Console.WriteLine($"Band: {bandName}");
                            Console.WriteLine($"Band ID: {bandId}");
                            return longId;
                        }
                    }
                }
            }
            Console.WriteLine("No band IDs found.");
            return 0;
        }

        public async Task<string> GetBandsProfilesUrlsAsync(string name)
        {
            var bandOccurrencesUrl = await _urlService.GetUrlAllBandsOccurrencesAsync(name);

            string htmlContent = await _httpClient.GetStringAsync(bandOccurrencesUrl);
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