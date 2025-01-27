using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetalMiner.Infra.Interfaces
{
    public interface ITablatureHandler
    {
        Task<string> PrintAllVersesAsync(string tabUrl);
    }
}