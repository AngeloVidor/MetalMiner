using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using metallumscraper.Infra.Interfaces.Songsterr;

namespace metallumscraper.Infra.Services.Songsterr
{
    public class SearchEngine : ISearchEngine
    {
        private readonly HttpClient _httpClient;

        public SearchEngine(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetTabASync(string band_name, string song_name)
        {
            {
                if (!string.IsNullOrEmpty(band_name) && band_name.Contains(" "))
                {
                    band_name = band_name.Replace(" ", "%20");  
                }

                if (!string.IsNullOrEmpty(song_name) && song_name.Contains(" "))
                {
                    song_name = song_name.Replace(" ", "%20");  
                }

                string base_url = $"https://www.songsterr.com/?pattern={band_name}%20{song_name}";

                var htmlContent = await _httpClient.GetAsync(base_url);
                System.Console.WriteLine(htmlContent);

                return base_url;
            }
        }
    }
}