using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using metallumscraper.Infra.Data;
using scrap;

namespace metallumscraper.Infra.Interfaces;

public interface IMetallumService
{
    Task<string> BuildBandJsonSearchUrlAsync(string name, string genre);
    Task<string> GetBandIdAsync(string name);
    Task<string> GetBandsProfilesUrlsAsync(string name);
    Task<List<string>> GetBandDiscographyByBandIdAsync(int bandId);
}

