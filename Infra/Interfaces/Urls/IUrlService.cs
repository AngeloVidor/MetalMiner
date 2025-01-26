using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetalMiner.Infra.Data;

namespace MetalMiner.Infra.Interfaces
{
    public interface IUrlService
    {
        Task<string> GetUrlAllBandsOccurrencesAsync(string name);
        Task<string> AdvanceSearchUrlAsync(string name, string? genre);
        Task<string> ExtractBandIdFromUrlAsync(string url);

    }
}