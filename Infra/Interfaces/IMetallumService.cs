using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace metallumscraper.Infra.Interfaces;

public interface IMetallumService
{
    Task<string> BuildBandJsonSearchUrlAsync(string name, string genre);
}

