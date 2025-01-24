using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using metallumscraper.Infra.Data;
using metallumscraper.Infra.Interfaces;

namespace metallumscraper.Infra.Services
{
    public class UrlService : IUrlService
    {
        public async Task<string> AdvanceSearchUrlAsync(string name, string? genre)
        {
            if (name == null)
            {
                throw new NullReferenceException("Name cannot be null");
            }

            string baseUrl = "https://www.metal-archives.com/search/ajax-advanced/searching/bands/";
            string nameParam = Uri.EscapeDataString(name);
            string genreParam = Uri.EscapeDataString(genre);
            string url = $"{baseUrl}?bandName={nameParam}&genre={genreParam}&country=&yearCreationFrom=&yearCreationTo=&bandNotes=&status=&themes=&location=&bandLabelName=&sEcho=1&iColumns=3&sColumns=&iDisplayStart=0&iDisplayLength=200&mDataProp_0=0&mDataProp_1=1&mDataProp_2=2";
            return url;
        }

        public async Task<string> GetUrlBandOccurrencesAsync(string name)
        {
            string encodedBandName = Uri.EscapeDataString(name);
            string searchUrl = $"https://www.metal-archives.com/bands/{encodedBandName}";
            return searchUrl;
        }
    }
}