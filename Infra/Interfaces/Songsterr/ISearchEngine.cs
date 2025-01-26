using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetalMiner.Infra.Interfaces.Songsterr
{
    public interface ISearchEngine
    {
        Task<string> GetTabASync(string band_name, string song_name);
        Task<IEnumerable<string>> GetTabsByFilterAsync(string band_name, string song_name);
    }
}