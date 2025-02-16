using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using MetalMiner.Infra.Data;
using MetalMiner.Infra.Interfaces;
using scrap;

namespace MetalMiner.Infra.Services
{
    public class MetallumService : IMetallumService
    {
        private readonly IUrlService _urlService;
        private readonly HttpClient _httpClient;
        private List<AlbumData> albums = new List<AlbumData>();

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

        public async Task<List<AlbumData>> GetBandDiscographyByBandIdAsync(long bandId)
        {
            string discographyUrl = $"https://www.metal-archives.com/band/discography/id/{bandId}/tab/all";
            var disco = await _httpClient.GetStringAsync(discographyUrl);

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(disco);

            var albums = new List<AlbumData>();

            var discographyNodes = html.DocumentNode.SelectNodes("//table//a");
            if (discographyNodes != null && discographyNodes.Count > 0)
            {
                foreach (var node in discographyNodes)
                {
                    var url_album = node.Attributes["href"].Value;
                    var albumName = node.InnerText;

                    //ToDo: centralize parse in a single method
                    Uri uri = new Uri(url_album);
                    var pathSegments = uri.AbsolutePath.Split('/');

                    if (uri.AbsolutePath.StartsWith("/albums/"))
                    {
                        var albumIdStr = pathSegments.LastOrDefault();
                        if (long.TryParse(albumIdStr, out var albumId))
                        {
                            var formattedAlbumName = albumName.Replace(" ", "_");

                            var albumData = new AlbumData
                            {
                                band_id = bandId,
                                album_url = url_album,
                                album_name = formattedAlbumName,
                                album_id = albumId
                            };
                            albums.Add(albumData);
                        }
                    }
                }
            }
            return albums;
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

        public async Task<List<long>> GetAlbumIdsByBandIdAsync(long bandId)
        {
            List<long> albumIds = new List<long>();

            var discos = await GetBandDiscographyByBandIdAsync(bandId);
            foreach (var disco in discos)
            {
                var uri = new Uri(disco.album_url);
                var pathSegments = uri.AbsolutePath.Split('/');

                if (uri.AbsolutePath.StartsWith("/albums/"))
                {
                    var albumIdStr = pathSegments.LastOrDefault();
                    if (long.TryParse(albumIdStr, out var albumId))
                    {
                        albumIds.Add(albumId);
                    }
                }
            }
            return albumIds;
        }

        public async Task<List<string>> GetAlbumSongsByAlbumIdAsync(long albumId, string album_name, string band_name)
        {
            List<string> Songs = new List<string>();

            string base_url = $"https://www.metal-archives.com/albums/{band_name}/{album_name}/{albumId}";
            var htmlContent = await _httpClient.GetStringAsync(base_url);
            //System.Console.WriteLine(htmlContent);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);

            var songsNodes = doc.DocumentNode.SelectNodes("//table[@class='display table_lyrics']//tr/td[@class='wrapWords']");
            if (songsNodes != null)
            {
                foreach (var node in songsNodes)
                {
                    string song = node.InnerText.Trim();
                    Songs.Add(song);
                }
            }
            return Songs;
        }
    }
}