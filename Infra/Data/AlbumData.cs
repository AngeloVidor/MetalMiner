using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace metallumscraper.Infra.Data
{
    public class AlbumData
    {
        public long band_id { get; set; }
        public string album_url { get; set; }
        public string album_name { get; set; }
        
        public List<long> album_ids { get; set; }
    }
}