using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scrap
{
    public class MetalArchievesResponse
    {
        public string Error { get; set; }
        public int TotalRecords { get; set; }
        public int TotalDisplayRecords { get; set; }
        public int Echo { get; set; }
        public List<List<string>> AaData { get; set; }
    }
}