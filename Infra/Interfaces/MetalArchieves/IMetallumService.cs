using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetalMiner.Infra.Data;
using scrap;

namespace MetalMiner.Infra.Interfaces;

public interface IMetallumService
{
    Task<string> BuildBandJsonSearchUrlAsync(string name, string genre);
    Task<long> GetBandIdAsync(string name);
    Task<string> GetBandsProfilesUrlsAsync(string name);
    Task<List<AlbumData>> GetBandDiscographyByBandIdAsync(long bandId);
    Task<List<long>> GetAlbumIdsByBandIdAsync(long bandId);
    Task<List<string>> GetAlbumSongsByAlbumIdAsync(long albumId, string album_name, string band_name);
}

