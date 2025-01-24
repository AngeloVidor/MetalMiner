using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using metallumscraper.Infra.Interfaces;

namespace metallumscraper.Infra.Services
{
    public class MetallumService : IMetallumService
    {
        public Task<string> BuildBandJsonSearchUrlAsync(string name, string genre)
        {
            string baseUrl = "https://www.metal-archives.com/search/ajax-advanced/searching/bands/";
            string nameParam = Uri.EscapeDataString(name);
            string genreParam = Uri.EscapeDataString(genre);
            string url = $"{baseUrl}?bandName={nameParam}&genre={genreParam}&country=&yearCreationFrom=&yearCreationTo=&bandNotes=&status=&themes=&location=&bandLabelName=&sEcho=1&iColumns=3&sColumns=&iDisplayStart=0&iDisplayLength=200&mDataProp_0=0&mDataProp_1=1&mDataProp_2=2";
            return Task.FromResult(url);
        }

        public Task<string> GetBandIdAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}