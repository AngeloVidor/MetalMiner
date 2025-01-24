using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using metallumscraper.Infra.Data;

namespace metallumscraper.Infra.Interfaces
{
    public interface IUrlService
    {
        Task<string> GetUrlBandOccurrencesAsync(string name);
        Task<string> AdvanceSearchUrlAsync(string name, string? genre);

    }
}